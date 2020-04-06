using System;
using System.ComponentModel.DataAnnotations;

namespace Aliera.DatabaseEntities.Models
{
    public class BrokerCommissions
    {
        public long Id { get; set; }
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
