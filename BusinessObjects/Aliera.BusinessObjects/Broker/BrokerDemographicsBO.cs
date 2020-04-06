using System;
using System.Collections.Generic;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerDemographicsBO
    {
        public long BrokerId { get; set; }
        public string ExternalId { get; set; }
        public long UserId { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string PersonalNPN { get; set; }
        public List<BrokerAddressBO> BrokerAddress { get; set; }
        public BrokerContactBO BrokerContact { get; set; }
        public string BestTimeToCall { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Parent { get; set; }
        public BrokerStatus Status { get; set; }
        public string BrokerType { get; set; }
        public string TaxId { get; set; }
        public EODetailsBO EODetails { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}