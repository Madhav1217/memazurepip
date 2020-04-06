using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class ProductDocumentLink
    {
        public long Id { set; get; }
        public int ProductId { get; set; }
        public int DocumentAndFormId { get; set; }
        public int DocumentType { get; set; }

    }
}
