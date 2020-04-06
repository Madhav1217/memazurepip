using Aliera.BusinessObjects.Member;
using Aliera.MemberService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberWorkflow.Controllers
{
    [Authorize]
    [Route("api/member/[controller]")]
    [ApiController]
    public class MemberSearchController : ControllerBase
    {
        private readonly IMemberSearchService _memberSearchService;

        public MemberSearchController(IMemberSearchService memberSearchService)
        {
            _memberSearchService = memberSearchService;
        }

        /// <summary>
        /// Gets the members by search.
        /// </summary>
        /// <param name="memberSearchBO">The member search bo.</param>
        /// <returns></returns>
        [Route("GetMembers")]
        [HttpPost]
        public async Task<List<MemberDataBO>> GetMembersBySearch(MemberSearchBO memberSearchBO)
        {
            return await _memberSearchService.GetMembersBySearch(memberSearchBO);
        }

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [Route("UserDetails")]
        [HttpGet]
        public async Task<MemberDashboardBO> GetUserDetails(long userId)
        {
            return await _memberSearchService.GetUserDetails(userId);
        }
    }
}