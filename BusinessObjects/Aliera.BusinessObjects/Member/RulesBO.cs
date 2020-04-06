using System;

namespace Aliera.BusinessObjects.Member
{
    public class RulesBO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TemplateId { get; set; }
        public string API { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string AdditionalInput { get; set; }
        public string TemplateName { get; set; }
        public string RuleType { get; set; }
        public int RuleTypeId { get; set; }
        public bool IsDmsDocument { get; set; }
        public string DeliveryType { get; set; }
        public int DeliveryTypeId { get; set; }
        public int RecipientCategoryId { get; set; }
        public int RetryAttempts { get; set; }
        public DateTime? InActiveDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProductIds { get; set; }
        public int? LanguageId { get; set; }
    }
}
