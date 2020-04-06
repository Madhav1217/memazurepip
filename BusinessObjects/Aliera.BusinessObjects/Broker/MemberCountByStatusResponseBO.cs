using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberCountByStatusResponseBO
    {
        public MemberStatus Status { get; set; }
        public decimal Percentage { get; set; } = 0;
        public int Count { get; set; } = 0;
    }
}
