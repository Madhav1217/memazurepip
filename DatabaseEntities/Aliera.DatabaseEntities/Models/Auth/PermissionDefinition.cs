using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models.Auth
{
    public partial class PermissionDefinition
    {
        public PermissionDefinition()
        {
            RolePermission = new HashSet<RolePermission>();
            UserPermission = new HashSet<UserPermission>();
        }

        public int PermissionDefinitionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int PermissionGroupId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual PermissionGroup PermissionGroup { get; set; }
        public virtual ICollection<RolePermission> RolePermission { get; set; }
        public virtual ICollection<UserPermission> UserPermission { get; set; }
    }
}
