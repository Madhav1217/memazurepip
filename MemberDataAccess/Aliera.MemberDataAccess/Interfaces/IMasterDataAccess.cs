using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Broker;
using Aliera.BusinessObjects.Member;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public interface IMasterDataAccess
    {
        Task<IEnumerable<SecurityQuestionsBO>> GetSecurityQuestions(AuditLogBO auditLogBO);

        Task<IEnumerable<StateBO>> GetStates(AuditLogBO auditLogBO);

        Task<bool> UpdateDigitalDocumentId(DigitalDocumentBO digitalDoc, AuditLogBO auditLogBO);
    }
}