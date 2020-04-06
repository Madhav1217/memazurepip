using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MemberSubscribedPlan
    {
        public MemberSubscribedPlan()
        {
            MemberDependentStatusHistory = new HashSet<MemberDependentStatusHistory>();
            MemberStatusHistory = new HashSet<MemberStatusHistory>();
            MemberSubscribedPlanHistory = new HashSet<MemberSubscribedPlanHistory>();
        }

        public long MemberSubscribedPlanId { get; set; }
        public long MemberSubscriptionId { get; set; }
        public string PolicyNumber { get; set; }
        public int PlanId { get; set; }
        public int PlanType { get; set; }
        public decimal Premium { get; set; }
        public decimal? ApplicationFee { get; set; }
        public decimal? TobaccoRider { get; set; }
        public decimal? FamilyRider { get; set; }
        public int Status { get; set; }
        public DateTime? CancelDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int FamilyIndicator { get; set; }
        public int? PolicyTerm { get; set; }
        public DateTime? FullfillmentDate { get; set; }
        public DateTime? NextBillingDate { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? InactiveDate { get; set; }

        public virtual MemberSubscription MemberSubscription { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual MigratedMemberProductDetails MigratedMemberProductDetails { get; set; }
        public virtual ICollection<MemberDependentStatusHistory> MemberDependentStatusHistory { get; set; }
        public virtual ICollection<MemberStatusHistory> MemberStatusHistory { get; set; }
        public virtual ICollection<MemberSubscribedPlanHistory> MemberSubscribedPlanHistory { get; set; }
    }
}
