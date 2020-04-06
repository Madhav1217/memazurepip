using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;

namespace Aliera.MemberService
{
    public class MemberFaqService : IMemberFaqService
    {
        private readonly IMemberFaqDataAccess _memberFaqDa;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberFaqService"/> class.
        /// </summary>
        /// <param name="memberFaqDa">The member FAQ da.</param>
        public MemberFaqService(IMemberFaqDataAccess memberFaqDa)
        {
            _memberFaqDa = memberFaqDa;
        }

        /// <summary>
        /// Gets the FAQ categories.
        /// </summary>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MemberFAQCategoryListEmptyErrorCode</exception>
        public async Task<IEnumerable<MemberFaqCategoryBO>> GetFaqCategories(AuditLogBO auditLogBO)
        {
            var response = await _memberFaqDa.GetFaqCategories(auditLogBO);
            if (response == null) throw new CustomException(nameof(MemberConstants.MemberFAQCategoryListEmptyErrorCode));
            return response;
        }

        /// <summary>
        /// Gets the fa qs.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberFAQCategorySearchIdEmptyErrorCode
        /// or
        /// MemberFAQListEmptyErrorCode
        /// </exception>
        public async Task<IEnumerable<MemberFaqBO>> GetFaQs(int categoryId, AuditLogBO auditLogBO)
        {
            if (categoryId <= 0) throw new CustomException(nameof(MemberConstants.MemberFAQCategorySearchIdEmptyErrorCode));
            var response = await _memberFaqDa.GetFaQs(categoryId, auditLogBO);
            if (response == null) throw new CustomException(nameof(MemberConstants.MemberFAQListEmptyErrorCode));
            return response;
        }
    }
}