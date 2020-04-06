using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Admin
{
    public class SubModulePermissionBO
    {
        public SubModulePermissionBO()
        {
            CanCreate = CanDelete = CanUpdate = CanDownload = CanRead = Permission.No;
        }
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionDefinitionId { get; set; }
        public string SubModuleName { get; set; }
        public string ModuleName { get; set; }
        public int SubModuleId { get; set; }
        public Permission CanRead { get; set; }
        public Permission CanCreate { get; set; }
        public Permission CanUpdate { get; set; }
        public Permission CanDelete { get; set; }
        public Permission CanDownload { get; set; }
        public int PermissionGroupId { get; set; }
        public string ModuleCode { get; set; }
        public bool ValidateCanRead()
        {
            return CanRead != Permission.Yes && (CanCreate == Permission.Yes || CanUpdate == Permission.Yes || CanDelete == Permission.Yes || CanDownload == Permission.Yes) ? false : true;
        }
    }
}
