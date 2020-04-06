using System.Collections.Generic;

namespace Aliera.BusinessObjects.Member
{
    public class MessageDetailBO
    { 
        public int UserId { get; set; }
        public int InboxUnreadCount { get; set; }
        public int InboxCount { get; set; }
        public int ArchiveUnreadCount { get; set; }
        public int ArchiveCount { get; set; }
        public int PageNumber { get; set; }
        public int MessagesPerPage { get; set; }
        public string SortColumn { get; set; }
        public bool IsSortByDesc { get; set; }
        public bool? IsArchivedMessageRequest { get; set; }
        public List<MessageBO> Messages { get; set; }

    }
}
