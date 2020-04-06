using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public interface IMemberRegistrationDataAccess
    {
        Task<int> RegisterMember(MemberRegistrationBO memberRegistration, AuditLogBO auditLogBO);

        Task<int> SendOtpToMember(MemberRegistrationBO memberInfo, AuditLogBO auditLogBO);
    }
}