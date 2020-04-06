using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Plan
    {
        public Plan()
        {
            AddOnPlan = new HashSet<AddOnPlan>();
            MemberSubscribedPlan = new HashSet<MemberSubscribedPlan>();
            PlanPackage = new HashSet<PlanPackage>();
            PlanPremium = new HashSet<PlanPremium>();
        }

        public int PlanId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double PlanRating { get; set; }
        public int Type { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Msra { get; set; }
        public string CoverageLimit { get; set; }
        public string CoverageDuration { get; set; }
        public string RelatedPlanIds { get; set; }
        public double CategoryWisePlanRating { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<AddOnPlan> AddOnPlan { get; set; }
        public virtual ICollection<MemberSubscribedPlan> MemberSubscribedPlan { get; set; }
        public virtual ICollection<PlanPackage> PlanPackage { get; set; }
        public virtual ICollection<PlanPremium> PlanPremium { get; set; }
    }
}
