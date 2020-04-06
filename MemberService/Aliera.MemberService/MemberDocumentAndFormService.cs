using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Broker;
using Aliera.MemberDataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;

namespace Aliera.MemberService
{
    public class MemberDocumentAndFormService : IMemberDocumentAndFormService
    {
        private readonly IMemberDocumentAndFormDataAccess _memberDocumentAndFormDa;

        public MemberDocumentAndFormService(IMemberDocumentAndFormDataAccess memberDocumentAndFormDa)
        {
            _memberDocumentAndFormDa = memberDocumentAndFormDa;
        }

        /// <summary>
        /// Gets the document and forms.
        /// </summary>
        /// <param name="recordsPerPage">The records per page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MemberGeneralDocumentEmptyErrorCode</exception>
        public async Task<IEnumerable<DocumentAndFormBO>> GetDocumentAndForms(int recordsPerPage, int pageNumber, AuditLogBO auditLogBO)
        {
            var response = await _memberDocumentAndFormDa.GetDocumentAndFormsOrderByFileName(recordsPerPage, pageNumber, auditLogBO);
            if (response == null) throw new CustomException(nameof(MemberConstants.MemberGeneralDocumentEmptyErrorCode));
            return response;
        }

        /// <summary>
        /// Gets the document and form search.
        /// </summary>
        /// <param name="documentAndFormFilterBO">The document and form filter bo.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberDocumentSearchFilterEmptyErrorCode
        /// or
        /// MemberDocumentSearchResultEmptyErrorCode
        /// </exception>
        public async Task<IEnumerable<DocumentAndFormBO>> GetDocumentAndFormSearch(DocumentAndFormFilterBO documentAndFormFilterBO, AuditLogBO auditLogBO)
        {
            if (documentAndFormFilterBO == null) throw new CustomException(nameof(MemberConstants.MemberDocumentSearchFilterEmptyErrorCode));
            var response = await _memberDocumentAndFormDa.GetDocumentAndFormSearch(documentAndFormFilterBO, auditLogBO);
            if (response == null) throw new CustomException(nameof(MemberConstants.MemberDocumentSearchResultEmptyErrorCode));
            return response;
        }

        /// <summary>
        /// Gets the section list.
        /// </summary>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MemberDocumentSectionListEmptyErrorCode</exception>
        public async Task<IEnumerable<string>> GetSectionList(AuditLogBO auditLogBO)
        {
            var response = await _memberDocumentAndFormDa.GetSectionList(auditLogBO);
            if (response == null) throw new CustomException(nameof(MemberConstants.MemberDocumentSectionListEmptyErrorCode));
            return response;
        }
    }
}