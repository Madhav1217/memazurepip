using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Threading.Tasks;

namespace Aliera.MemberService
{
    public interface IMemberRegistrationService
    {
        Task<int> RegisterMember(MemberRegistrationBO memberRegistration, AuditLogBO auditLogBO);

        Task<int> SendOtpToMember(MemberRegistrationBO memberInfo, AuditLogBO auditLogBO);
    }
}