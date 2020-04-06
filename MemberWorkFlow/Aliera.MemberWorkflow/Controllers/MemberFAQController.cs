using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
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
    public class MemberFAQController : Controller
    {
        private readonly IMemberFaqService _memberFaqService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MemberFAQController(IMemberFaqService memberFaqService, IOptions<AppSettings> appSettings
            , IHttpContextAccessor httpContextAccessor)
        {
            _memberFaqService = memberFaqService;
            _appSettings = appSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Members the FAQ categories list.
        /// </summary>
        /// <returns></returns>
        [Route("FAQCategoryList")]
        [HttpGet]
        [ClaimRequirement("roles", "MEM_FAQ", "CanRead")]
        public async Task<IEnumerable<MemberFaqCategoryBO>> MemberFAQCategoriesList()
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            return await _memberFaqService.GetFaqCategories(auditLogBO);
        }

        /// <summary>
        /// Members the fa qs list.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        [Route("FAQs/{categoryId}")]
        [HttpGet]
        [ClaimRequirement("roles", "MEM_FAQ", "CanRead")]
        public async Task<IEnumerable<MemberFaqBO>> MemberFAQsList([FromRoute]int categoryId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            return await _memberFaqService.GetFaQs(categoryId, auditLogBO);
        }
    }
}