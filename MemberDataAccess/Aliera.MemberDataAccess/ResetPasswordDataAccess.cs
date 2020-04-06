using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aliera.Utilities;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;
using Aliera.Utilities.Helpers;
using Aliera.Utilities.Notifications;
using System.Net.Http;
using System.Web;

namespace Aliera.MemberDataAccess
{
    public class ResetPasswordDataAccess : IResetPasswordDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IOptions<AppSettings> _appSettings;

        public ResetPasswordDataAccess(IUnitOfWork iUnitOfWork, IEmailService emailService, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = iUnitOfWork;
            _emailService = emailService;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Gets the user security questions.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<IEnumerable<SecurityQuestionsBO>> GetUserSecurityQuestions(string userName, AuditLogBO auditLogBO)
        {
            var securityQuestionsBO = new List<SecurityQuestionsBO>();

            if (!string.IsNullOrEmpty(userName))
            {
                var userDetails = await _unitOfWork.GetRepository<Users>().GetFirstOrDefaultAsync(a => a, predicate: u => u.UserName.ToLower().Equals(userName.ToLower()));

                if (userDetails != null)
                {
                    var securityQuestions = await _unitOfWork.GetRepository<UserSecurityQuestionAnswers>().GetPagedListAsync(i => i,
                        predicate: item => item.UserId == userDetails.UserId);
                    var securityQuestionsData = await _unitOfWork.GetRepository<SecurityQuestions>().GetPagedListAsync(i => i,
                        pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

                    securityQuestionsBO = securityQuestions.Items.Join(securityQuestionsData.Items, a => a.SecurityQuestionId,
                        b => b.SecurityQuestionId, (a, b) => new SecurityQuestionsBO
                        {
                            SecurityQuestionId = a.SecurityQuestionId,
                            SecurityQuestion = b.SecurityQuestion,
                            UserId = a.UserId
                        }).ToList();

                    //Log audit
                    //await AuditMapper.AuditLogging(auditLogBO, userDetails.UserId, AuditAction.Select, userDetails);
                }
            }

            return securityQuestionsBO;
        }

        /// <summary>
        /// Validates the user security q as.
        /// </summary>
        /// <param name="securityQuestionAnswers">The security question answers.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<bool> ValidateUserSecurityQAs(List<SecurityQuestionAnswersBO> securityQuestionAnswers, AuditLogBO auditLogBO)
        {
            var response = false;
            var resetToken = string.Empty;
            if (securityQuestionAnswers != null && securityQuestionAnswers.Count > 0)
            {
                var securityQuestionAnswersfromDb = await _unitOfWork.GetRepository<UserSecurityQuestionAnswers>().GetPagedListAsync(i => i,
                        predicate: item => item.UserId == securityQuestionAnswers[0].UserId);
                var userDetails = await _unitOfWork.GetRepository<Users>().GetFirstOrDefaultAsync(
                    predicate: user => user.UserId == securityQuestionAnswers[0].UserId);

                securityQuestionAnswers.ForEach(item => item.Answer = EncryptionHelper.HashCode(item.Answer));

                if (securityQuestionAnswersfromDb != null)
                {
                    var securityQAsFromDb = MemberMapper.Map(securityQuestionAnswersfromDb);
                    response = securityQAsFromDb.SequenceEqual(securityQuestionAnswers, new QuestionComparer());

                    if (response)
                    {
                        await GetPasswordResetToken(userDetails.UserName, _appSettings.Value.AuthUrl, userDetails.UserId);
                        //await SendPassWordResetLink(userDetails.UserName, userDetails.UserId, resetToken);
                    }
                }
                else
                    response = false;
            }

            //await AuditMapper.AuditLogging(auditLogBO, securityQuestionAnswers[0].UserId, AuditAction.Select, null);
            return response;
        }

        /// <summary>
        /// SendPasswordResetConfirmationEmail
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passwordResetDateTime"></param>
        /// <param name="auditLogBO"></param>
        /// <returns></returns>
        public async Task<bool> SendPasswordResetConfirmationEmail(string userName, DateTime passwordResetDateTime, AuditLogBO auditLogBO)
        {
            var response = false;
            var userRepository = _unitOfWork.GetRepository<Users>();
            var userDetails = await userRepository.GetFirstOrDefaultAsync(i => i, predicate: u => u.UserName == userName);
            if (userDetails != null)
            {
                var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(i => i, m => m.UserId == userDetails.UserId);
                var memberDetails = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(i => i, m => m.MemberDetailId == member.MemberDetailId);

                var notificaton = await _unitOfWork.GetRepository<Notification>().GetFirstOrDefaultAsync(predicate: n => n.Type == (int)EmailType.MemberResetSuccess);
                if (notificaton != null)
                {
                    var subject = notificaton.Subject.Replace("{externalId}", member.ExternalId);
                    var messageBody = notificaton.Content.Replace("{firstname}", memberDetails.FirstName).Replace("{lastname}", memberDetails.LastName).
                        Replace("passwordresetdate", passwordResetDateTime.ToString("MM/dd/yyyy")).
                        Replace("passwordresettime", passwordResetDateTime.ToString("hh:mm tt")).
                        Replace("{loginPath}", _appSettings.Value.LoginPath);

                    var emailDetails = MemberMapper.Map(memberDetails.EmailId, messageBody, subject, _appSettings, notificaton.From);

                    //Log audit
                    //await AuditMapper.AuditLogging(auditLogBO, userDetails.UserId, AuditAction.Select, member);
                    response = await _emailService.SendEmailAsync(emailDetails);
                }
            }

            return response;
        }

        /// <summary>
        /// Sends the member account lock email.
        /// </summary>
        /// <param name="userName">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<bool> SendMemberAccountLockEmail(string userName, AuditLogBO auditLogBO)
        {
            var response = false;
            var userRepository = _unitOfWork.GetRepository<Users>();
            var userDetails = await userRepository.GetFirstOrDefaultAsync(i => i, predicate: u => u.UserName == userName);
            if (userDetails != null)
            {
                var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(i => i, m => m.UserId == userDetails.UserId);
                var memberDetails = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(i => i, m => m.MemberDetailId == member.MemberDetailId);

                var notificaton = await _unitOfWork.GetRepository<Notification>().GetFirstOrDefaultAsync(predicate: n => n.Type == (int)EmailType.MemberAccountAccess);
                if (notificaton != null)
                {
                    var subject = notificaton.Subject.Replace("{externalId}", member.ExternalId);
                    var messageBody = notificaton.Content.Replace("{firstname}", memberDetails.FirstName).Replace("{lastname}", memberDetails.LastName);

                    var emailDetails = MemberMapper.Map(memberDetails.EmailId, messageBody, subject, _appSettings, notificaton.From);

                    //Log audit
                    //await AuditMapper.AuditLogging(auditLogBO, userDetails.UserId, AuditAction.Select, member);
                    response = await _emailService.SendEmailAsync(emailDetails);
                }
            }

            return response;
        }

        /// <summary>
        /// Updates the reset password access code.
        /// </summary>
        /// <param name="securityQuestionAnswers">The security question answers.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        //private async Task UpdateResetPasswordAccessCode(List<SecurityQuestionAnswersBO> securityQuestionAnswers, AuditLogBO auditLogBO)
        //{
        //    var userRepository = _unitOfWork.GetRepository<Users>();
        //    var userDetails = await userRepository.GetFirstOrDefaultAsync(i => i, predicate: u => u.UserId == securityQuestionAnswers[0].UserId);
        //    var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(i => i, m => m.UserId == userDetails.UserId);
        //    var memberDetails = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(i => i,
        //        m => m.MemberDetailId == member.MemberDetailId);

        //    var guid = Guid.NewGuid().ToString();
        //    userDetails = MemberMapper.Map(userDetails, guid, securityQuestionAnswers[0].UserId);
        //    userRepository.Update(userDetails);

        //    //await SendPassWordResetLink(securityQuestionAnswers, member, memberDetails, guid);
        //    await _unitOfWork.SaveChangesAsync();

        //    await AuditMapper.AuditLogging(auditLogBO, securityQuestionAnswers[0].UserId, AuditAction.Update, member);
        //}

        /// <summary>
        /// Sends the pass word reset link.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        private async Task SendPassWordResetLink(string username, long userId, string token)
        {
            dynamic memberDetails;
            var email = string.Empty;
            var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(i => i, m => m.UserId == userId);
            if (member != null)
            {
                memberDetails = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(i => i,
              m => m.MemberDetailId == member.MemberDetailId);
                email = memberDetails.EmailId;
            }
            else
            {
                memberDetails = await _unitOfWork.GetRepository<Users>().GetFirstOrDefaultAsync(i => i,
                  m => m.UserId == userId);
                email = memberDetails.Email;
            }

            var notificatonInfo = await _unitOfWork.GetRepository<Notification>().GetFirstOrDefaultAsync(predicate: n => n.Type == (int)EmailType.MemberResetRequest);
            var link = "http://" + _appSettings.Value.ServerIP + "/#" + _appSettings.Value.PasswordResetPage +
                       "?token=" + token + "&username=" + username;

            var subject = notificatonInfo.Subject.Replace("{UserId}", member != null ? member.ExternalId : Convert.ToString(userId));
            var messageBody = notificatonInfo.Content.Replace("{firstname}", memberDetails.FirstName).Replace("{lastname}", memberDetails.LastName).Replace("{link}", link);
            var emailDetails = MemberMapper.Map(email, messageBody, subject, _appSettings, notificatonInfo?.From);
            await _emailService.SendEmailAsync(emailDetails);
        }

        /// <summary>
        /// GetPasswordResetToken
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="authUrl"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task GetPasswordResetToken(string userName, string authUrl, long userId)
        {
            var token = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(authUrl);
                var response = await client.GetAsync("Auth/getpasswordresettoken?userName=" + userName);
                if (response.IsSuccessStatusCode)
                    token = await response.Content.ReadAsStringAsync();
            }

            await SendPassWordResetLink(userName, userId, HttpUtility.UrlEncode(token));
        }
    }

    public class QuestionComparer : IEqualityComparer<SecurityQuestionAnswersBO>
    {
        public bool Equals(SecurityQuestionAnswersBO x, SecurityQuestionAnswersBO y)
        {
            if (x.SecurityQuestionId == y.SecurityQuestionId && x.Answer.ToLower() == y.Answer.ToLower())
                return true;

            return false;
        }

        public int GetHashCode(SecurityQuestionAnswersBO obj)
        {
            return obj.GetHashCode();
        }
    }
}