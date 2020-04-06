using Aliera.Utilities.Enumerations;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class DocumentAndFormFilterBO
    {
        public string SearchText { get; set; }
        public int RecordsPerPage { get; set; }
        public int PageNumber { get; set; }
        public string SortColumn { get; set; }
        public bool IsSortByDesc { get; set; }
        public List<int> CategoryIds { get; set; } 
        public DocumentCategoryType DocumentCategoryType { set; get; }
    }

}
