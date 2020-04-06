using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberCountDetailBO
    {
        public long MemberId { get; set; }
        public int PlandId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public MemberStatus Status { get; set; }
        public MemberProductStatus MemberProductStatus { get; set; }
        public string ExternalId { get; set; }
        public string ProductLabel { get; set; }
    }
}
