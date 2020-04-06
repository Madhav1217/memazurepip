using System;
using System.Collections.Generic;
using System.Text;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerLookupBO
    {
        public string ExternalId { get; set; }
        public long BrokerInternalId { get; set; }
        public string BrokerName { get; set; }
        public BrokerStatus Status { get; set; }
    }
}
