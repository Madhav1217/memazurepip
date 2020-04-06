using Aliera.BusinessObjects.Audit;
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
    public class AccumulatorsController : ControllerBase
    {
        private readonly IAccumulatorService _accumulatorService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor method for AccumulatorsController
        /// </summary>
        /// <param name="accumulatorService"></param>
        public AccumulatorsController(IAccumulatorService accumulatorService, IHttpContextAccessor httpContextAccessor
            , IOptions<AppSettings> appSettings)
        {
            _accumulatorService = accumulatorService;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Accumulator Details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("MemberAccumulatorDetails")]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_FAMILY_ACCUMULATORS", "CanRead")]
        public async Task<JsonResult> GetMemberAccumulatorDetails(int userId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var memberAccumulatorDetails = await _accumulatorService.GetMemberAccumulatorDetails(userId, auditLogBO);
            return new JsonResult(memberAccumulatorDetails);
        }

        /// <summary>
        /// Gets the individual accumulator details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="dependentDetailId">The dependent detail identifier.</param>
        /// <returns></returns>
        [Route("IndividualAccumulatorDetails")]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_INDIVIDUAL_MSRA_AND_LIMITS", "CanRead")]
        public async Task<JsonResult> GetIndividualAccumulatorDetails(int? userId, int? dependentDetailId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var individualAccumulatorDetails = await _accumulatorService.GetIndividualAccumulatorDetails(userId, dependentDetailId, auditLogBO);
            return new JsonResult(individualAccumulatorDetails);
        }
    }
}