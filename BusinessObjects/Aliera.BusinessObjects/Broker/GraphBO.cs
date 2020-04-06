using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class GraphBO
    {
        public long LoggedInBrokerId { get; set; } = 0;
        public ViewBy FilterType { get; set; } = ViewBy.OnlyMe;
        public string BrokerId { get; set; }
        public long InternalBrokerId { get; set; } = 0;
    }
}
