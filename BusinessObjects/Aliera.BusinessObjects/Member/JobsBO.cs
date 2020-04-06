using System;

namespace Aliera.BusinessObjects.Member
{
    public class JobsBO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int RuleId { get; set; }
        public string Data { get; set; }
        public int Status { get; set; }
        public int FailedAttemptCount { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }        
        public int RetryAttempts { get; set; }
        public int? DocumentId { get; set; }
        public int DeliveryType { get; set; }      
        public int? TemplateId { get; set; }
        public string Type { get; set; }
        public string TemplateName { get; set; }
        public bool IsDMSDocument { get; set; }
        public int RecipientCategoryId { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public int TotalNoOfRecords { get; set; }
        public string EmailId { get; set; }
        public int RuleTypeId { get; set; }
        public string RuleName { get; set; }
        public bool IsAttachment { get; set; }
        public int RulesExecutionStatusId { get; set; }
    }
}
