using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Broker;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberService
{
    public interface IMemberDocumentAndFormService
    {
        Task<IEnumerable<DocumentAndFormBO>> GetDocumentAndForms(int recordsPerPage, int pageNumber, AuditLogBO auditLogBO);

        Task<IEnumerable<DocumentAndFormBO>> GetDocumentAndFormSearch(DocumentAndFormFilterBO documentAndFormFilterBO, AuditLogBO auditLogBO);

        Task<IEnumerable<string>> GetSectionList(AuditLogBO auditLogBO);
    }
}