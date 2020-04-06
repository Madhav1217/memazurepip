using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Messages
    {
        public long MessageId { get; set; }
        public long RecipientId { get; set; }
        public int? RecipientGroupId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime MessageSentTime { get; set; }
        public long SenderId { get; set; }
        public bool? IsArchived { get; set; }
        public int PortalId { get; set; }

        public virtual Portals Portal { get; set; }
    }
}
