using Aliera.BusinessObjects.Member;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public interface IMemberSearchDataAccess
    {
        Task<List<MemberDataBO>> GetMembersBySearch(MemberSearchBO memberSearchBO);

        Task<MemberDashboardBO> GetUserDetails(long userId);
    }
}