using Aliera.BusinessObjects.Audit;
using Aliera.MemberService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aliera.Utilities;
using Aliera.Utilities.Constants;
using Aliera.AuthUtility;

namespace Aliera.MemberWorkflow.Controllers
{
    [Authorize]
    [Route("api/member/[Controller]")]
    [ApiController]
    public class MemberDocumentAndFormController : Controller
    {
        private readonly IMemberDocumentAndFormService _memberDocumentAndFormService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MemberDocumentAndFormController(IMemberDocumentAndFormService memberDocumentAndFormService, IOptions<AppSettings> appSettings
            , IHttpContextAccessor httpContextAccessor)
        {
            _memberDocumentAndFormService = memberDocumentAndFormService;
            _appSettings = appSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// get Document And Form Search
        /// </summary>
        /// <param name="documentAndFormFilterBO"></param>
        /// <returns></returns>
        [Route(nameof(GetDocumentAndFormSearch))]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_DOCUMENTS_AND_FORMS", "CanRead")]
        public async Task<IEnumerable<BusinessObjects.Broker.DocumentAndFormBO>> GetDocumentAndFormSearch([FromBody]BusinessObjects.Broker.DocumentAndFormFilterBO documentAndFormFilterBO)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            return await _memberDocumentAndFormService.GetDocumentAndFormSearch(documentAndFormFilterBO, auditLogBO);
        }

        /// <summary>
        /// Members the document section list.
        /// </summary>
        /// <returns></returns>
        [Route("DocumentAndFormSectionList")]
        [HttpGet]
        [ClaimRequirement("roles", "MEM_DOCUMENTS_AND_FORMS", "CanRead")]
        public async Task<IEnumerable<string>> MemberDocumentSectionList()
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            return await _memberDocumentAndFormService.GetSectionList(auditLogBO);
        }
    }
}