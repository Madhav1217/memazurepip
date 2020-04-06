using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberService
{
    public interface IAccumulatorService
    {
        Task<IEnumerable<MemberAccumulatorDetailsBO>> GetMemberAccumulatorDetails(int userId, AuditLogBO auditLogBO);

        Task<IEnumerable<MemberAccumulatorDetailsBO>> GetIndividualAccumulatorDetails(int? userId, int? dependentDetailId, AuditLogBO auditLogBO);
    }
}