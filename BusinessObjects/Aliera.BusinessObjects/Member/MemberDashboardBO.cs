using Aliera.BusinessObjects.Broker;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class MemberDashboardBO
    {
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long UserId { get; set; }
        public DateTimeOffset? LastLogin { get; set; }
        public List<MemberDependentBO> DepandentDetails { get; set; }
        public long BrokerId { get; set; }
        public string BrokerName { get; set; }
        public List<MemberPlanBO> PlanDetails { get; set; }
        public CoveredPlansBO PlansCovered { get; set; }
        public long? AvatarImageId { get; set; }
        public long? GroupId { get; set; }
        public string GroupName { get; set; }
        public Dictionary<string, string> ProductCodes { get; set; }
        public int SessionTimeOut { get; set; }
        public int SessionIdleTime { get; set; }
        public bool IsInternalBroker { get; set; }
    }
}