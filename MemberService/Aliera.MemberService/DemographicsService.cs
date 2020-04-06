using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;
using System.Collections.Generic;
using Aliera.BusinessObjects.Broker;

namespace Aliera.MemberService
{
    public class DemographicsService : IDemographicsService
    {
        private readonly IDemographicsDataAccess _demographicsDataAccess;

        public DemographicsService(IDemographicsDataAccess demographicsDataAccess)
        {
            _demographicsDataAccess = demographicsDataAccess;
        }

        /// <summary>
        /// Gets the member demographics.
        /// </summary>
        /// <param name="demographicsPermissionBO">The demographics permission bo.</param>
        /// <returns></returns>
        public Task<MemberDemographicsBO> GetMemberDemographics(DemographicsPermissionBO demographicsPermissionBO)
        {
            if (demographicsPermissionBO.UserId <= 0) throw new CustomException(nameof(MemberConstants.MemberUserIdForMemberDemographicsEmptyErrorCode));
            var memberDemographics = _demographicsDataAccess.GetMemberDemographics(demographicsPermissionBO);
            if (memberDemographics == null) throw new CustomException(nameof(MemberConstants.MemberMemberDemographicsEmptyErrorCode));
            return memberDemographics;
        }

        /// <summary>
        /// Updates the member demographics.
        /// </summary>
        /// <param name="memberDemographics">The member demographics.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberDemographicsForBrokerDemographicsUpdateEmptyErrorCode
        /// or
        /// MemberUpdateBrokerDemographicsEmptyErrorCode
        /// </exception>
        public Task<int> UpdateMemberDemographics(MemberDemographicsBO memberDemographics, AuditLogBO auditLogBO)
        {
            if (memberDemographics == null) throw new CustomException(nameof(MemberConstants.MemberDemographicsForBrokerDemographicsUpdateEmptyErrorCode));
            var rows = _demographicsDataAccess.UpdateMemberDemographics(memberDemographics, auditLogBO);
            if (rows == null) throw new CustomException(nameof(MemberConstants.MemberUpdateBrokerDemographicsEmptyErrorCode));
            return rows;
        }

        /// <summary>
        /// Adds the dependent.
        /// </summary>
        /// <param name="primaryMember">The primary member.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberPrimaryMemberForAddDependentEmptyErrorCode
        /// or
        /// MemberAddDependentStatusEmptyErrorCode
        /// </exception>
        public async Task<(int, long)> AddDependent(DependentDetailsBO primaryMember, AuditLogBO auditLogBO)
        {
            if (primaryMember == null) throw new CustomException(nameof(MemberConstants.MemberPrimaryMemberForAddDependentEmptyErrorCode));
            var statusCode = await _demographicsDataAccess.AddDependent(primaryMember, auditLogBO);
            if (statusCode.Item1 == 0) throw new CustomException(nameof(MemberConstants.MemberAddDependentStatusEmptyErrorCode));
            return statusCode;
        }

        /// <summary>
        /// Deletes the dependent.
        /// </summary>
        /// <param name="dependentDetailId">The dependent detail identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberDependentDetailIdForDeleteDependentEmptyErrorCode
        /// or
        /// MemberDeleteDependentStatusEmptyErrorCode
        /// </exception>
        public async Task<int> DeleteDependent(long dependentDetailId, AuditLogBO auditLogBO)
        {
            if (dependentDetailId <= 0) throw new CustomException(nameof(MemberConstants.MemberDependentDetailIdForDeleteDependentEmptyErrorCode));
            var statusCode = await _demographicsDataAccess.DeleteDependent(dependentDetailId, auditLogBO);
            if (statusCode == 0) throw new CustomException(nameof(MemberConstants.MemberDeleteDependentStatusEmptyErrorCode));
            return statusCode;
        }

        /// <summary>
        /// GetMemberQuestionnairesAsync
        /// </summary>
        /// <param name="type"></param>
        /// <param name="auditLogBO"></param>
        /// <returns></returns>
        public async Task<List<MemberQuestionnaireBO>> GetMemberQuestionnairesAsync(int type, AuditLogBO auditLogBO)
        {
            if (type <= 0) throw new CustomException(nameof(MemberConstants.MemberUserIdForMemberDemographicsEmptyErrorCode));
            var memberQuestionnaire = await _demographicsDataAccess.GetMemberQuestionnairesAsync(type);
            if (memberQuestionnaire == null) throw new CustomException(nameof(MemberConstants.MemberMemberDemographicsEmptyErrorCode));
            return memberQuestionnaire;
        }
    }
}