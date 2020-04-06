using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class SupplementalPlanDetailsBO
    {
        public int ProductId { get; set; }
        public int PlanId { get; set; }
        public string ProductLogo { get; set; }
        public int PlanType { get; set; }
        public string SupplementPlanName { get; set; }
        public List<PlanBenefitListBO> PlanBenefitLists { get; set; }
        public decimal? ApplicationFee { get; set; }
        public decimal Premium { get; set; }
        public string DocumentPath { get; set; }
        public string Description { get; set; }
        public string ShellSheetName { get; set; }
        public string RelatedPlanIds { get; set; }
        public double CategoryWisePlanRating { get; set; }
        public double PlanRating { get; set; }
    }
}
