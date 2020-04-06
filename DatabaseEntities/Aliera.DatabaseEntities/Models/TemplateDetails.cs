using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class TemplateDetails
    {
        public int TemplateDetailId { get; set; }
        public int TemplateId { get; set; }
        public int? LanguageId { get; set; }
        public string ProductId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Html { get; set; }
        public int? ProviderNetworkId { get; set; }

        public Language Language { get; set; }
        public ProviderNetwork ProviderNetwork { get; set; }
        public Templates Template { get; set; }
    }
}
