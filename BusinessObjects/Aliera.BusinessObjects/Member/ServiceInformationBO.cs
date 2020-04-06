using System;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Member
{
    public class ServiceInformationBO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MemberName { get; set; }
        public string MemberType { get; set; }
        public List<string> PlanName { get; set; }
        public List<string> Product { get; set; }
        public List<ProductInformationBO> ProductInformation { get; set; }
        public string LevelOfCoverage { get; set; }
        public DateTime? PaidThroughDate { get; set; }
        public long DependentDetailId { get; set; }
        public long? DigitalCardId { get; set; }
        public string DependentStatus { get; set; }
    }
}