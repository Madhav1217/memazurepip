using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models.Auth
{
    public partial class PermissionGroup
    {
        public PermissionGroup()
        {
            PermissionDefinition = new HashSet<PermissionDefinition>();
        }

        public int PermissionGroupId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<PermissionDefinition> PermissionDefinition { get; set; }
    }
}
