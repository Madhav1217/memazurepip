using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberPricingDetailsRequestBO
    {
        public List<int> PlanId { get; set; }
        public long MemberId { get; set; }
        public long BrokerId { get; set; }
    }
}