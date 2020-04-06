using System.Collections.Generic;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerProductsRequestBO
    {
        public string SortColumn { get; set; }        
        public bool IsSortByDesc { get; set; }        
        public int PageNumber { get; set; }        
        public int RecordsPerPage { get; set; } 
        public long BrokerId { get; set; }
        public List<BrokerProductBO> BrokerProducts { get; set; }       
        public int TotalRecords { get; set; }        
        public int TotalRecordsForPagination { get; set; }
    }
}
