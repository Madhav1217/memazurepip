using System;

namespace Aliera.BusinessObjects.Member
{
    public class RulesExecutionBO
    {
        public int Id { get; set; }
        public int RuleId { get; set; }
        public DateTime ExecutionDate { get; set; }
        public int TotalJobsCreated { get; set; }
        public int SuccessfulJobs { get; set; }
        public int FailedJobs { get; set; }
        public string RuleInput { get; set; }
        public string RuleData { get; set; }
    }

    public class JobsSummary
    {
        public DateTime ExecutionDate { get; set; }
        public int TotalJobsCreated { get; set; }
        public int SuccessfulJobs { get; set; }
        public int FailedJobs { get; set; }
        public int PendingJobs { get; set; }
        public int RetryJobs { get; set; }
        public int InProgressJobs { get; set; }
    }
}
