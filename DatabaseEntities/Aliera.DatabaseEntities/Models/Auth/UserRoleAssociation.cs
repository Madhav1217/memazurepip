using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Aliera.DatabaseEntities.Models.Auth
{
    public partial class UserRoleAssociation
    {
        public UserRoleAssociation()
        {
            UserPermission = new HashSet<UserPermission>();
        }

        public int UserRoleAssociationId { get; set; }
        public int PortalRoleId { get; set; }
        public long UserId { get; set; }
        public bool IsCustomPermission { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual PortalRoles PortalRole { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserPermission> UserPermission { get; set; }
    }
}
