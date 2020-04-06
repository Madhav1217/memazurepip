using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aliera.BusinessObjects.Auth
{
    public class RolePermissionBO
    {
        [Key]
        public int RolePermissionId { get; set; }

        public int RoleId { get; set; }
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