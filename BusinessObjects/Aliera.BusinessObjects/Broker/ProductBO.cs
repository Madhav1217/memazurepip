using System;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class ProductBO
    {
        public ProductBO()
        {
            MigratedMemberProductDetails = new MigratedMemberProductDetailsBO();
        }
        public long MemberId { get; set; }
        public int PlanId { get; set; }
        public int ProductId { get; set; }
        public string Label { get; set; }
        public decimal? MSRA { get; set; }
        public decimal? ApplicationFee { get; set; }
        public decimal? MonthlyFee { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? BillingDate { get; set; }
        public DateTime? FullfillmentDate { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? InactiveDate { get; set; }
        public MemberStatus Status { get; set; }
        public string FamilyIndicator { get; set; }
        public decimal? RecurringFee { get; set; }
        public MigratedMemberProductDetailsBO MigratedMemberProductDetails { get; set; }
    }
}
