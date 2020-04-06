using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class PrimaryPlanDetailsBO
    {
        public int PrdouctId { get; set; }
        public int PlanId { get; set; }
        public int PlanType { get; set; }
        public int? ProductCode { get; set; }
        public string ProductLogo { get; set; }
        public string ProductLogoName { get; set; }
        public string DocumentPath { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double PlanRating { get; set; }
        public decimal Premium { get; set; }
        public int? ProductCategoryId { get; set; }
        public decimal? MSRA { get; set; }
        public List<PlanBenefitListBO> PlanBenefits { get; set; }
        public decimal ApplicationFee { get; set; }
        public string RelatedPlanIds { get; set; }
        public string ShellSheetName { get; set; }
        public double CategoryWisePlanRating { get; set; }
        public bool IsPolicyTerm { get; set; }
        public int PolicyTerm { get; set; }
    }
}
