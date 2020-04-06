using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class ClaimsBO
    {
        public long ClaimId { get; set; }
        public string ClaimsNumber { get; set; }
        public string MemberExternalID { get; set; }
        public int ClaimTypeID { get; set; }
        public int ClaimsStatusID { get; set; }
        public string ClaimsTypeDescription { get; set; }
        public string ClaimsStatusDescription { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public DateTime ServiceEndDate { get; set; }
        public string ServiceDescription { get; set; }
        public decimal PlanDiscountedRate { get; set; }
        public decimal PlanPaidAmount { get; set; }
        public decimal BilledAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal MSRAAmount { get; set; }
        public string FacilityName { get; set; }
        public long? EOBIdentifier { get; set; }
        public DateTime ProcessedDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }
        public int TotalNoOfItems { get; set; }
        public string Admin123Id { get; set; }

    }
}
