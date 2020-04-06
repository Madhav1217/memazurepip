using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Broker;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;
using Portals = Aliera.Utilities.Enumerations.Portals;
using System;

namespace Aliera.MemberDataAccess
{
    public class MemberDocumentAndFormDataAccess : IMemberDocumentAndFormDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberDocumentAndFormDataAccess(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the name of the document and forms order by file.
        /// </summary>
        /// <param name="recordsPerPage">The records per page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<IEnumerable<DocumentAndFormBO>> GetDocumentAndFormsOrderByFileName(int recordsPerPage, int pageNumber, AuditLogBO auditLogBO)
        {
            var documentAndFormBOPagesItems = MemberMapper.Map(await GetDocumentAndForms());
            var documentAndFormBOs = documentAndFormBOPagesItems.Where(x => x.PortalId == (int)Portals.MemberPortal).ToList();
            documentAndFormBOs = documentAndFormBOs.OrderByDescending(a => a.LastUpdatedOn).ToList();

            if (recordsPerPage > 0)
            {
                var start = (pageNumber - 1) * recordsPerPage;
                documentAndFormBOs = documentAndFormBOs.Skip(start).Take(recordsPerPage).ToList();
            }
            //await AuditMapper.AuditLogging(auditLogBO, null, AuditAction.Select, null);
            return documentAndFormBOs;
        }

        /// <summary>
        /// Gets the document and form search.
        /// </summary>
        /// <param name="documentAndFormFilterBO">The document and form filter bo.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<DocumentAndFormBO>> GetDocumentAndFormSearch(DocumentAndFormFilterBO documentAndFormFilterBO, AuditLogBO auditLogBO)
        {
            var documentAndFormBOs = MemberMapper.Map(await GetDocumentAndForms());

            documentAndFormFilterBO.SearchText = string.IsNullOrEmpty(documentAndFormFilterBO.SearchText) ? string.Empty : documentAndFormFilterBO.SearchText;
            documentAndFormBOs = documentAndFormBOs.Where(x => x.DocumentName.Trim().ToLower().Contains(documentAndFormFilterBO.SearchText.ToLower())).ToList();

            switch (documentAndFormFilterBO.SortColumn.ToLower())
            {
                case BrokerConstants.FILE_NAME:
                    documentAndFormBOs = documentAndFormFilterBO.IsSortByDesc
                        ? documentAndFormBOs.OrderByDescending(x => x.DocumentName).ThenByDescending(x => x.LastUpdatedOn).ToList()
                        : documentAndFormBOs.OrderBy(x => x.DocumentName).ThenByDescending(x => x.LastUpdatedOn).ToList();
                    break;

                case BrokerConstants.CATEGORY_LOWER:
                    documentAndFormBOs = documentAndFormFilterBO.IsSortByDesc
                        ? documentAndFormBOs.OrderByDescending(x => x.CategoryName).ThenByDescending(x => x.LastUpdatedOn).ToList()
                        : documentAndFormBOs.OrderBy(x => x.CategoryName).ThenByDescending(x => x.LastUpdatedOn).ToList();
                    break;

                case BrokerConstants.LAST_UPDATED_ON:
                    documentAndFormBOs = documentAndFormFilterBO.IsSortByDesc
                        ? documentAndFormBOs.OrderByDescending(x => x.LastUpdatedOn).ToList()
                        : documentAndFormBOs.OrderBy(x => x.LastUpdatedOn).ToList();
                    break;

                default:
                    documentAndFormBOs = documentAndFormBOs.OrderByDescending(a => a.LastUpdatedOn).ToList();
                    break;
            }

            if (documentAndFormFilterBO.RecordsPerPage > 0)
            {
                var pageStartCount = (documentAndFormFilterBO.PageNumber - 1) * documentAndFormFilterBO.RecordsPerPage;
                documentAndFormBOs = documentAndFormBOs.Skip(pageStartCount).Take(documentAndFormFilterBO.RecordsPerPage).ToList();
            }
            //await AuditMapper.AuditLogging(auditLogBO, null, AuditAction.Select, null);
            return documentAndFormBOs;
        }

        /// <summary>
        /// Gets the section list.
        /// </summary>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetSectionList(AuditLogBO auditLogBO)
        {
            var documentAndFormBOPagesItems = await _unitOfWork.GetRepository<DocumentCategory>().GetPagedListAsync(a => a,
                predicate: dc => dc.DocumentCategoryType.Equals((int)DocumentCategoryType.MemberDocumentAndForm));
            var documentSections = documentAndFormBOPagesItems.Items
                .OrderBy(a => a.DocumentCategoryName)
                .Select(a => a.DocumentCategoryName).Distinct().ToList();

            //await AuditMapper.AuditLogging(auditLogBO, null, AuditAction.Select, null);
            return documentSections;
        }

        /// <summary>
        /// Gets the document and forms.
        /// </summary>
        /// <returns></returns>
        public async Task<IPagedList<DocumentAndForm>> GetDocumentAndForms()
        {
            var documentAndForms = await _unitOfWork.GetRepository<DocumentAndForm>().GetPagedListAsync(a => a,
                predicate: d => d.PortalId.Equals((int)Portals.MemberPortal)
                           && d.DocumentCategory.DocumentCategoryType.Equals((int)DocumentCategoryType.MemberDocumentAndForm),
                pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE,
                include: i => i.Include(df => df.DocumentCategory));
            return documentAndForms;
        }
    }
}