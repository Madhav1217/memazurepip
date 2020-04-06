using System;
using System.Collections.Generic;
using System.Text;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
   public class BrokerHierarchyCountByStatusResponseBO
    {

        public BrokerStatus Status { get; set; }
        public int Count { get; set; } = 0;
    }
}
