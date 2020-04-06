using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public interface IMemberVerifyDataAccess
    {
        Task<Tuple<int, string, string>> IsMemberValid(MemberRegistrationBO memberRegistrationBO, AuditLogBO auditLogBO);

        Task<bool> IsUsernameAvailable(string username, AuditLogBO auditLogBO);

        Task<bool> ValidateOtp(string externalId, int otp, AuditLogBO auditLogBO);
    }
}