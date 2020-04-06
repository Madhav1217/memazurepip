using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Threading.Tasks;
using System;

namespace Aliera.MemberService
{
    public interface IMemberVerifyService
    {
        Task<Tuple<int, string, string>> IsMemberValid(MemberRegistrationBO memberRegBO, AuditLogBO auditLogBO);

        Task<bool> IsUsernameAvailable(string username, AuditLogBO auditLogBO);

        Task<bool> ValidateOtp(string memberId, int otp, AuditLogBO auditLogBO);
    }
}