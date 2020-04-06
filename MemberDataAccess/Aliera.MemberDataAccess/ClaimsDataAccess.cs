using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aliera.Utilities;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;
using Aliera.Utilities.Helpers;

namespace Aliera.MemberDataAccess
{
    public class ClaimsDataAccess : IClaimsDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<AppSettings> _appSettings;

        public ClaimsDataAccess(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Gets the claims details.
        /// </summary>
        /// <param name="claimsRequestBO">The claims request bo.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ClaimsBO>> GetClaimsDetails(ClaimsFilterBO claimsRequestBO, AuditLogBO auditLogBO)
        {
            var fromDate = new DateTime(DateTime.UtcNow.Year, 01, 01);
            var toDate = DateTime.UtcNow;
            var returnList = new List<ClaimsBO>();
            string memberExternalId;
            switch (claimsRequestBO.FilterAttribute.ToLower())
            {
                case MemberConstants.LastThreeMonthsFilter:
                    fromDate = DateTime.UtcNow.AddMonths(-3);
                    break;

                case MemberConstants.LastSixMonthsFilter:
                    fromDate = DateTime.UtcNow.AddMonths(-6);
                    break;

                case MemberConstants.LastTweleveMonthsFilter:
                    fromDate = DateTime.UtcNow.AddMonths(-12);
                    break;

                default:
                    break;
            }

            if (claimsRequestBO != null)
            {
                if (String.IsNullOrEmpty(claimsRequestBO.ExternalMemberId))
                {
                    var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(a => a, predicate: b => b.UserId == claimsRequestBO.userId);
                    memberExternalId = member?.ExternalId;
                }
                else
                    memberExternalId = claimsRequestBO.ExternalMemberId;

                var primaryMemberclaims = await _unitOfWork.GetRepository<Claims>().GetPagedListAsync(a => a,
                     predicate: b => b.MemberExternalId == memberExternalId
                     && b.ServiceStartDate >= fromDate
                     && b.ServiceStartDate <= toDate
                     && (claimsRequestBO.ClaimsType > (int)ClaimType.All && b.ClaimsTypeId == claimsRequestBO.ClaimsType || claimsRequestBO.ClaimsType == (int)ClaimType.All && b.ClaimsTypeId > 0),
                     orderBy: GenericHelper.GetOrderBy<Claims>(claimsRequestBO.SortAttribute, claimsRequestBO.SortOrder),
                     pageIndex: claimsRequestBO.PageNumber, pageSize: claimsRequestBO.PageSize);

                returnList = await FetchClaimsData(primaryMemberclaims, primaryMemberclaims.Items.Count);

                //await AuditMapper.AuditLogging(auditLogBO, claimsRequestBO.userId, AuditAction.Select, claimsRequestBO);
            }
            return returnList;
        }

        /// <summary>
        /// Fetches the claims data.
        /// </summary>
        /// <param name="claims">The claims.</param>
        /// <param name="totalRecords">The total records.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Unexpected Case</exception>
        private async Task<List<ClaimsBO>> FetchClaimsData(IPagedList<Claims> claims, int totalRecords)
        {
            var claimsStatus = await _unitOfWork.GetRepository<ClaimsStatus>().GetPagedListAsync(cs => cs,
                pageIndex: 0, pageSize: 5);

            var claimsBO = claims.Items.Join(claimsStatus.Items,
                claim => claim.ClaimsStatusId, cs => cs.Id, (claim, cs) => new ClaimsBO
                {
                    ClaimsNumber = claim.ClaimsNumber,
                    MemberExternalID = claim.MemberExternalId,
                    ClaimTypeID = claim.ClaimsTypeId,
                    ClaimsStatusID = claim.ClaimsStatusId,
                    ServiceStartDate = claim.ServiceStartDate,
                    ServiceEndDate = claim.ServiceEndDate,
                    ServiceDescription = claim.ServiceDescription,
                    MSRAAmount = claim.Msraamount,
                    DueAmount = claim.DueAmount,
                    BilledAmount = claim.BilledAmount,
                    PlanPaidAmount = claim.PlanPaidAmount,
                    PlanDiscountedRate = claim.PlanDiscountedRate,
                    FacilityName = claim.FacilityName,
                    EOBIdentifier = claim.DocumentId,
                    ProcessedDate = claim.ProcessingDate,
                    ClaimsStatusDescription = cs.Description,
                    ClaimsTypeDescription = Convert.ToString(claim.ClaimsTypeId),
                    TotalNoOfItems = totalRecords
                }).ToList();

            return claimsBO;
        }
    }
}