using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class MessageBO
    {
        public long MessageId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public bool IsArchived { get; set; }
        public DateTime MessageSentTime { get; set; }
        public string SenderEmailId { get; set; }
    }
}
