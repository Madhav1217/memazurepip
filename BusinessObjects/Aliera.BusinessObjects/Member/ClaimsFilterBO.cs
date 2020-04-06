using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class ClaimsFilterBO
    {
        public string ExternalMemberId { get; set; }
        public long? userId { get; set; }
        public string FilterAttribute { get; set; }
        public string SortAttribute { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int ClaimsType { get; set; }        
    }
}
