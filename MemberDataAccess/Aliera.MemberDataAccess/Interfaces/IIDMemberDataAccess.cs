using Aliera.BusinessObjects.Member;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public interface IIDMemberDataAccess
    {
        Task<IDCardBO> GetIDMemberDetails(long userId, int productCode, string productLabel);

        Task<bool> SendPhysicalIdCardRequestEmail(long memberId, long memberDetailId, string memberType);
    }
}