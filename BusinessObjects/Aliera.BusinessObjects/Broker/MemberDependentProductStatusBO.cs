using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberDependentProductStatusBO
    {
        public DateTime? ActiveDate { get; set; }
        public DateTime? InactiveDate { get; set; }
        public DateTime? HoldDate { get; set; }
        public string Product { get; set; }
    }
}
