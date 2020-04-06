using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aliera.Utilities.Enumerations;

namespace Aliera.MemberDataAccess
{
    public class AccumulatorDataAccess : IAccumulatorDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccumulatorDataAccess(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the member accumulator details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<IEnumerable<MemberAccumulatorDetailsBO>> GetMemberAccumulatorDetails(int userId, AuditLogBO auditLogBO)
        {
            var memberRepo = await _unitOfWork.GetRepository<Member>()
                .GetFirstOrDefaultAsync(i => i, predicate: m => m.UserId == userId,
                include: i => i
                .Include(a => a.MemberAccumulatorDetails)
                .ThenInclude(a => a.Accumulator)
                );

            var memberAccDetails = new List<MemberAccumulatorDetailsBO>();

            memberAccDetails.AddRange(memberRepo.MemberAccumulatorDetails.Select(memberAccumulators => new MemberAccumulatorDetailsBO
            {
                AccumulatorType = memberAccumulators.AccumulatorType,
                MaxValue = memberAccumulators.AllocatedAmount,
                UsedValue = memberAccumulators.ConsumedAmount,
                Percentage = memberAccumulators.ConsumedAmount / memberAccumulators.AllocatedAmount * 100,
                RemainingPercentage = ((memberAccumulators.AllocatedAmount - memberAccumulators.ConsumedAmount) / memberAccumulators.AllocatedAmount) * 100,
                AccumulatorName = memberAccumulators.Accumulator.Name,
                NetworkTier = memberAccumulators.NetworkTier,
                IsFamilyAccumulator = true
            }));

            //await AuditMapper.AuditLogging(auditLogBO, userId, AuditAction.Select, null);

            return memberAccDetails;
        }

        /// <summary>
        /// Gets the individual accumulator details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="dependentDetailId">The dependent detail identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<IEnumerable<MemberAccumulatorDetailsBO>> GetIndividualAccumulatorDetails(int? userId, int? dependentDetailId, AuditLogBO auditLogBO)
        {
            var member = await _unitOfWork.GetRepository<Member>()
                .GetFirstOrDefaultAsync(i => i, predicate: m => m.UserId == userId,
                include: i => i
                .Include(a => a.MemberDependent)
                .Include(a => a.MemberAccumulatorDetails)
                .ThenInclude(a => a.Accumulator)
                );

            var memberAccDetails = new List<MemberAccumulatorDetailsBO>();

            if (member != null)
            {
                memberAccDetails.AddRange(member.MemberAccumulatorDetails.Where(x => !x.IsFamilyAccumulator).Select(memberAccumulators => new MemberAccumulatorDetailsBO
                {
                    AccumulatorType = memberAccumulators.AccumulatorType,
                    MaxValue = memberAccumulators.AllocatedAmount,
                    UsedValue = memberAccumulators.ConsumedAmount,
                    Percentage = memberAccumulators.ConsumedAmount / memberAccumulators.AllocatedAmount * 100,
                    RemainingPercentage = ((memberAccumulators.AllocatedAmount - memberAccumulators.ConsumedAmount) / memberAccumulators.AllocatedAmount) * 100,
                    AccumulatorName = memberAccumulators.Accumulator.Name,
                    NetworkTier = memberAccumulators.NetworkTier,
                    IsFamilyAccumulator = false
                }));
            }
            else if (member.MemberDependent != null)
            {
                memberAccDetails.AddRange(member.MemberAccumulatorDetails.Where(x => !x.IsFamilyAccumulator).Select(memberAccumulators => new MemberAccumulatorDetailsBO
                {
                    AccumulatorType = memberAccumulators.AccumulatorType,
                    MaxValue = memberAccumulators.AllocatedAmount,
                    UsedValue = memberAccumulators.ConsumedAmount,
                    Percentage = memberAccumulators.ConsumedAmount / memberAccumulators.AllocatedAmount * 100,
                    RemainingPercentage = ((memberAccumulators.AllocatedAmount - memberAccumulators.ConsumedAmount) / memberAccumulators.AllocatedAmount) * 100,
                    AccumulatorName = memberAccumulators.Accumulator.Name,
                    NetworkTier = memberAccumulators.NetworkTier,
                    IsFamilyAccumulator = false
                }));
            }

            //await AuditMapper.AuditLogging(auditLogBO, userId, AuditAction.Select, null);
            return memberAccDetails;
        }
    }
}