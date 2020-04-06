namespace Aliera.BusinessObjects.Broker
{
    public class AgentCommissionBO
    {
        public string Parent { get; set; }
        public string BrokerId { get; set; }
        public string PayToBrokerId { get; set; }
        public string PayToBrokerLabel { get; set; }
        public bool PayToAgentActive { get; set; }
        public string Amount { get; set; }
        public string AmountType { get; set; }
        public string PriceTypes { get; set; }
        public string Advancement { get; set; }
        public string AdvancementMonths { get; set; }
        public string IncludeforPaymentNumber { get; set; }
        public string ExcludeforPaymentNumber { get; set; }
    }
}
