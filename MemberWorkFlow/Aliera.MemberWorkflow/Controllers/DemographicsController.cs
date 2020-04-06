using Aliera.AuthUtility;
using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Auth;
using Aliera.BusinessObjects.Broker;
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

namespace Aliera.MemberWorkflow.Controllers
{
    [Authorize]
    [Route("api/member/[controller]")]
    [ApiController]
    public class DemographicsController : ControllerBase
    {
        private readonly IDemographicsService _demographicsService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor method for DemographicsController
        /// </summary>
        /// <param name="demographicsService,appSettings,httpContextAccessor"></param>
        public DemographicsController(IDemographicsService demographicsService,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            _demographicsService = demographicsService;
            _appSettings = appSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the member demographics.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>json string of Member Demographics, Dependents and Coverage</returns>

        [Route("MemberDemographics")]
        [HttpGet]
        public async Task<JsonResult> GetMemberDemographics(int userId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);

            var tokenObject = UtilityHelper.ParseJwt(jwt);
            var rolesJson = tokenObject.Claims.FirstOrDefault(c => c.Type.ToLower() == MemberConstants.Roles.ToLower()).Value;
            var customRoles = JsonConvert.DeserializeObject<List<CustomRolesBO>>(rolesJson);

            var demographicsPermissionBO = new DemographicsPermissionBO
            {
                UserId = userId,
                AuditLogBO = auditLogBO
            };

            if (customRoles != null)
            {
                demographicsPermissionBO.SubscriberInfoPermission = customRoles.Any(r => r.Privileges.
                Any(p => p.Name.ToLower() == MemberConstants.MemberSubscriberInfoPermission.ToLower() && p.Permission.Any(pe => (int)pe.CanRead == 1))) ? 1 : 0;

                demographicsPermissionBO.DependentInfoPermission = customRoles.Any(r => r.Privileges.
                Any(p => p.Name.ToLower() == MemberConstants.MemberDependentInfoPermission.ToLower() && p.Permission.Any(pe => (int)pe.CanRead == 1))) ? 1 : 0;

                demographicsPermissionBO.ServiceInfoPermission = customRoles.Any(r => r.Privileges.
                Any(p => p.Name.ToLower() == MemberConstants.MemberServiceInfoPermission.ToLower() && p.Permission.Any(pe => (int)pe.CanRead == 1))) ? 1 : 0;

                demographicsPermissionBO.DigitalIdCardPermission = customRoles.Any(r => r.Privileges.
                Any(p => p.Name.ToLower() == MemberConstants.MemberIdCardsPermission.ToLower() && p.Permission.Any(pe => (int)pe.CanRead == 1))) ? 1 : 0;
            }

            var memberDemographics = await _demographicsService.GetMemberDemographics(demographicsPermissionBO);
            return new JsonResult(memberDemographics);
        }

        /// <summary>
        /// Updates Member Demographics for a given member demographic body
        /// </summary>
        /// <param name="memberDemographics"></param>
        /// <returns>row of updated Primary Member Demographics</returns>
        [Route(nameof(UpdateMemberDemographics))]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_SUBSCRIBER_INFORMATION", "CanUpdate")]
        public async Task<ActionResult> UpdateMemberDemographics([FromBody]MemberDemographicsBO memberDemographics)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var rows = await _demographicsService.UpdateMemberDemographics(memberDemographics, auditLogBO);
            return new JsonResult(rows);
        }

        /// <summary>
        /// Adds the dependent.
        /// </summary>
        /// <param name="primaryMember">The primary member.</param>
        /// <returns></returns>
        [Route("AddNewDependent")]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_DEPENDENT_INFORMATION", "CanCreate")]
        public async Task<(int, long)> AddDependent([FromBody]DependentDetailsBO primaryMember)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var statusCode = await _demographicsService.AddDependent(primaryMember, auditLogBO);
            return statusCode;
        }

        /// <summary>
        /// Deletes the dependent.
        /// </summary>
        /// <param name="dependentDetailId">The dependent detail identifier.</param>
        /// <returns></returns>
        [Route(nameof(DeleteDependent))]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_DEPENDENT_INFORMATION", "CanUpdate")]
        public async Task<int> DeleteDependent(long dependentDetailId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var statusCode = await _demographicsService.DeleteDependent(dependentDetailId, auditLogBO);
            return statusCode;
        }

        ///// <summary>
        ///// Gets the plan details based on user identifier.
        ///// </summary>
        ///// <param name="userId">The user identifier.</param>
        ///// <returns>json string of Member Demographics, Dependents and Coverage</returns>
        //[Route("GetPlanDetailsBasedOnUserId")]
        //[HttpGet]
        //public async Task<JsonResult> GetPlanDetailsBasedOnUserId(int userId)
        //{
        //    var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
        //    //var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
        //    var auditLogBO = new AuditLogBO();
        //    var memberPlanDetails = await _demographicsService.GetPlanDetailsBasedOnUserId(userId, auditLogBO);
        //    return new JsonResult(memberPlanDetails);
        //}

        /// <summary>
        /// Member Questionnnaires
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [Route("{type}/Questionnaires")]
        [HttpGet]
        [ClaimRequirement("roles", "MEM_DEPENDENT_INFORMATION", "CanRead")]
        public async Task<List<MemberQuestionnaireBO>> GetDependentQuestionnairesAsync(int type)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var result = await _demographicsService.GetMemberQuestionnairesAsync(type, auditLogBO);
            return result;
        }
    }
}