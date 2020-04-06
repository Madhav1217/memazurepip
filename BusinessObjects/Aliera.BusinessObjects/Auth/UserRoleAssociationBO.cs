using System;
using System.ComponentModel.DataAnnotations;

namespace Aliera.BusinessObjects.Auth
{
    public class UserRoleAssociationBO
    {
        [Key]
        public int UserRoleAssociationId { get; set; }

        public int RoleId { get; set; }
        public long UserId { get; set; }
        public bool IsCustomPermission { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}