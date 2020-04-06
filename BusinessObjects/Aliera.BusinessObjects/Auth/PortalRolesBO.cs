using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aliera.BusinessObjects.Auth
{
    public class PortalRolesBO
    {
        [Key]
        public int RoleId { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsCustomRole { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}