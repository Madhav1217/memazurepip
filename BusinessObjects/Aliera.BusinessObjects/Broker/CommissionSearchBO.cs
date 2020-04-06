using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class CommissionSearchBO
    {
        public string BrokerID { get; set; }
        public long? InternalBrokerId { get; set; }
        public string MemberBrokerId { get; set; }
        public long? InternalMemberBrokerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNPN { get; set; }
        public string Company { get; set; }
        public bool IsTree { get; set; }
        public long? LoggedinBrokerId { get; set; }
        public int RecordsPerPage { get; set; }
        public int PageNumber { get; set; }
        public string SortColumn { get; set; }
        public bool IsSortByDesc { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<CommissionBO> CommissionsByPayPeriod { get; set; }
        public int TotalCount { get; set; }
        public long? GroupId { get; set; }
    }
}
