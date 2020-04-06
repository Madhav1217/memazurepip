using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.DMSModels
{
    public partial class DocumentAccess
    {
        public long DocumentId { get; set; }
        public int EntityTypeId { get; set; }
        public long EntityId { get; set; }
    }
}
