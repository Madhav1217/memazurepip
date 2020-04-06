using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.DatabaseEntities.Models
{
   public class BrokerCommission
    {
        public long Id { get; set; }
        public long PayeeBrokerId { get; set; }
        public string PayeeExternalId { get; set; }
        public string Commission { get; set; }
        public string Status { get; set; }
        public string Premium { get; set; }
        public string Refunds { get; set; }
        public string MemberBrokerCompany { get; set; }
        public long? MemberBrokerId { get; set; }
        public string MemberExternalId { get; set; }
        public long? GroupId { get; set; }
        public string GroupExternalId { get; set; }
        public string PayeeBrokerCompany { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }

        public string SubType { get; set; }
        public string MemberId { get; set; }
    }
}
