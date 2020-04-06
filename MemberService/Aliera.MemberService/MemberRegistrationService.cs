using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;

namespace Aliera.MemberService
{
    public class MemberRegistrationService : IMemberRegistrationService
    {
        private readonly IMemberRegistrationDataAccess _memberRegistrationDa;

        public MemberRegistrationService(IMemberRegistrationDataAccess memberRegistrationDa)
        {
            _memberRegistrationDa = memberRegistrationDa;
        }

        /// <summary>
        /// Registers the member.
        /// </summary>
        /// <param name="memberRegistration">The member registration.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberMemberForRegistrationEmptyErrorCode
        /// or
        /// MemberStatusForMemberRegisterMemberEmptyErrorCode
        /// </exception>
        public async Task<int> RegisterMember(MemberRegistrationBO memberRegistration, AuditLogBO auditLogBO)
        {
            if (memberRegistration == null) throw new CustomException(nameof(MemberConstants.MemberMemberForRegistrationEmptyErrorCode));
            var statusCode = await _memberRegistrationDa.RegisterMember(memberRegistration, auditLogBO);
            if (statusCode == 0) throw new CustomException(nameof(MemberConstants.MemberStatusForMemberRegisterMemberEmptyErrorCode));
            return statusCode;
        }

        /// <summary>
        /// Sends the otp to member.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MemberRegistrationServiceSendOtpToMemberInputEmptyErrorCode</exception>
        public async Task<int> SendOtpToMember(MemberRegistrationBO memberInfo, AuditLogBO auditLogBO)
        {
            if (memberInfo == null)
                throw new CustomException(nameof(MemberConstants.MemberRegistrationServiceSendOtpToMemberInputEmptyErrorCode));
            return await _memberRegistrationDa.SendOtpToMember(memberInfo, auditLogBO);
        }
    }
}