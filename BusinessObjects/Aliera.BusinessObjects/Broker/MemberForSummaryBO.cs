using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberForSummaryBO
    {
        public DemographicsBO Demographics { get; set; }
        public List<MemberAddressBO> Address { get; set; }
        public MemberSummaryPlanListBO Plan { get; set; }
        public MemberSummaryProductListBO ProgramInfo { get; set; }
    }
}
