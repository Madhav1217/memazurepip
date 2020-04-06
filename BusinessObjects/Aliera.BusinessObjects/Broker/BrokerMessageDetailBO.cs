using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerMessageDetailBO
    {
        public long BrokerId { get; set; }
        public int UserId { get; set; }
        public int ReadCount { get; set; }
        public int UnreadCount { get; set; }
        public int PageNumber { get; set; }
        public int MessagesPerPage { get; set; }
        public string SortColumn { get; set; }
        public bool IsSortByDesc { get; set; }
        public bool IsUserRequest { get; set; }
        public bool? IsArchived { get; set; }
        public List<BrokerMessagesBO> BrokerMessages { get; set; }
    }
}
