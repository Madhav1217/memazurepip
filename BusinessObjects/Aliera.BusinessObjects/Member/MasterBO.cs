using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class MasterBO
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }

    public class SecurityQuestionsBO
    {
        public long UserId { get; set; }
        public int SecurityQuestionId { get; set; }
        public string SecurityQuestion { get; set; }
    }

    public class SecurityQuestionAnswersBO
    {
        public long UserId { get; set; }
        public int SecurityQuestionId { get; set; }
        public string Answer { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    
    public class MemberPlanBO
    {               
        public string MemberExternalId { get; set; }        
        public string MSRA { get; set; }
        public string Duration { get; set; }
        public string MaxLimit { get; set; }
        public DateTime? CoverageDate { get; set; }
        public DateTime? NextBillingDate { get; set; }
        public DateTime? InActiveDate { get; set; }
        public int PlanType { get; set; }
        public string PlanName { get; set; }
    }   
}
