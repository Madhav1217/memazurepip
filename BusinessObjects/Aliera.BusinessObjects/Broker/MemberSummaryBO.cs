using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberSummaryBO
    {
        public long BrokerId { get; set; }
        public bool IsEmailSMSRequired { get; set; }
        public PaymentDetailsBO Payment { get; set; }
        public List<MemberQuestionnaireBO> Questions { get; set; }
        public List<MemberForSummaryBO> Member { get; set; }
        public bool IsSigned { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
    }
}
