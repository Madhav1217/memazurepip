using System;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerAddressBO
    {
        public long BrokerAddressId { get; set; }
        public bool IsPrimary { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string TimeZone { get; set; }
    }
}