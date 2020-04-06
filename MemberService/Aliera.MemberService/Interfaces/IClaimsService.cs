using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberService
{
    public interface IClaimsService
    {
        Task<IEnumerable<ClaimsBO>> GetClaimsDetails(ClaimsFilterBO claimsRequestBO, AuditLogBO auditLogBO);

        string GetEobPdfLink(string eobIdentifier, AuditLogBO auditLogBO);
    }
}