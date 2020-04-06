using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Threading.Tasks;

namespace Aliera.MemberService
{
    public interface IPlanDetailsService
    {
        Task<MemberPlanDetailsBO> GetMemberProductGuideBook(long userId, AuditLogBO auditLogBO);
    }
}