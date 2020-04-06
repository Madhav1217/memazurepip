using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class ClaimsStatus
    {
        public ClaimsStatus()
        {
            Claims = new HashSet<Claims>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Claims> Claims { get; set; }
    }
}
