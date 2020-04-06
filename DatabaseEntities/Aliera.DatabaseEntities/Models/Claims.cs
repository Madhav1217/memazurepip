using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Claims
    {
        public string ClaimsNumber { get; set; }
        public string MemberExternalId { get; set; }
        public int ClaimsStatusId { get; set; }
        public int ClaimsTypeId { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public string ServiceDescription { get; set; }
        public decimal PlanDiscountedRate { get; set; }
        public decimal BilledAmount { get; set; }
        public decimal PlanPaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal Msraamount { get; set; }
        public string FacilityName { get; set; }
        public long? DocumentId { get; set; }
        public DateTime ProcessingDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime ServiceEndDate { get; set; }
        public long ClaimId { get; set; }
        public string Admin123Id { get; set; }

        public virtual ClaimsStatus ClaimsStatus { get; set; }
    }
}
