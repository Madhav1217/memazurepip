using System.Collections.Generic;

namespace Aliera.BusinessObjects.Auth
{
    public class CustomRolesBO
    {
        public int RoleId { get; set; }
        public string Role { get; set; }
        public long UserId { get; set; }
        public List<Privilege> Privileges { get; set; }
    }

    public class Privilege
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<Permission> Permission { get; set; }
    }

    public class Permission
    {
        public Utilities.Enumerations.Permission CanCreate { get; set; }
        public Utilities.Enumerations.Permission CanRead { get; set; }
        public Utilities.Enumerations.Permission CanUpdate { get; set; }
        public Utilities.Enumerations.Permission CanDelete { get; set; }
        public Utilities.Enumerations.Permission CanDownload { get; set; }
    }

    public class RolesList
    {
        public int RoleId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int PermissionDefinitionId { get; set; }
        public string PermissionDefinitionCode { get; set; }
        public string PermissionDefinitionName { get; set; }
        public Utilities.Enumerations.Permission CanCreate { get; set; }
        public Utilities.Enumerations.Permission CanRead { get; set; }
        public Utilities.Enumerations.Permission CanUpdate { get; set; }
        public Utilities.Enumerations.Permission CanDelete { get; set; }
        public Utilities.Enumerations.Permission CanDownload { get; set; }
    }
}