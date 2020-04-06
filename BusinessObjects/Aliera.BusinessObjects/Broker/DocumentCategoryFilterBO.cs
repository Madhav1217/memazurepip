using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class DocumentCategoryFilterBO
    {
        public string SearchText { get; set; }
        public int RecordsPerPage { get; set; }
        public int PageNumber { get; set; }
        public string SortColumn { get; set; }
        public bool IsSortByDesc { get; set; }
        public DocumentCategoryType DocumentCategoryType { set; get; }
    }  

}
