using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Aliera.Utilities;
using Aliera.Utilities.Enumerations;
using Aliera.Utilities.Notifications;
using System.Collections.Generic;
using Aliera.Utilities.Constants;

namespace Aliera.MemberDataAccess
{
    public class MemberFeedbackDataAccess : IMemberFeedbackDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IEmailService _emailService;

        public MemberFeedbackDataAccess(IUnitOfWork unitOfWork, IEmailService emailService, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
            _emailService = emailService;
        }

        /// <summary>
        /// To save member feedback to db
        /// </summary>
        /// <param name="memberFeedbackBO"></param>
        /// <param name="auditLogBO"></param>
        /// <returns></returns>
        public async Task<bool> SaveMemberFeedback(MemberFeedbackBO memberFeedbackBO, AuditLogBO auditLogBO)
        {
            var returnValue = false;

            if (memberFeedbackBO != null)
            {
                var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(i => i, u => u.UserId == memberFeedbackBO.UserId);
                if (member != null)
                {
                    var memberDetails = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(i => i, m => m.MemberDetailId == member.MemberDetailId);

                    memberFeedbackBO.MemberExternalId = member.ExternalId;
                    var memberFeedbackInfo = MemberMapper.Map(memberFeedbackBO);
                    _unitOfWork.GetRepository<MemberFeedback>().Insert(memberFeedbackInfo);
                    var recordsInserted = await _unitOfWork.SaveChangesAsync();

                    if (recordsInserted > 0)
                    {
                        returnValue = true;
                        var mailIdFrom = "";
                        var mailTo = "";
                        var body = "";
                        var subject = "";
                        if (memberFeedbackBO.SourceId == (int)MemberFeedbackType.ShareStory)
                        {
                            mailTo = memberFeedbackBO.Email;
                            var notificaton = await _unitOfWork.GetRepository<Notification>().GetFirstOrDefaultAsync(predicate: n => n.Type == (int)EmailType.MemberStory);
                            if (notificaton != null)
                            {
                                subject = notificaton.Subject;
                                body = notificaton.Content.Replace("{firstname}", memberDetails.FirstName).Replace("{lastname}", memberDetails.LastName);
                                mailIdFrom = notificaton.From;
                            }

                            notificaton = await _unitOfWork.GetRepository<Notification>().GetFirstOrDefaultAsync(predicate: n => n.Type == (int)EmailType.MemberStoryTestimonial);
                            if (notificaton != null)
                            {
                                var marketingMailSubject = notificaton.Subject;
                                var marketingMailBody = notificaton.Content.Replace("{firstname}", memberDetails.FirstName).
                                    Replace("{lastname}", memberDetails.LastName).Replace("{email}", memberFeedbackBO.Email).
                                    Replace("{message}", memberFeedbackBO.Message);
                                mailIdFrom = notificaton.From;
                                var email = MemberMapper.Map(_appSettings.Value.AlieraMarketingEmail, marketingMailBody, marketingMailSubject, _appSettings, mailIdFrom);
                                await _emailService.SendEmailAsync(email);
                            }
                        }
                        else if (memberFeedbackBO.SourceId == (int)MemberFeedbackType.ShareContactInfo)
                        {
                            mailTo = memberFeedbackBO.InquiryTypeEmail;
                            var notificaton = await _unitOfWork.GetRepository<Notification>().GetFirstOrDefaultAsync(predicate: n => n.Type == (int)EmailType.MemberContactSubmission);
                            if (notificaton != null)
                            {
                                subject = notificaton.Subject;
                                body = notificaton.Content.Replace("{firstname}", memberDetails.FirstName).
                                    Replace("{lastname}", memberDetails.LastName).Replace("{email}", memberFeedbackBO.Email).
                                    Replace("{message}", memberFeedbackBO.Message).Replace("{phonenumber}", memberFeedbackBO.PhoneNumber)
                                    .Replace("{Inquirytype}", memberFeedbackBO.InquiryType);
                                mailIdFrom = notificaton.From;
                            }
                        }

                        var emailDetails = MemberMapper.Map(mailTo, body, subject, _appSettings, mailIdFrom);
                        await _emailService.SendEmailAsync(emailDetails);
                    }
                }
            }

            await AuditMapper.AuditLogging(auditLogBO, memberFeedbackBO.UserId, AuditAction.Insert, memberFeedbackBO);
            return returnValue;
        }

        /// <summary>
        /// Gets the member inquiry types.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<string>> GetMemberInquiryTypes()
        {
            var memberInquiryTypes = await _unitOfWork.GetRepository<MasterData>().GetPagedListAsync(a => a.Value,
                predicate: m => m.Key.ToLower() == MemberConstants.MemberInquiryType.ToLower(),
                pageIndex: 0, pageSize: 10);

            return memberInquiryTypes.Items;
        }
    }
}