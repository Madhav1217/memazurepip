using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class BrokerDocument
    {
        public int BrokerDocumentId { get; set; }
        public long BrokerId { get; set; }
        public DateTimeOffset DocumentCreatedDate { get; set; }
        public int DocumentCategoryId { get; set; }
        public string DisplayDocumentName { get; set; }
        public string DocumentName { get; set; }
        public string DocumentLabel { get; set; }
        public string DocumentNotes { get; set; }
        public bool IsSecuredDocument { get; set; }
        public string DocumentType { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? DocumentId { get; set; }
        public string DocumentId123 { get; set; }

        public virtual Broker Broker { get; set; }
        public virtual DocumentCategory DocumentCategory { get; set; }
    }
}
