using System;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class PaymentDateBO
    {
        public DateTime PaymentDate { get; set; }
        public List<int> ProductId { get; set; }
    }
}
