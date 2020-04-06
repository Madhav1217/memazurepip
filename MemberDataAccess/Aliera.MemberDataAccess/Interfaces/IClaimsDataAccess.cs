using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public interface IClaimsDataAccess
    {
        Task<IEnumerable<ClaimsBO>> GetClaimsDetails(ClaimsFilterBO claimsRequestBO, AuditLogBO auditLogBO);
    }
}