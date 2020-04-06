using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class PaymentDetailsBO
    {
        public string RoutingNumber { get; set; }
        public string CardOrAccountNumber { get; set; }
        public PaymentType? PaymentType { get; set; } 
    }
}
