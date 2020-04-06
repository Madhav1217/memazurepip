using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class PrimaryPlanListBO
    {
        public List<PrimaryPlanDetailsBO> PlanDetails { get; set; }
        public AveragePlanPriceRangeBO AveragePlanPriceRange { get; set; }
    }
}
