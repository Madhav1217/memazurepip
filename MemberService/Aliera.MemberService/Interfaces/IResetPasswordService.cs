using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberService
{
    public interface IResetPasswordService
    {
        Task<IEnumerable<SecurityQuestionsBO>> GetUserSecurityQuestions(string userName, AuditLogBO auditLogBO);

        Task<bool> ValidateUserSecurityQAs(List<SecurityQuestionAnswersBO> securityQuestionAnswers, AuditLogBO auditLogBO);

        Task<bool> SendPasswordResetConfirmationEmail(string userName, DateTime passwordResetDateTime, AuditLogBO auditLogBO);

        Task<bool> SendMemberAccountLockEmail(string userName, AuditLogBO auditLogBO);
    }
}