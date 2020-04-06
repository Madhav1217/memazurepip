using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aliera.BusinessObjects.Auth
{
    public class PermissionDefinitionBO
    {
        [Key]
        public int PermissionDefinitionId { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public int PermissionGroupId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}