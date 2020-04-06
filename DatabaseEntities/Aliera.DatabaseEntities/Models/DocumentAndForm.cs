using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class DocumentAndForm
    {
        public int DocumentAndFormId { get; set; }
        public int DocumentCategoryId { get; set; }
        public string DocumentName { get; set; }
        public string Ext { get; set; }
        public string Path { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int PortalId { get; set; }
        public long? DocumentId { get; set; }
        public bool IsDeleted { get; set; }
        public int DocumentType { set; get; }

        public virtual Portals Portal { get; set; }
        public virtual DocumentCategory DocumentCategory { get; set; }

    }
}
