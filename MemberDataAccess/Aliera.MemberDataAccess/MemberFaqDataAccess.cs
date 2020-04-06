using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;
using Portals = Aliera.Utilities.Enumerations.Portals;

namespace Aliera.MemberDataAccess
{
    public class MemberFaqDataAccess : IMemberFaqDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberFaqDataAccess(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the faqs.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<IEnumerable<MemberFaqBO>> GetFaQs(int categoryId, AuditLogBO auditLogBO)
        {
            var memFaqRepository = _unitOfWork.GetRepository<Faq>();
            //await AuditMapper.AuditLogging(auditLogBO, categoryId, AuditAction.Select, null);
            if (categoryId != 0)
            {
                var memFaqs = await memFaqRepository.GetPagedListAsync(a => a,
                    s => s.FaqcategoryId == categoryId && s.PortalId == (int)Portals.MemberPortal,
                    pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);
                return MemberMapper.Map(memFaqs).OrderBy(s => s.Faqid).ToList();
            }
            else
            {
                var memFaqs = await memFaqRepository.GetPagedListAsync(a => a,
                    s => s.PortalId == (int)Portals.MemberPortal,
                    pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);
                return MemberMapper.Map(memFaqs).OrderBy(s => s.Faqid).ToList();
            }
        }

        /// <summary>
        /// Gets the FAQ categories.
        /// </summary>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<IEnumerable<MemberFaqCategoryBO>> GetFaqCategories(AuditLogBO auditLogBO)
        {
            var memFaqCategoryRepository = _unitOfWork.GetRepository<Faqcategory>();
            var memFaqCategories = await memFaqCategoryRepository.GetPagedListAsync(a => a,
                s => s.PortalId == (int)Portals.MemberPortal,
                pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);
            //await AuditMapper.AuditLogging(auditLogBO, null, AuditAction.Select, null);
            return MemberMapper.Map(memFaqCategories).OrderBy(s => s.CatgId).ToList();
        }
    }
}