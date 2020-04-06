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
using Aliera.Utilities.RHIntegration;

namespace Aliera.MemberWorkflow.Controllers
{
    [Authorize]
    [Route("api/member/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor method for PaymentController
        /// </summary>
        /// <param name="paymentService,appSettings,httpContextAccessor"></param>
        public PaymentController(IPaymentService paymentService,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            _paymentService = paymentService;
            _appSettings = appSettings;
            _httpContextAccessor = httpContextAccessor;
        }
        
        /// <summary>
        /// Gets Member Demographics for a given member user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>json string of Member Demographics, Dependents and Coverage</returns>
        [Route("PaymentInformationForDemo")]
        [HttpGet]
        [ClaimRequirement("roles", "MEM_CHANGE_PAYMENT_METHOD", "CanRead")]
        public async Task<JsonResult> GetPaymentInformation(int userId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var paymentDetails = await _paymentService.GetPaymentInformation(userId, auditLogBO);
            return new JsonResult(paymentDetails);
        }

        /// <summary>
        /// Updates Payment Information for logged in member
        /// </summary>
        /// <param name="paymentInformation"></param>
        /// <returns>row of updated Primary Member Demographics</returns>
        [Route(nameof(UpdatePaymentInformation))]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_CHANGE_PAYMENT_METHOD", "CanUpdate")]
        public async Task<int> UpdatePaymentInformation([FromBody]PaymentInformationBO paymentInformation)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var statusCode = await _paymentService.UpdatePaymentInformation(paymentInformation, auditLogBO);
            return statusCode;
        }

        /// <summary>
        /// Get Payment Receipt Details of Member
        /// </summary>
        /// <param name="paymentReceiptFilterBO"></param>
        /// <returns>row of updated Primary Member Demographics</returns>
        [Route(nameof(GetPaymentReceiptDetails))]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_VIEW_PAYMENT_RECEIPTS", "CanRead")]
        public async Task<JsonResult> GetPaymentReceiptDetails([FromBody]PaymentReceiptFilterBO paymentReceiptFilterBO)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var paymentReceiptDetails = await _paymentService.GetPaymentReceiptDetails(paymentReceiptFilterBO, auditLogBO);
            return new JsonResult(paymentReceiptDetails);
        }

        /// <summary>
        /// Updates Payment Information for logged in member in RH
        /// </summary>
        /// <param name="paymentInformation"></param>
        /// <returns>row of updated Primary Member Demographics</returns>
        [Route(nameof(UpdateRHPaymentInformation))]
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> UpdateRHPaymentInformation([FromBody]PaymentInformationBO paymentInformation)
        {
             var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var status = await _paymentService.UpdateRHPaymentInformation(paymentInformation, auditLogBO);

            return new JsonResult(status);
        }

        [Route("RHStep2Response")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<RHCompleteTransactionResponse> RHStep2Response()
        {
            var tokenId = this.HttpContext.Request.QueryString.Value.Split('=')[1];
            var token = this.HttpContext.Request.QueryString.Value[0];
            var step2Response = await _paymentService.CompleteRHPayment(tokenId);

            return step2Response;
        }
    }
}