using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public interface IPlanDetailsDataAccess
    {
        Task<MemberPlanDetailsBO> GetMemberProductGuideBook(long userId, AuditLogBO auditLogBO);
    }
}