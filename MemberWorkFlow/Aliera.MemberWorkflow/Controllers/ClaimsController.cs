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
    [Route("api/member/[Controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimsService _claimsService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor method for ClaimsController
        /// </summary>
        /// <param name="claimsService"></param>
        public ClaimsController(IClaimsService claimsService, IHttpContextAccessor httpContextAccessor
            , IOptions<AppSettings> appSettings)
        {
            _claimsService = claimsService;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Claim Details
        /// </summary>
        /// <param name="claimsRequest"></param>
        /// <returns></returns>
        [Route("ClaimsDetails")]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_SHARE_REQUESTS", "CanRead")]
        public async Task<JsonResult> GetClaimsDetails([FromBody]ClaimsFilterBO claimsRequest)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var claimsDetails = await _claimsService.GetClaimsDetails(claimsRequest, auditLogBO);
            return new JsonResult(claimsDetails);
        }
    }
}