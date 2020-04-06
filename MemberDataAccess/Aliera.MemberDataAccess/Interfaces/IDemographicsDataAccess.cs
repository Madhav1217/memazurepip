using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Broker;
using Aliera.BusinessObjects.Member;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public interface IDemographicsDataAccess
    {
        Task<MemberDemographicsBO> GetMemberDemographics(DemographicsPermissionBO demographicsPermissionBO);

        Task<int> UpdateMemberDemographics(MemberDemographicsBO memberDemographics, AuditLogBO auditLogBO);

        Task<(int, long)> AddDependent(DependentDetailsBO primaryMember, AuditLogBO auditLogBO);

        Task<int> DeleteDependent(long dependentDetailId, AuditLogBO auditLogBO);

        //Task<MemberPlanDetailsBO> GetPlanDetailsBasedOnUserId(int userId, AuditLogBO auditLogBO);

        Task<List<MemberQuestionnaireBO>> GetMemberQuestionnairesAsync(int type);
    }
}