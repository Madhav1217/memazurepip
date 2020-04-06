using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;

namespace Aliera.MemberService
{
    public class AccumulatorService : IAccumulatorService
    {
        private readonly IAccumulatorDataAccess _accumulatorDataAccess;

        public AccumulatorService(IAccumulatorDataAccess accumulatorDataAccess)
        {
            _accumulatorDataAccess = accumulatorDataAccess;
        }

        /// <summary>
        /// Gets the member accumulator details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">AccumulatorServiceGetMemberAccumulatorDetailsInputEmptyErrorCode</exception>
        public async Task<IEnumerable<MemberAccumulatorDetailsBO>> GetMemberAccumulatorDetails(int userId, AuditLogBO auditLogBO)
        {
            if (userId == 0) throw new CustomException(nameof(MemberConstants.AccumulatorServiceGetMemberAccumulatorDetailsInputEmptyErrorCode));
            return await _accumulatorDataAccess.GetMemberAccumulatorDetails(userId, auditLogBO);
        }

        /// <summary>
        /// Gets the individual accumulator details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="dependentDetailId">The dependent detail identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">AccumulatorServiceGetIndividualAccumulatorDetailsInputEmptyErrorCode</exception>
        public async Task<IEnumerable<MemberAccumulatorDetailsBO>> GetIndividualAccumulatorDetails(int? userId, int? dependentDetailId, AuditLogBO auditLogBO)
        {
            if (userId == 0 && dependentDetailId == 0)
                throw new CustomException(nameof(MemberConstants.AccumulatorServiceGetIndividualAccumulatorDetailsInputEmptyErrorCode));
            return await _accumulatorDataAccess.GetIndividualAccumulatorDetails(userId, dependentDetailId, auditLogBO);
        }
    }
}