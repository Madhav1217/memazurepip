using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;

namespace Aliera.MemberService
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IResetPasswordDataAccess _resetPasswordDataAccess;

        public ResetPasswordService(IResetPasswordDataAccess resetPasswordDataAccess)
        {
            _resetPasswordDataAccess = resetPasswordDataAccess;
        }

        /// <summary>
        /// Gets the user security questions.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberUsernameForSecurityQuestionsEmptyErrorCode
        /// or
        /// MemberResponseForGetSecurityQuestionsEmptyErrorCode
        /// </exception>
        public async Task<IEnumerable<SecurityQuestionsBO>> GetUserSecurityQuestions(string userName, AuditLogBO auditLogBO)
        {
            if (userName == string.Empty) throw new CustomException(nameof(MemberConstants.MemberUsernameForSecurityQuestionsEmptyErrorCode));
            var securityQuestions = await _resetPasswordDataAccess.GetUserSecurityQuestions(userName, auditLogBO);
            if (securityQuestions == null) throw new CustomException(nameof(MemberConstants.MemberResponseForGetSecurityQuestionsEmptyErrorCode));
            return securityQuestions;
        }

        /// <summary>
        /// Validates the user security q as.
        /// </summary>
        /// <param name="securityQuestionAnswers">The security question answers.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberSecurityQAForValidationEmptyErrorCode
        /// or
        /// MemberResponseForSecurityQAValidationEmptyErrorCode
        /// </exception>
        public async Task<bool> ValidateUserSecurityQAs(List<SecurityQuestionAnswersBO> securityQuestionAnswers, AuditLogBO auditLogBO)
        {
            if (securityQuestionAnswers == null) throw new CustomException(nameof(MemberConstants.MemberSecurityQAForValidationEmptyErrorCode));
            var response = await _resetPasswordDataAccess.ValidateUserSecurityQAs(securityQuestionAnswers, auditLogBO);
            if (response == false) throw new CustomException(nameof(MemberConstants.MemberResponseForSecurityQAValidationEmptyErrorCode));
            return response;
        }

        /// <summary>
        /// Sends the password reset confirmation email.
        /// </summary>
        /// <param name="userName">The user identifier.</param>
        /// <param name="passwordResetDateTime">The password reset date time.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberParametersForSendPasswordResetConfirmationEmailEmptyErrorCode
        /// or
        /// MemberResponseForSendPasswordResetConfirmationEmailEmptyErrorCode
        /// </exception>
        public async Task<bool> SendPasswordResetConfirmationEmail(string userName, DateTime passwordResetDateTime, AuditLogBO auditLogBO)
        {
            if (string.IsNullOrEmpty(userName) || passwordResetDateTime == null) throw new CustomException(nameof(MemberConstants.MemberParametersForSendPasswordResetConfirmationEmailEmptyErrorCode));
            var response = await _resetPasswordDataAccess.SendPasswordResetConfirmationEmail(userName, passwordResetDateTime, auditLogBO);
            if (response == false) throw new CustomException(nameof(MemberConstants.MemberResponseForSendPasswordResetConfirmationEmailEmptyErrorCode));
            return response;
        }

        /// <summary>
        /// Sends the member account lock email.
        /// </summary>
        /// <param name="userName">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberUserIdForSendMemberAccountLockEmailEmptyErrorCode
        /// or
        /// MemberResponseForSendMemberAccountLockEmailEmptyErrorCode
        /// </exception>
        public async Task<bool> SendMemberAccountLockEmail(string userName, AuditLogBO auditLogBO)
        {
            if (string.IsNullOrEmpty(userName)) throw new CustomException(nameof(MemberConstants.MemberUserIdForSendMemberAccountLockEmailEmptyErrorCode));
            var response = await _resetPasswordDataAccess.SendMemberAccountLockEmail(userName, auditLogBO);
            if (response == false) throw new CustomException(nameof(MemberConstants.MemberResponseForSendMemberAccountLockEmailEmptyErrorCode));
            return response;
        }
    }
}