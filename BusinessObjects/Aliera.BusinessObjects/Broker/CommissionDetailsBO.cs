using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class CommissionDetailsBO
    {
        public CommissionDetailsBO()
        {
            Commissions = new List<CommissionBO>();
        }
        public int TotalCommissions { get; set; }
        public List<CommissionBO> Commissions { get; set; }
    }
}
