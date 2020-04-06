using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerPayPeriodBO
    {
        public string CurrentPayPeriod { get; set; }
        public string LastPayPeriod { get; set; }
        public PayPeriod PayPeriod { get; set; }
        public CommissionStatus CurrentStatus { get; set; }
        public CommissionStatus LastStatus { get; set; }
        public decimal CurrentCommission { get; set; }
        public decimal LastCommission { get; set; }
    }
}
