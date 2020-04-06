using System;

namespace Aliera.BusinessObjects.Member
{
    public class RulesInput
    {
        public DateTime? StartDate{ get; set; }
        public DateTime? EndDate{ get; set; }
        public string AdditionalInput { get; set; }
        public string ProductIds { get; set; }
        public int? LanguageId { get; set; }

    }

}