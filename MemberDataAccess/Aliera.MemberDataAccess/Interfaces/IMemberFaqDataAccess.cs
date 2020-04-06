using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public interface IMemberFaqDataAccess
    {
        Task<IEnumerable<MemberFaqCategoryBO>> GetFaqCategories(AuditLogBO auditLogBO);

        Task<IEnumerable<MemberFaqBO>> GetFaQs(int categoryId, AuditLogBO auditLogBO);
    }
}