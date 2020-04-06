using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberService;
using Aliera.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;

namespace Aliera.MemberWorkflow.Controllers
{
    [Route("api/member/[Controller]")]
    [ApiController]
    public class MemberRegistrationController : Controller
    {
        private readonly IMemberRegistrationService _memberRegistrationService;
        private readonly IMemberVerifyService _memberVerifyService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor method for MemberRegistrationController
        /// </summary>
        /// <param name="memberRegistrationService"></param>
        public MemberRegistrationController(IMemberRegistrationService memberRegistrationService, IMemberVerifyService memberVerifyService,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            _memberRegistrationService = memberRegistrationService;
            _memberVerifyService = memberVerifyService;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
        }

        [Route("Registration")]
        [HttpPost]
        public async Task<int> RegisterMember([FromBody] MemberRegistrationBO memberRegistration)
        {
            var auditLogBO = new AuditLogBO();
            var statusCode = await _memberRegistrationService.RegisterMember(memberRegistration, auditLogBO);
            return statusCode;
        }

        /// <summary>
        /// get Broker Information
        /// </summary>
        /// <param name="memberRegBO"></param>
        /// <returns>Returns True if memberid and date of birth is valid</returns>
        [Route("IsValid")]
        [HttpPost]
        public async Task<Tuple<int, string, string>> ValidateMember([FromBody]MemberRegistrationBO memberRegBO)
        {
            var auditLogBO = new AuditLogBO();
            var response = await _memberVerifyService.IsMemberValid(memberRegBO, auditLogBO);
            return response;
        }

        /// <summary>
        /// To send otp to member for registration.
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        [Route("SendOtpForRegistration")]
        [HttpPost]
        public async Task<int> SendOtpToMember(MemberRegistrationBO memberInfo)
        {
            var auditLogBO = new AuditLogBO();
            var response = await _memberRegistrationService.SendOtpToMember(memberInfo, auditLogBO);
            return response;
        }

        /// <summary>
        /// To validate member otp
        /// </summary>
        /// <param name="externalId"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        [Route("IsValidOtp")]
        [HttpPost]
        public async Task<bool> ValidateOtp(string externalId, int otp)
        {
            var auditLogBO = new AuditLogBO();
            var response = await _memberVerifyService.ValidateOtp(externalId, otp, auditLogBO);
            return response;
        }
    }
}