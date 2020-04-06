using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Broker;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public interface IMemberDocumentAndFormDataAccess
    {
        Task<IEnumerable<DocumentAndFormBO>> GetDocumentAndFormsOrderByFileName(int recordsPerPage, int pageNumber, AuditLogBO auditLogBO);

        Task<IEnumerable<DocumentAndFormBO>> GetDocumentAndFormSearch(DocumentAndFormFilterBO documentAndFormFilterBO, AuditLogBO auditLogBO);

        Task<IEnumerable<string>> GetSectionList(AuditLogBO auditLogBO);
    }
}