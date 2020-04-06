using Aliera.BusinessObjects.Member;
using Aliera.MemberService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Aliera.MemberWorkflow.Controllers
{
    [Route("api/member/[controller]")]
    [ApiController]
    public class PaymentReceiptController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentReceiptController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// GetMemberPaymentTemplateDetails
        /// </summary>
        /// <param name="externalId"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>       
        [Route(nameof(GetMemberPaymentTemplateDetails))]
        [HttpGet]
        public async Task<IActionResult> GetMemberPaymentTemplateDetails(long userId, string transactionId)
        {
            var fileBytes = await _paymentService.GetMemberPaymentTemplateDetails(userId, transactionId);
            Stream stream = new MemoryStream(fileBytes);
            stream.Position = 0;
            return File(stream, "application/octet-stream", "PaymentReceipt");
        }

        ///// <summary>
        ///// Updates the document identifier.
        ///// </summary>
        ///// <param name="paymentReceipt">The payment receipt.</param>
        ///// <returns></returns>
        //[Route("UpdateDocumentId")]
        //[HttpPost]
        //public async Task<bool> UpdateDocumentId(PaymentReceiptTemplateBO paymentReceipt)
        //{
        //    return await _paymentService.UpdateDocumentId(paymentReceipt);
        //}
    }
}