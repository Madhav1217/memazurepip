using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;
using System;
using System.Threading.Tasks;

namespace Aliera.MemberService
{
    public class MemberVerifyService : IMemberVerifyService
    {
        private readonly IMemberVerifyDataAccess _memberVerifyDa;

        public MemberVerifyService(IMemberVerifyDataAccess memberVerifyDa)
        {
            _memberVerifyDa = memberVerifyDa;
        }

        /// <summary>
        /// Determines whether [is member valid] [the specified member reg bo].
        /// </summary>
        /// <param name="memberRegBO">The member reg bo.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public Task<Tuple<int, string, string>> IsMemberValid(MemberRegistrationBO memberRegBO, AuditLogBO auditLogBO)
        {
            if (memberRegBO == null) throw new CustomException(nameof(MemberConstants.MemberVerifyServiceIsMemberValidInputEmptyErrorCode));
            return _memberVerifyDa.IsMemberValid(memberRegBO, auditLogBO);
        }

        /// <summary>
        /// Determines whether [is username available] [the specified username].
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MemberVerifyServiceIsUsernameAvailableInputEmptyErrorCode</exception>
        public Task<bool> IsUsernameAvailable(string username, AuditLogBO auditLogBO)
        {
            if (string.IsNullOrEmpty(username)) throw new CustomException(nameof(MemberConstants.MemberVerifyServiceIsUsernameAvailableInputEmptyErrorCode));
            return _memberVerifyDa.IsUsernameAvailable(username, auditLogBO);
        }

        /// <summary>
        /// Validates the otp.
        /// </summary>
        /// <param name="externalId">The external identifier.</param>
        /// <param name="otp">The otp.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MemberFeedbackForSaveEmptyErrorCode</exception>
        public Task<bool> ValidateOtp(string externalId, int otp, AuditLogBO auditLogBO)
        {
            if (string.IsNullOrEmpty(externalId) || otp == 0) throw new CustomException(nameof(MemberConstants.MemberFeedbackForSaveEmptyErrorCode));
            return _memberVerifyDa.ValidateOtp(externalId, otp, auditLogBO);
        }
    }
}