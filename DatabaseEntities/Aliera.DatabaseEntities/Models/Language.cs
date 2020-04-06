using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Language
    {
        public Language()
        {
            MemberDetail = new HashSet<MemberDetail>();
            TemplateDetails = new HashSet<TemplateDetails>();
        }

        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MemberDetail> MemberDetail { get; set; }
        public virtual ICollection<TemplateDetails> TemplateDetails { get; set; }
    }
}
