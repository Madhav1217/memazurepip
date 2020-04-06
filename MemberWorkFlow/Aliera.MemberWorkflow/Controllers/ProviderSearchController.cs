using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Aliera.Utilities;
using Aliera.Utilities.Constants;
using Aliera.AuthUtility;

namespace Aliera.MemberWorkflow.Controllers
{
    [Authorize]
    [Route("api/member/[controller]")]
    [ApiController]
    public class ProviderSearchController : Controller
    {
        private readonly IProviderSearchService _providerSearchService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProviderSearchController(IProviderSearchService providerSearchService, IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            _providerSearchService = providerSearchService;
            _appSettings = appSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the provider search URL.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="planType">Type of the plan.</param>
        /// <returns></returns>
        [Route("FetchProviderSearchURL")]
        [HttpGet]
        [ClaimRequirement("roles", "MEM_FIND_PROVIDERS", "CanRead")]
        public async Task<ProviderSearchBO> GetProviderSearchURL(long userId, string planType)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            return await _providerSearchService.GetProviderSearchUrl(userId, planType, auditLogBO);
        }
    }
}