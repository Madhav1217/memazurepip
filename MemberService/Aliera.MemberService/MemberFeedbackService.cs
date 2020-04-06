using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;
using System.Collections.Generic;

namespace Aliera.MemberService
{
    public class MemberFeedbackService : IMemberFeedbackService
    {
        private readonly IMemberFeedbackDataAccess _memberFeedbackDataAccess;

        public MemberFeedbackService(IMemberFeedbackDataAccess memberFeedbackDataAccess)
        {
            _memberFeedbackDataAccess = memberFeedbackDataAccess;
        }

        /// <summary>
        /// Saves the member feedback.
        /// </summary>
        /// <param name="memberFeedbackBO">The member feedback bo.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MemberFeedbackForSaveEmptyErrorCode</exception>
        public async Task<bool> SaveMemberFeedback(MemberFeedbackBO memberFeedbackBO, AuditLogBO auditLogBO)
        {
            if (memberFeedbackBO == null) throw new CustomException(nameof(MemberConstants.MemberFeedbackForSaveEmptyErrorCode));
            return await _memberFeedbackDataAccess.SaveMemberFeedback(memberFeedbackBO, auditLogBO);
        }

        /// <summary>
        /// Gets the member inquiry types.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<string>> GetMemberInquiryTypes()
        {
            return await _memberFeedbackDataAccess.GetMemberInquiryTypes();
        }
    }
}