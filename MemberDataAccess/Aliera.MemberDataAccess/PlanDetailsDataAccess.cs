using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;

namespace Aliera.MemberDataAccess
{
    public class PlanDetailsDataAccess : IPlanDetailsDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanDetailsDataAccess(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the member product guide book.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<MemberPlanDetailsBO> GetMemberProductGuideBook(long userId, AuditLogBO auditLogBO)
        {
            var memberRepo = _unitOfWork.GetRepository<Member>();
            var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(a => a,
                predicate: b => b.UserId == userId,
                include: i => i.Include(m => m.MemberSubscription)
                .ThenInclude(m => m.MemberSubscribedPlan)
                .ThenInclude(m => m.Plan)
                .ThenInclude(m => m.Product));

            var prod = member.MemberSubscription.Select(mem => new
            {
                ProductId = mem.MemberSubscribedPlan.Select(x => x.Plan.ProductId).FirstOrDefault(),
                DocumentId = mem.MemberSubscribedPlan.Select(x => x.Plan.Product.DocumentId).FirstOrDefault(),
                Name = mem.MemberSubscribedPlan.Select(x => x.Plan.Name).FirstOrDefault()
            }).ToList();

            var documentId = prod.Select(x => x.DocumentId).First() != null ? prod.Select(x => x.DocumentId).First().Value : 0;

            var guideBooks = await _unitOfWork.GetRepository<DocumentAndForm>().GetFirstOrDefaultAsync(a => a,
                predicate: doc => documentId != 0 && doc.DocumentId == documentId);

            var memberPlanDetailsBO = new MemberPlanDetailsBO
            {
                PlanName = prod.Select(x => x.Name).FirstOrDefault(),
                ProductGuideBookId = documentId,
                ProductGuideBookPath = guideBooks?.Path
            };
            //await AuditMapper.AuditLogging(auditLogBO, userId, AuditAction.Select, null);
            return memberPlanDetailsBO;
        }
    }
}