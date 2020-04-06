using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberProductsBO
    {
        public string Product { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? InActiveDate { get; set; }
        public DateTime? HoldDate { get; set; }
        public string BenefitLevel { get; set; }
    }
}
