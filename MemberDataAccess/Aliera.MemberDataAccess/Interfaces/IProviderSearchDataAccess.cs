using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public interface IProviderSearchDataAccess
    {
        Task<ProviderSearchBO> GetProviderSearchUrl(long userId, string planType, AuditLogBO auditLogBO);
    }
}