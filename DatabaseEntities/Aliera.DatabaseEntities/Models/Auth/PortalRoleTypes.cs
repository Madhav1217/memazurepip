using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models.Auth
{
    public partial class PortalRoleTypes
    {
        public PortalRoleTypes()
        {
            PortalRoles = new HashSet<PortalRoles>();
        }

        public int PortalRoleTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PortalRoles> PortalRoles { get; set; }
    }
}
