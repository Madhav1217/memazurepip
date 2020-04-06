using System;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerLicenseBO
    {
        public int BrokerLicenseId { get; set; }
        public string LicenseNumber { get; set; }
        public long BrokerId { get; set; }
        public string StateCode { get; set; }
        public string Description { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string LicenseType { get; set; }
        public string State { get; set; }
    }
}
