using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Aliera.MemberDataAccess
{
    public interface IMemberFeedbackDataAccess
    {
        Task<bool> SaveMemberFeedback(MemberFeedbackBO memberFeedbackBO, AuditLogBO auditLogBO);

        Task<IList<string>> GetMemberInquiryTypes();
    }
}