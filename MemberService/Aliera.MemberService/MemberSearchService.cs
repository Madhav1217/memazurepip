using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberService
{
    public class MemberSearchService : IMemberSearchService
    {
        private readonly IMemberSearchDataAccess _memberSearchDataAccess;

        public MemberSearchService(IMemberSearchDataAccess memberSearchDataAccess)
        {
            _memberSearchDataAccess = memberSearchDataAccess;
        }

        /// <summary>
        /// Gets the members by search.
        /// </summary>
        /// <param name="memberSearchBO">The member search bo.</param>
        /// <returns></returns>
        public async Task<List<MemberDataBO>> GetMembersBySearch(MemberSearchBO memberSearchBO)
        {
            if (memberSearchBO == null) throw new CustomException(nameof(MemberConstants.MemberSearchServiceGetMembersBySearchInputEmptyErrorCode));
            var response = await _memberSearchDataAccess.GetMembersBySearch(memberSearchBO);
            return response;
        }

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<MemberDashboardBO> GetUserDetails(long userId)
        {
            if (userId <= 0) throw new CustomException(nameof(MemberConstants.MemberSearchServiceGetUserDetailsInputEmptyErrorCode));
            var response = await _memberSearchDataAccess.GetUserDetails(userId);
            return response;
        }
    }
}