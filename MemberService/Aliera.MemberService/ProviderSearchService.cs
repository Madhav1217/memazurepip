using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;

namespace Aliera.MemberService
{
    public class ProviderSearchService : IProviderSearchService
    {
        public readonly IProviderSearchDataAccess ProviderSearchDataAccess;

        public ProviderSearchService(IProviderSearchDataAccess providerSearchDataAccess)
        {
            ProviderSearchDataAccess = providerSearchDataAccess;
        }

        /// <summary>
        /// Gets the provider search URL.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="planType">Type of the plan.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">ProviderSearchServiceGetProviderSearchUrlInputEmptyErrorCode</exception>
        public async Task<ProviderSearchBO> GetProviderSearchUrl(long userId, string planType, AuditLogBO auditLogBO)
        {
            if (planType == null || userId <= 0)
                throw new CustomException(nameof(MemberConstants.ProviderSearchServiceGetProviderSearchUrlInputEmptyErrorCode));
            return await ProviderSearchDataAccess.GetProviderSearchUrl(userId, planType, auditLogBO);
        }
    }
}