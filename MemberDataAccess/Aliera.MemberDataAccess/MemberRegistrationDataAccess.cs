using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Broker;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Aliera.Utilities;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;
using Aliera.Utilities.Helpers;
using Aliera.Utilities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Aliera.MemberDataAccess
{
    public class MemberRegistrationDataAccess : IMemberRegistrationDataAccess
    {
        private readonly IUnitOfWork<Context> _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ISMSService _sMsService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IMemoryCache _memoryCache;

        public MemberRegistrationDataAccess(IUnitOfWork<Context> unitOfWork, IEmailService emailService, ISMSService sMsService,
            IOptions<AppSettings> appSettings, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _sMsService = sMsService;
            _appSettings = appSettings;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Registers the member.
        /// </summary>
        /// <param name="memberRegistration">The member registration.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<int> RegisterMember(MemberRegistrationBO memberRegistration, AuditLogBO auditLogBO)
        {
            bool isSuccessful;
            var externalId = memberRegistration.ExternalId;
            var statusCode = MemberRegistrationVerification.InvalidMemberId;
            var isRegistrationSuccessful = 0;

            if (!string.IsNullOrEmpty(externalId))
            {
                statusCode = await VerifyMemberDetails(memberRegistration, externalId, statusCode);

                if (statusCode == MemberRegistrationVerification.Verified)
                {
                    var user = MemberMapper.Map(memberRegistration);
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        try
                        {
                            //_unitOfWork.GetRepository<Users>().Insert(user);
                            //_unitOfWork.DbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Users ON");
                            //isRegistrationSuccessful += _unitOfWork.SaveChanges();
                            //_unitOfWork.DbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Users OFF");

                            var newUserIdSecurityQA = memberRegistration.SecurityQuestionAnswer.Select(item => new UserSecurityQuestionAnswers
                            {
                                UserId = user.UserId,
                                SecurityQuestionId = item.SecurityQuestionId,
                                Answer = EncryptionHelper.HashCode(item.Answer),
                                CreatedBy = item.CreatedBy,
                                CreatedOn = DateTime.UtcNow,
                                ModifiedBy = item.ModifiedBy,
                                ModifiedOn = memberRegistration.ModifiedOn
                            });

                            _unitOfWork.GetRepository<UserSecurityQuestionAnswers>().Insert(newUserIdSecurityQA);
                            isRegistrationSuccessful += _unitOfWork.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            throw;
                        }
                        scope.Complete();
                    }

                    isSuccessful = isRegistrationSuccessful > 0;

                    if ((isSuccessful && memberRegistration.UpdateToAdmin123) || memberRegistration.PhoneNumberNotFound
                        || memberRegistration.EmailNotFound)
                    {
                        await this.UpateMemberEmailAndPhoneNumber(memberRegistration);
                        await this.UpateEmailAndPhoneNumberToAdmin123(memberRegistration);
                    }

                    await AuditMapper.AuditLogging(auditLogBO, null, AuditAction.Update, memberRegistration);
                }
                else
                    isSuccessful = false;

                if (isSuccessful)
                    await SendRegistrationConformationMail(memberRegistration);
            }

            return (int)statusCode;
        }

        /// <summary>
        /// Sends the otp to member.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> SendOtpToMember(MemberRegistrationBO memberInfo, AuditLogBO auditLogBO)
        {
            var sentOtpStatus = MemberRegistrationVerification.InvalidDetails;
            var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(predicate: m => m.ExternalId == memberInfo.ExternalId);

            if (member != null && memberInfo.Mode != 0 && !string.IsNullOrEmpty(memberInfo.ModeValue))
            {
                var memberDetail = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(
                    predicate: detail => detail.MemberDetailId == member.MemberDetailId);

                var rand = new Random();
                var otpValue = rand.Next(100000, 999999);
                _memoryCache.Set(member.MemberId, otpValue);
                _memoryCache.Set(member.MemberId + MemberConstants.GeneratedTime, DateTime.Now);
                var notification = await _unitOfWork.GetRepository<Notification>().GetFirstOrDefaultAsync(predicate: n => n.Type == (int)EmailType.MemberRegistrationOtp);
                if (notification != null)
                {
                    var otpMessageContent = notification.Content.Replace("{otp}", otpValue.ToString());
                    switch (memberInfo.Mode)
                    {
                        case (int)NotificationType.Email:
                            var mailDetails = MemberMapper.Map(memberInfo.ModeValue, otpMessageContent, notification.Subject, _appSettings, notification.From);
                            if (await _emailService.SendEmailAsync(mailDetails))
                                sentOtpStatus = MemberRegistrationVerification.SentOtpSuccessfully;
                            break;

                        case (int)NotificationType.Sms:
                            var smsDetails = MemberMapper.Map(_appSettings.Value.CountryCode + memberInfo.ModeValue, otpMessageContent, _appSettings);
                            if (_sMsService.SendMessage(smsDetails))
                                sentOtpStatus = MemberRegistrationVerification.SentOtpSuccessfully;
                            break;

                        default:
                            break;
                    }
                }
            }

            //await AuditMapper.AuditLogging(auditLogBO, null, AuditAction.Select, memberInfo);
            return (int)sentOtpStatus;
        }

        /// <summary>
        /// Send an conformation mail to member registered mailID
        /// </summary>
        /// <param name="memberRegistration">MemberRegistrationBO</param>
        private async Task SendRegistrationConformationMail(MemberRegistrationBO memberRegistration)
        {
            if (memberRegistration != null)
            {
                var notificaton = await _unitOfWork.GetRepository<Notification>().GetFirstOrDefaultAsync(predicate: n => n.Type == (int)EmailType.MemberRegistrationSuccess);
                if (notificaton != null)
                {
                    var subject = notificaton.Subject.Replace("{externalId}", memberRegistration.ExternalId);
                    var message = notificaton.Content.Replace("{firstname}", memberRegistration.FirstName).
                        Replace("{lastname}", memberRegistration.LastName).
                        Replace("{loginPath}", _appSettings.Value.LoginPath);

                    var emailDetails = MemberMapper.Map(memberRegistration.Email, message, subject, _appSettings, notificaton.From);
                    await _emailService.SendEmailAsync(emailDetails);
                }
            }
        }

        /// <summary>
        /// Verifies the member details.
        /// </summary>
        /// <param name="memberRegistration">The member registration.</param>
        /// <param name="externalId">The external identifier.</param>
        /// <param name="statusCode">The status code.</param>
        /// <returns></returns>
        private async Task<MemberRegistrationVerification> VerifyMemberDetails(MemberRegistrationBO memberRegistration, string externalId, MemberRegistrationVerification statusCode)
        {
            var memberRepo = _unitOfWork.GetRepository<Member>();
            var memberDependentRepo = _unitOfWork.GetRepository<MemberDependent>();
            var memberDetailsRepo = _unitOfWork.GetRepository<MemberDetail>();

            MemberDependent memDependentDetails;
            var memDetails = await memberRepo.GetFirstOrDefaultAsync(a => a, predicate: b => b.Admin123Id == externalId);
            if (memDetails == null)
            {
                memDetails = await memberRepo.GetFirstOrDefaultAsync(a => a, predicate: b => b.ExternalId == externalId);
                memDependentDetails = await memberDependentRepo.GetFirstOrDefaultAsync(a => a, predicate: b => b.ExternalId == externalId);
            }
            else
                memDependentDetails = await memberDependentRepo.GetFirstOrDefaultAsync(a => a, predicate: b => b.Admin123Id == externalId);

            if (memDetails != null)
            {
                statusCode = memDetails.UserId != null ?
                        MemberRegistrationVerification.ExistingMember : MemberRegistrationVerification.Verified;

                if (statusCode == MemberRegistrationVerification.Verified)
                {
                    memberRegistration.MemberId = memDetails.MemberId;
                    memDetails.UserId = memberRegistration.UserId;
                    memberRepo.Update(memDetails);
                }
            }
            else if (memDependentDetails != null)
            {
                statusCode = memDependentDetails.UserId != null ?
                    MemberRegistrationVerification.ExistingMember : MemberRegistrationVerification.Verified;

                if (statusCode == MemberRegistrationVerification.Verified)
                {
                    memberRegistration.MemberId = memDependentDetails.MemberId;
                    memDependentDetails.UserId = memberRegistration.UserId;
                    memberDependentRepo.Update(memDependentDetails);
                }
            }

            return statusCode;
        }

        /// <summary>
        /// Upates the email and phone number to admin123.
        /// </summary>
        /// <param name="memberRegistration">The member registration.</param>
        /// <returns></returns>
        private async Task<string> UpateEmailAndPhoneNumberToAdmin123(MemberRegistrationBO memberRegistration)
        {
            var admin123 = new Admin123Mapper(_unitOfWork);
            try
            {
                var memberDetails = await this.GetMemberDetailsForUpdateAsync(memberRegistration);
                if (!string.IsNullOrEmpty(memberDetails.Item1))
                {
                    var admin123BO = new Admin123BO
                    {
                        CORPID = _appSettings.Value.CorpId,
                        AGENT = _appSettings.Value.AgentId,
                        //UNIQUEID = $"{memberDetails.Item1}-{AppSettings.Environment}",
                        LASTNAME = memberRegistration.LastName
                    };

                    if (memberRegistration.UpdateToAdmin123 || (memberRegistration.EmailNotFound && memberRegistration.PhoneNumberNotFound))
                    {
                        admin123BO.PHONE1 = memberRegistration.PhoneNumber;
                        admin123BO.EMAIL = memberRegistration.Email;
                    }
                    else if (memberRegistration.EmailNotFound)
                        admin123BO.EMAIL = memberRegistration.Email;
                    else if (memberRegistration.PhoneNumberNotFound)
                        admin123BO.EMAIL = memberRegistration.PhoneNumber;

                    var memberDetailsObject = JsonConvert.SerializeObject(admin123BO);
                    var uri = new Uri(_appSettings.Value.Admin123Url);
                    var stringContent = new StringContent(memberDetailsObject, UnicodeEncoding.UTF8, "application/json");
                    var task = Task.Run(() => admin123.PostURIAsync(uri, stringContent,
                        _appSettings.Value.Admin123User, _appSettings.Value.Admin123Password, memberDetailsObject, memberDetails.Item1));
                    task.Wait();
                    return task.Result;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                await admin123.InsertAdmin123LogAsync(ex.StackTrace, ex.Message.ToString());
                throw;
            }
        }

        /// <summary>
        /// Upates the member email and phone number.
        /// </summary>
        /// <param name="memberRegistration">The member registration.</param>
        private async Task UpateMemberEmailAndPhoneNumber(MemberRegistrationBO memberRegistration)
        {
            var memberDetailsRepo = _unitOfWork.GetRepository<MemberDetail>();
            var memberDetails = await this.GetMemberDetailsForUpdateAsync(memberRegistration);

            if (memberDetails.Item2.MemberDetailId != 0)
            {
                memberDetailsRepo.Update(memberDetails.Item2);
                _unitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the member details for update.
        /// </summary>
        /// <param name="memberRegistration">The member registration.</param>
        /// <returns></returns>
        private async Task<Tuple<string, MemberDetail>> GetMemberDetailsForUpdateAsync(MemberRegistrationBO memberRegistration)
        {
            var memberDetailsRepo = _unitOfWork.GetRepository<MemberDetail>();
            var memberDetails = new MemberDetail();

            var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(a => a,
                predicate: m => m.Admin123Id == memberRegistration.ExternalId || m.ExternalId == memberRegistration.ExternalId);
            if (member != null)
            {
                memberDetails = await memberDetailsRepo.GetFirstOrDefaultAsync(a => a,
                predicate: md => md.MemberDetailId == member.MemberDetailId);

                if (memberDetails != null)
                {
                    if (memberRegistration.UpdateToAdmin123 || (memberRegistration.EmailNotFound && memberRegistration.PhoneNumberNotFound))
                    {
                        memberDetails.PhoneNumber = memberRegistration.PhoneNumber;
                        memberDetails.EmailId = memberRegistration.Email;
                    }
                    else if (memberRegistration.EmailNotFound)
                        memberDetails.EmailId = memberRegistration.Email;
                    else if (memberRegistration.PhoneNumberNotFound)
                        memberDetails.EmailId = memberRegistration.PhoneNumber;
                }
            }

            return Tuple.Create(member.ExternalId, memberDetails);
        }
    }
}