using System;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Admin
{
    /// <summary>
    /// Model class to provide Roles based on RoleFilterBO
    /// </summary>
    public class RolesBO
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string RoleType { get; set; }
        public string ModuleAccess { get; set; }
        public PortalRoleStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int TotalRecordsCount { get; set; }
    }
}
