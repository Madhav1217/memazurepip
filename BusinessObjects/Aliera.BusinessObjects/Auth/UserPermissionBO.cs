using System;
using System.ComponentModel.DataAnnotations;

namespace Aliera.BusinessObjects.Auth
{
    public class UserPermissionBO
    {
        [Key]
        public int UserPermissionId { get; set; }

        public int UserRoleAssociationId { get; set; }
        public int PermissionDefinitionId { get; set; }
        public Utilities.Enumerations.Permission CanCreate { get; set; }
        public Utilities.Enumerations.Permission CanRead { get; set; }
        public Utilities.Enumerations.Permission CanUpdate { get; set; }
        public Utilities.Enumerations.Permission CanDelete { get; set; }
        public Utilities.Enumerations.Permission CanDownload { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}