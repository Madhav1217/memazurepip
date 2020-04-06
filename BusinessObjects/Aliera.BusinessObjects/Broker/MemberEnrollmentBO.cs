using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberEnrollmentBO
    {
        public long LoggedInBrokerId { get; set; }
        public bool IsBrokerLicenseActiveforState { get; set; }
        public string NextButton { get; set; }
        public long MemberSubscriptionId { get; set; } = 0;
        public List<QuestionsBO> Questions { get; set; }
        public List<ConsentMemberQuestionAnswersBO> ConsentMemberQuestionAnswers { get; set; }
        public List<MemberBO> Members { get; set; }
        public PaymentBO Payment { get; set; }
    }

    //public class Members
    //{
    //    public DemographicsBO Demographics { get; set; }
    //    public List<MemberAddressBO> Addresses { get; set; }
    //    public List<PlanBO> Plan { get; set; }
    //}
}
