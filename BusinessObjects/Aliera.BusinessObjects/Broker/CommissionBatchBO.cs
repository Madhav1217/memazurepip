using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class CommissionBatchBO
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string Status { get; set; }
        public long memberBrokerId { get; set; }
    }
}
