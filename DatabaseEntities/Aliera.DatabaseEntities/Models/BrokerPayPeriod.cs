using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class BrokerPayPeriod
    {
        public int PayPeriodId { get; set; }
        public long BrokerId { get; set; }
        public int? PayPeriod { get; set; }
        public string StartPeriod { get; set; }
        public string EndPeriod { get; set; }

        public virtual Broker Broker { get; set; }
    }
}
