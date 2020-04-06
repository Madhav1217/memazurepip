using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Aliera.MemberService
{
    public interface IMemberFeedbackService
    {
        Task<bool> SaveMemberFeedback(MemberFeedbackBO memberFeedbackBO, AuditLogBO auditLogBO);

        Task<IList<string>> GetMemberInquiryTypes();
    }
}