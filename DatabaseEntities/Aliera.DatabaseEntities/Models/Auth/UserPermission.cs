using System;
using System.Collections.Generic;
using Aliera.Utilities.Enumerations;
using Newtonsoft.Json;

namespace Aliera.DatabaseEntities.Models.Auth
{
    public partial class UserPermission
    {
        public int UserPermissionId { get; set; }
        public int UserRoleAssociationId { get; set; }
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

        [JsonIgnore]
        public virtual PermissionDefinition PermissionDefinition { get; set; }
        [JsonIgnore]
        public virtual UserRoleAssociation UserRoleAssociation { get; set; }
    }
}
