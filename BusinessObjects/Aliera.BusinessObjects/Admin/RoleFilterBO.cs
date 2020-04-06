
using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Admin
{
    /// <summary>
    /// Model to fetch input for populating RolesBO
    /// </summary>
    public class RoleFilterBO
    {
        public int PageNumber { get; set; }
        public int RecordsPerPage { get; set; }
        public int Status { get; set; }
        public int RoleTypeId { get; set; }
        public string SortBy { get; set; }
        public int SortOrder { get; set; }
        public string SearchText { get; set; }
        
    }
}
