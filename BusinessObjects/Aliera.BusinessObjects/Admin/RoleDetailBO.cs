using System;
using System.Collections.Generic;
using System.Text;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Admin
{
    public class RoleDetailBO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int RoleTypeId { get; set; }
        public List<ModuleAccessBO> Modules { get; set; }
        public PortalRoleStatus Status { get; set; }
        public bool IsCustomRole { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
