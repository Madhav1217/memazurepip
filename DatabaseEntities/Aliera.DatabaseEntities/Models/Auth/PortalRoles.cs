using System;
using System.Collections.Generic;
using Aliera.Utilities.Enumerations;

namespace Aliera.DatabaseEntities.Models.Auth
{
    public partial class PortalRoles
    {
        public PortalRoles()
        {
            RolePermission = new HashSet<RolePermission>();
            UserRoleAssociation = new HashSet<UserRoleAssociation>();
        }

        public int PortalRoleId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsCustomRole { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int PortalRoleTypeId { get; set; }
        public PortalRoleStatus Status { get; set; }

        public virtual PortalRoleTypes PortalRoleType { get; set; }
        public virtual ICollection<RolePermission> RolePermission { get; set; }
        public virtual ICollection<UserRoleAssociation> UserRoleAssociation { get; set; }
    }
}
