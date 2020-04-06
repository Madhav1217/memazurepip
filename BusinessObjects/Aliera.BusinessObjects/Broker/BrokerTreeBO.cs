using System;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerTreeBO
    {
        public BrokerTreeBO()
        {
            Expanded = false;
        }

        public long BrokerId { get; set; }
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string BrokerTypeName { get; set; }
        public int CumulativeChildren { get; set; }
        public int MemberCount { get; set; }
        public int TreeMembersCount { get; set; }
        public bool IsWebsiteActive { get; set; }
        public bool IsActive { get; set; }
        public long ParentId { get; set; }
        public DateTime? TermDate { get; set; }
        public int Status { get; set; }

        public bool Expanded { get; set; }
    }
}
