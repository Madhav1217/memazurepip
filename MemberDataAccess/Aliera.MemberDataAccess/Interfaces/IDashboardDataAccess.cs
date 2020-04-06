using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aliera.MemberDataAccess
{
    public interface IDashboardDataAccess
    {
        Task<IEnumerable<MemberDataBO>> GetMemberAndDependentNames(long userId, AuditLogBO auditLogBO);

        Task<MemberDashboardBO> GetMemberDetailsForDashBoard(long userId, AuditLogBO auditLogBO, int leftNavPermission);

        Task<CoveredPlansBO> GetMemberPlanTypes(long userId, AuditLogBO auditLogBO);

        Task<IEnumerable<SecurityQuestionAnswersBO>> GetSecurityQuestionsByUserId(long userId, AuditLogBO auditLogBO);

        Task<bool> UpdateSecurityQuestionsByUserId(List<SecurityQuestionAnswersBO> updatedSecurityQuestions, AuditLogBO auditLogBO);

        Task<Tuple<bool, int, int>> IsGroupMember(long userId, AuditLogBO auditLogBO);

        Task<MemberDashboardBO> GetMemberPlanDetails(long memberId, long userId, AuditLogBO auditLogBO);

        Task<string> GetMemberExternalId(long userId);
    }
}