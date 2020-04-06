using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberPlanPricingDetailsBO
    {
        public int PrdouctId { get; set; }
        public int PlanId { get; set; }
        public int PlanType { get; set; }
        public int? ProductCode { get; set; }
        public string ProductLogo { get; set; }
        public string DocumentPath { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal? TotalPremium { get; set; }
        public int? ProductCategoryId { get; set; }
        public decimal? ApplicationFee { get; set; }
        public decimal? ToboccoFee { get; set; }
        public decimal? FamilyRiderFee { get; set; }
        public decimal PrimaryPlanPremium { get; set; }
        public decimal PackagedPlanPremium { get; set; }
        public List<PlanBenefitListBO> PlanBenefits { get; set; }
        public List<PackagedPlanDetailsBO> PackagedPlans { get; set; }
        public string RelatedPlanIds { get; set; }
        public string ShellSheetName { get; set; }
        public double CategoryWisePlanRating { get; set; }
        public double PlanRating { get; set; }
        public bool IsPolicyTerm { get; set; }
    }

    public class MemberPlanPricingBO
    {
        public List<MemberPlanPricingDetailsBO> MemberPrimaryPlan { get; set; }
        public List<SupplementalPlanDetailsBO> SupplementalPlans { get; set; }
    }
}
