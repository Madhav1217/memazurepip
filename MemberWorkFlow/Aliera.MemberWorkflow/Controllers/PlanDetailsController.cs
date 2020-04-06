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
    public class PlanDetailsController : Controller
    {
        private readonly IPlanDetailsService _planDetailsService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlanDetailsController(IPlanDetailsService planDetailsService, IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            _planDetailsService = planDetailsService;
            _appSettings = appSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the member product guide book.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [Route("GetMemberGuideBook")]
        [HttpGet]
        [ClaimRequirement("roles", "MEM_MY_PLAN_DETAILS", "CanRead")]
        public async Task<MemberPlanDetailsBO> GetMemberProductGuideBook(long userId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            return await _planDetailsService.GetMemberProductGuideBook(userId, auditLogBO);
        }
    }
}