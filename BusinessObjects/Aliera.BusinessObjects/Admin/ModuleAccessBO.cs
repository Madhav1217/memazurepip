using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Admin
{
    public class ModuleAccessBO
    {
        public string ModuleName { get; set; }
        public int ModuleId { get; set; }
        public bool Selected { get; set; }
        public string ModuleCode { get; set; }
        public List<SubModulePermissionBO> SubModules { get; set; }
    }
}
