using Aliera.Utilities.Enumerations;
using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models.Auth
{
    public partial class RolePermission
    {
        public int RolePermissionId { get; set; }
        public int PortalRoleId { get; set; }
        public int PermissionDefinitionId { get; set; }
        public Permission CanCreate { get; set; }
        public Permission CanRead { get; set; }
        public Permission CanUpdate { get; set; }
        public Permission CanDelete { get; set; }
        public Permission CanDownload { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual PermissionDefinition PermissionDefinition { get; set; }
        public virtual PortalRoles PortalRole { get; set; }
    }
}
