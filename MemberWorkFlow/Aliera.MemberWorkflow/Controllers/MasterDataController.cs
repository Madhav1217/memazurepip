using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberService;
using Aliera.Utilities;
using Aliera.Utilities.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Aliera.MemberWorkflow.Controllers
{
    [Authorize]
    [Route("api/member/[controller]")]
    [ApiController]
    public class MasterDataController : Controller
    {
        private readonly IMasterService _masterService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor method for MasterDataController
        /// </summary>
        /// <param name="masterService"></param>
        /// <param name="appSettings"></param>
        /// <param name="httpContextAccessor"></param>
        public MasterDataController(IMasterService masterService, IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            _masterService = masterService;
            _appSettings = appSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// get all security questions
        /// </summary>
        /// <returns>List of Security Question</returns>
        [Route("SecurityQuestions")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSecurityQuestions()
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO();
            var securityQuestions = await _masterService.GetSecurityQuestions(auditLogBO);
            return Ok(securityQuestions);
        }

        /// <summary>
        /// GetStates
        /// </summary>
        /// <returns>List of States</returns>
        [Route("States")]
        [HttpGet]
        public async Task<IActionResult> GetStates()
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var states = await _masterService.GetStates(auditLogBO);
            return Ok(states);
        }

        /// <summary>
        /// Updates the digital document identifier.
        /// </summary>
        /// <param name="digitalDoc">The digital document.</param>
        /// <returns></returns>
        [Route(nameof(UpdateDigitalDocumentId))]
        [HttpPost]
        public async Task<bool> UpdateDigitalDocumentId(DigitalDocumentBO digitalDoc)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            return await _masterService.UpdateDigitalDocumentId(digitalDoc, auditLogBO);
        }
    }
}