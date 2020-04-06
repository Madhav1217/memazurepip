using Aliera.AuthUtility;
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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberWorkflow.Controllers
{
    [Authorize]
    [Route("api/member/[controller]")]
    [ApiController]
    public class MemberFeedbackController : Controller
    {
        private readonly IMemberFeedbackService _memberFeedbackService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MemberFeedbackController(IMemberFeedbackService memberFeedbackService, IOptions<AppSettings> appSettings
            , IHttpContextAccessor httpContextAccessor)
        {
            _memberFeedbackService = memberFeedbackService;
            _appSettings = appSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Saves the member feedback.
        /// </summary>
        /// <param name="memberFeedbackBO">The member feedback bo.</param>
        /// <returns></returns>
        [Route("SaveFeedback")]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_CONTANCT_US", "CanRead")]
        public async Task<bool> SaveMemberFeedback(MemberFeedbackBO memberFeedbackBO)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            return await _memberFeedbackService.SaveMemberFeedback(memberFeedbackBO, auditLogBO);
        }

        [Route("MemberInquiryTypes")]
        [HttpGet]
        [ClaimRequirement("roles", "MEM_CONTANCT_US", "CanRead")]
        public async Task<IList<string>> GetMemberInquiryTypes()
        {
            return await _memberFeedbackService.GetMemberInquiryTypes();
        }
    }
}