using System;

namespace Aliera.BusinessObjects.Broker
{
    public class CommissionBO
    {
        public string PayPeriod { get; set; }
        public long PayeeBrokerId { get; set; }
        public string PayeeExternalId { get; set; }
        public string PayeeBroker { get; set; }
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
        //Properties when user clicks on a specific Pay Period - +PayeeBrokerId, +MemberBrokerId
        public DateTime PostedDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }
        public string Total { get; set; }

        //Properties when user clicks on a specific Member Broker Id - +PostedDate, +Credit, +Debit, +Total, +PayeeBrokerId, +MemberBrokerId

        public string SubType { get; set; }
        public string MemberId { get; set; }

        public BrokerBO Broker { get; set; }

        public string PersonalNPN { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
