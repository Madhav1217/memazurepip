using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;

namespace Aliera.MemberService
{
    public class ClaimsService : IClaimsService
    {
        private readonly IClaimsDataAccess _iClaimsDataAccess;

        public ClaimsService(IClaimsDataAccess claimsDataAccess)
        {
            _iClaimsDataAccess = claimsDataAccess;
        }

        /// <summary>
        /// Gets the claims details.
        /// </summary>
        /// <param name="claimsRequestBO">The claims request bo.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// MemberClaimsRequestObjectEmptyErrorCode
        /// or
        /// MemberClaimsDataNotFoundErrorCode
        /// </exception>
        public async Task<IEnumerable<ClaimsBO>> GetClaimsDetails(ClaimsFilterBO claimsRequestBO, AuditLogBO auditLogBO)
        {
            if (claimsRequestBO == null) throw new CustomException(nameof(MemberConstants.MemberClaimsRequestObjectEmptyErrorCode));
            var response = await _iClaimsDataAccess.GetClaimsDetails(claimsRequestBO, auditLogBO);
            if (response == null) throw new CustomException(nameof(MemberConstants.MemberClaimsDataNotFoundErrorCode));
            return response;
        }

        /// <summary>
        /// Gets the eob PDF link.
        /// </summary>
        /// <param name="eobIdentifier">The eob identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MemberClaimsEobIdentifiedEmptyErrorCode</exception>
        public string GetEobPdfLink(string eobIdentifier, AuditLogBO auditLogBO)
        {
            if (eobIdentifier == string.Empty) throw new CustomException(nameof(MemberConstants.MemberClaimsEobIdentifiedEmptyErrorCode));
            // pdf link will be return from correspondence api, as of we are going to hard code the pdf link
            var pdfUrl = "https://appriver3651017519.sharepoint.com/sites/HealthEdgePortalDevelopment/Shared%20Documents/Forms/AllItems.aspx?id=%2Fsites%2FHealthEdgePortalDevelopment%2FShared%20Documents%2FMock-Ups%2FMember%20Portal%2FMemberId_ClaimID%2Epdf&parent=%2Fsites%2FHealthEdgePortalDevelopment%2FShared%20Documents%2FMock-Ups%2FMember%20Portal";
            return pdfUrl;
        }
    }
}