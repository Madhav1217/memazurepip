using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Threading.Tasks;

namespace Aliera.MemberService
{
    public interface IProviderSearchService
    {
        Task<ProviderSearchBO> GetProviderSearchUrl(long userId, string planType, AuditLogBO auditLogBO);
    }
}