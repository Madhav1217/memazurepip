using System;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerMessagesBO
    {
        public long BrokerMessageId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime MessageSentTime { get; set; }
    }
}
