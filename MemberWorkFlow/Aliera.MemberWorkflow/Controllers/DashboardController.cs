using Aliera.AuthUtility;
using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Auth;
using Aliera.BusinessObjects.Member;
using Aliera.MemberService;
using Aliera.Utilities;
using Aliera.Utilities.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aliera.MemberWorkflow.Controllers
{
    [Authorize]
    [Route("api/member/[Controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardController(IDashboardService dashboardService, IHttpContextAccessor httpContextAccessor
            , IOptions<AppSettings> appSettings)
        {
            _dashboardService = dashboardService;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
        }

        /// <summary>
        /// getmemberdetails
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route(nameof(GetMemberAndDependentNames))]
        [HttpGet]
        public async Task<IActionResult> GetMemberAndDependentNames(long userId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var response = await _dashboardService.GetMemberAndDependentNames(userId, auditLogBO);
            return Ok(response);
        }

        /// <summary>
        /// Gets the member details for dash board.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [Route(nameof(GetMemberDetailsForDashBoard))]
        [HttpGet]
        public async Task<MemberDashboardBO> GetMemberDetailsForDashBoard(long userId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);

            var tokenObject = UtilityHelper.ParseJwt(jwt);
            var rolesJson = tokenObject.Claims.FirstOrDefault(c => c.Type.ToLower() == MemberConstants.Roles.ToLower()).Value;
            var customRoles = JsonConvert.DeserializeObject<List<CustomRolesBO>>(rolesJson);
            var leftNavPermission = 0;
            if (customRoles != null)
            {
                leftNavPermission = customRoles.Any(r => r.Privileges.
                Any(p => p.Name.ToLower() == MemberConstants.MemberLeftPanPermission.ToLower() && p.Permission.Any(pe => (int)pe.CanRead == 1))) ? 1 : 0;
            }

            return await _dashboardService.GetMemberDetailsForDashBoard(userId, auditLogBO, leftNavPermission);
        }

        /// <summary>
        /// Gets the member plan types.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [Route(nameof(GetMemberPlanTypes))]
        [HttpGet]
        [ClaimRequirement("roles", "MEM_LEFT_PAN_MEMBER_AND_PROGRAM_DETAILS", "CanRead")]
        public async Task<CoveredPlansBO> GetMemberPlanTypes(long userId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            return await _dashboardService.GetMemberPlanTypes(userId, auditLogBO);
        }

        /// <summary>
        /// Gets the security questions by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [Route(nameof(GetSecurityQuestionsByUserId))]
        [HttpGet]
        [ClaimRequirement("roles", "MEM_CHANGE_SECURITY_QUESTIONS", "CanRead")]
        public async Task<IEnumerable<SecurityQuestionAnswersBO>> GetSecurityQuestionsByUserId(long userId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            return await _dashboardService.GetSecurityQuestionsByUserId(userId, auditLogBO);
        }

        [Route(nameof(UpdateSecurityQuestionsByUserId))]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_CHANGE_SECURITY_QUESTIONS", "CanUpdate")]
        public async Task<bool> UpdateSecurityQuestionsByUserId([FromBody]List<SecurityQuestionAnswersBO> updatedSecurityQuestions)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            return await _dashboardService.UpdateSecurityQuestionsByUserId(updatedSecurityQuestions, auditLogBO);
        }

        /// <summary>
        /// Determines whether [is group member] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [Route(nameof(IsGroupMember))]
        [HttpGet]
        public async Task<Tuple<bool, int, int>> IsGroupMember(long userId)
        {
            var auditLogBO = new AuditLogBO();
            return await _dashboardService.IsGroupMember(userId, auditLogBO);
        }

        /// <summary>
        /// Gets the member plan details.
        /// </summary>
        /// <param name="memberId">The member identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [Route("GetPlanDetails")]
        [HttpGet]
        public async Task<MemberDashboardBO> GetMemberPlanDetails(long memberId, long userId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            return await _dashboardService.GetMemberPlanDetails(memberId, userId, auditLogBO);
        }

        /// <summary>
        /// Gets the member external identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [Route(nameof(GetMemberExternalId))]
        [HttpGet]
        public async Task<string> GetMemberExternalId(long userId)
        {
            return await _dashboardService.GetMemberExternalId(userId);
        }
    }
}