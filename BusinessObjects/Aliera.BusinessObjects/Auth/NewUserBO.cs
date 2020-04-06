using System.Collections.Generic;
using Aliera.BusinessObjects.Admin;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Auth
{
    public class NewUserBO
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public UserStatus UserStatus { get; set; }
        public RoleType RoleType { get; set; }
        public EntityType UserType { get; set; }
        public long BrokerId { get; set; }
        public bool IsCustomPermission { get; set; }
        public List<ModuleAccessBO> AssignedModules { get; set; }
        public string SystemPassword { get; set; }
        public long LoggedInUserId { get; set; }
        public bool LockoutEnabled { get; set; }
        public PortalRoleStatus RoleStatus { get; set; }
        public string ExternalId { get; set; }
        public string RoleName { get; set; }
    }
}
