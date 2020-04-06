using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;
using System;

namespace Aliera.MemberService
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardDataAccess _dashboardDataAccess;

        public DashboardService(IDashboardDataAccess dashboardDataAccess)
        {
            _dashboardDataAccess = dashboardDataAccess;
        }

        /// <summary>
        /// Gets the member and dependent names.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberUserIdForDashboardEmptyErrorCode
        /// or
        /// MemberNoMemberAndDependentsErrorCode
        /// </exception>
        public async Task<IEnumerable<MemberDataBO>> GetMemberAndDependentNames(long userId, AuditLogBO auditLogBO)
        {
            if (userId <= 0) throw new CustomException(nameof(MemberConstants.MemberUserIdForDashboardEmptyErrorCode));
            var response = await _dashboardDataAccess.GetMemberAndDependentNames(userId, auditLogBO);
            if (response == null) throw new CustomException(nameof(MemberConstants.MemberNoMemberAndDependentsErrorCode));
            return response;
        }

        /// <summary>
        /// Gets the member details for dash board.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <param name="leftNavPermission">The left nav permission.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberUserIdForMemberDetailsEmptyErrorCode
        /// or
        /// MemberNoMemberDetailsErrorCode
        /// </exception>
        public async Task<MemberDashboardBO> GetMemberDetailsForDashBoard(long userId, AuditLogBO auditLogBO, int leftNavPermission)
        {
            if (userId <= 0) throw new CustomException(nameof(MemberConstants.MemberUserIdForMemberDetailsEmptyErrorCode));
            var response = await _dashboardDataAccess.GetMemberDetailsForDashBoard(userId, auditLogBO, leftNavPermission);
            if (response == null) throw new CustomException(nameof(MemberConstants.MemberNoMemberDetailsErrorCode));
            return response;
        }

        /// <summary>
        /// Gets the member plan types.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">DashboardServiceGetMemberPlanTypesInputEmptyErrorCode</exception>
        public async Task<CoveredPlansBO> GetMemberPlanTypes(long userId, AuditLogBO auditLogBO)
        {
            if (userId <= 0) throw new CustomException(nameof(MemberConstants.DashboardServiceGetMemberPlanTypesInputEmptyErrorCode));
            return await _dashboardDataAccess.GetMemberPlanTypes(userId, auditLogBO);
        }

        /// <summary>
        /// Gets the security questions by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">DashboardServiceGetSecurityQuestionsByUserIdInputEmptyErrorCode</exception>
        public async Task<IEnumerable<SecurityQuestionAnswersBO>> GetSecurityQuestionsByUserId(long userId, AuditLogBO auditLogBO)
        {
            if (userId <= 0) throw new CustomException(nameof(MemberConstants.DashboardServiceGetSecurityQuestionsByUserIdInputEmptyErrorCode));
            return await _dashboardDataAccess.GetSecurityQuestionsByUserId(userId, auditLogBO);
        }

        /// <summary>
        /// Updates the security questions by user identifier.
        /// </summary>
        /// <param name="updatedSecurityQuestions">The updated security questions.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">DashboardServiceUpdateSecurityQuestionsByUserIdInputEmptyErrorCode</exception>
        public async Task<bool> UpdateSecurityQuestionsByUserId(List<SecurityQuestionAnswersBO> updatedSecurityQuestions, AuditLogBO auditLogBO)
        {
            if (updatedSecurityQuestions == null)
                throw new CustomException(nameof(MemberConstants.DashboardServiceUpdateSecurityQuestionsByUserIdInputEmptyErrorCode));
            return await _dashboardDataAccess.UpdateSecurityQuestionsByUserId(updatedSecurityQuestions, auditLogBO);
        }

        /// <summary>
        /// Determines whether [is group member] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">DashboardServiceIsGroupMemberInputEmptyErrorCode</exception>
        public async Task<Tuple<bool, int, int>> IsGroupMember(long userId, AuditLogBO auditLogBO)
        {
            if (userId <= 0) throw new CustomException(nameof(MemberConstants.DashboardServiceIsGroupMemberInputEmptyErrorCode));
            return await _dashboardDataAccess.IsGroupMember(userId, auditLogBO);
        }

        /// <summary>
        /// Gets the member plan details.
        /// </summary>
        /// <param name="memberId">The member identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberUserIdForMemberDetailsEmptyErrorCode
        /// or
        /// MemberNoMemberDetailsErrorCode
        /// </exception>
        public async Task<MemberDashboardBO> GetMemberPlanDetails(long memberId, long userId, AuditLogBO auditLogBO)
        {
            if (userId <= 0 || memberId < 0) throw new CustomException(nameof(MemberConstants.MemberUserIdForMemberDetailsEmptyErrorCode));
            var response = await _dashboardDataAccess.GetMemberPlanDetails(memberId, userId, auditLogBO);
            if (response == null) throw new CustomException(nameof(MemberConstants.MemberNoMemberDetailsErrorCode));
            return response;
        }

        /// <summary>
        /// Gets the member identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<string> GetMemberExternalId(long userId)
        {
            return await _dashboardDataAccess.GetMemberExternalId(userId);
        }
    }
}