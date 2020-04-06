using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class DocumentCategory
    {
        public DocumentCategory()
        {
            BrokerDocument = new HashSet<BrokerDocument>();
        }

        public int DocumentCategoryId { get; set; }
        public int DocumentCategoryType { get; set; }
        public string DocumentCategoryName { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<BrokerDocument> BrokerDocument { get; set; }
    }
}
