namespace Aliera.BusinessObjects.Broker
{
    public class MemberPaymentDetailsRequestBO
    {
        public long BrokerId { get; set; }
        public long MemberId { get; set; }
        public decimal MSRA { get; set; }
        public decimal MinRange { get; set; }
        public decimal MaxRange { get; set; }
        public int? ProductCategoryId { get; set; }
    }
}