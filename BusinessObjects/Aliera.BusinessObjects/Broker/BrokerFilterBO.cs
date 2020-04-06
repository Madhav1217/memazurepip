using Aliera.Utilities.Enumerations;
using System;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerFilterBO
    {
        public string BrokerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNPN { get; set; }
        public BrokerStatus Status { get; set; }
        public string Company { get; set; }
        public bool IsTree { get; set; }
        public long? LoggedinBrokerId { get; set; }
        public List<string> ExcludeBrokerIds { get; set; } = new List<string>();
        public int ViewBy { get; set; }
        public string SortColumn { get; set; }
        public bool IsSortByDesc { get; set; }
        public int PageNumber { get; set; } 
        public int RecordsPerPage { get; set; }
        public List<BrokerSearchBO> BrokerList { get; set; }
        public int TotalRecords { get; set; }
        public string DateOfBirth { get; set; }
    }
}
