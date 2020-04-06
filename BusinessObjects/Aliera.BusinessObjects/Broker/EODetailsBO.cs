using System;

namespace Aliera.BusinessObjects.Broker
{
    public class EODetailsBO
    {
        public int EOId { get; set; }
        public string EOCarrier { get; set; }
        public string EOPolicyNumber { get; set; }
        public bool? IsEOCoverage { get; set; }
        public DateTime? EOExpirationDate { get; set; }
        public decimal? EOAmount { get; set; }
    }
}
