namespace Aliera.BusinessObjects.Broker
{
    public class PackagedPlanDetailsBO
    {
        public int PlanId { get; set; }
        public int? ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal? Premium { get; set; }
        public decimal? ApplicationFee { get; set; }
        public int PlanType { get; set; }
        public int ProductId { get; set; }
        public string MSRA { get; set; }
        public string Duration { get; set; }
        public string Limit { get; set; }
    }
}
