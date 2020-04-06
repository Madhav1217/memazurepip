using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aliera.Utilities;

namespace Aliera.MemberWorkflow.Controllers
{
    [Route("api/member/[Controller]")]
    [ApiController]
    public class ResetPasswordController : Controller
    {
        private readonly IResetPasswordService _resetPasswordService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResetPasswordController(IResetPasswordService resetPasswordService, IHttpContextAccessor httpContextAccessor
            , IOptions<AppSettings> appSettings)
        {
            _resetPasswordService = resetPasswordService;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Gets the user security questions.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        [Route(nameof(GetUserSecurityQuestions))]
        [HttpGet]
        public async Task<IEnumerable<SecurityQuestionsBO>> GetUserSecurityQuestions(string userName)
        {
            var auditLogBO = new AuditLogBO();
            var response = await _resetPasswordService.GetUserSecurityQuestions(userName, auditLogBO);
            return response;
        }

        /// <summary>
        /// Validates the user security q as.
        /// </summary>
        /// <param name="securityQuestionAnswers">The security question answers.</param>
        /// <returns></returns>
        [Route("IsValidUserSecurityQAs")]
        [HttpPost]
        public async Task<bool> ValidateUserSecurityQAs([FromBody]List<SecurityQuestionAnswersBO> securityQuestionAnswers)
        {
            var auditLogBO = new AuditLogBO();
            var response = await _resetPasswordService.ValidateUserSecurityQAs(securityQuestionAnswers, auditLogBO);
            return response;
        }

        /// <summary>
        /// Sends the password reset confirmation email.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="PasswordResetDateTime">The password reset date time.</param>
        /// <returns></returns>
        [Route("SendPasswordConfirmationEmail")]
        [HttpGet]
        public async Task<bool> SendPasswordResetConfirmationEmail(string userName, DateTime PasswordResetDateTime)
        {
            var auditLogBO = new AuditLogBO();
            var response = await _resetPasswordService.SendPasswordResetConfirmationEmail(userName, PasswordResetDateTime, auditLogBO);
            return response;
        }

        /// <summary>
        /// Sends the member account lock email.
        /// </summary>
        /// <param name="userName">The user identifier.</param>
        /// <returns></returns>
        [Route(nameof(SendMemberAccountLockEmail))]
        [HttpGet]
        public async Task<bool> SendMemberAccountLockEmail(string userName)
        {
            var auditLogBO = new AuditLogBO();
            var response = await _resetPasswordService.SendMemberAccountLockEmail(userName, auditLogBO);
            return response;
        }
    }
}