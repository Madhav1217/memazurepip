using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerCommissionsBO
    {
        public long BrokerId { get; set; }
        public DateTime PostedDate { get; set; }
        public string Type { get; set; }
        public string Total { get; set; }
        public bool IsCurrentCommission { get; set; }
        public int PayPeriod { get; set; }
        public string CurrentPeriod { get; set; }
        public string LastPeriod { get; set; }
    }
}
