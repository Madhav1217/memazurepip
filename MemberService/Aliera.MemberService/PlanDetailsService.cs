using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;

namespace Aliera.MemberService
{
    public class PlanDetailsService : IPlanDetailsService
    {
        private readonly IPlanDetailsDataAccess _planDetailsDataAccess;

        public PlanDetailsService(IPlanDetailsDataAccess planDetailsDataAccess)
        {
            _planDetailsDataAccess = planDetailsDataAccess;
        }

        /// <summary>
        /// Gets the member product guide book.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">PlanDetailsServiceGetMemberProductGuideBookInputEmptyErrorCode</exception>
        public async Task<MemberPlanDetailsBO> GetMemberProductGuideBook(long userId, AuditLogBO auditLogBO)
        {
            if (userId <= 0)
                throw new CustomException(nameof(MemberConstants.PlanDetailsServiceGetMemberProductGuideBookInputEmptyErrorCode));
            return await _planDetailsDataAccess.GetMemberProductGuideBook(userId, auditLogBO);
        }
    }
}