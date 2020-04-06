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
    public class IdCardController : ControllerBase
    {
        private readonly IIDMemberService _idService;

        public IdCardController(IIDMemberService idService)
        {
            _idService = idService;
        }

        /// <summary>
        /// GetIDMemberDetails
        /// </summary>
        /// <param name="externalId"></param>
        /// <returns></returns>
        [Route(nameof(GetIDMemberDetails))]
        [HttpGet]
        public async Task<IActionResult> GetIDMemberDetails(long userId, int productCode, string productLabel)
        {
            var fileBytes = await _idService.GetIDMemberDetails(userId, productCode, productLabel);
            Stream stream = new MemoryStream(fileBytes);
            stream.Position = 0;
            return File(stream, "application/octet-stream", "IdCard");
        }

        /// <summary>
        /// Sends the physical identifier card request email.
        /// </summary>
        /// <param name="memberId">The member identifier.</param>
        /// <param name="memberDetailId">The member detail identifier.</param>
        /// <param name="memberType">Type of the member.</param>
        /// <returns></returns>
        [Route("RequestForPhysicalIdCard")]
        [HttpGet]
        public async Task<bool> SendPhysicalIdCardRequestEmail(long memberId, long memberDetailId, string memberType)
        {
            return await _idService.SendPhysicalIdCardRequestEmail(memberId, memberDetailId, memberType);
        }
    }
}