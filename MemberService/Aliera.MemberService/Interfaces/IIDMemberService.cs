using Aliera.BusinessObjects.Member;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberService
{
    public interface IIDMemberService
    {
        Task<byte[]> GetIDMemberDetails(long userId, int productCode, string productLabel);

        Task<bool> SendPhysicalIdCardRequestEmail(long memberId, long memberDetailId, string memberType);
    }
}