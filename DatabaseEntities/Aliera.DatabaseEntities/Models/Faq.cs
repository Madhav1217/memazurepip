using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Faq
    {
        public int Faqid { get; set; }
        public int FaqcategoryId { get; set; }
        public string Faqquestion { get; set; }
        public string Faqanswer { get; set; }
        public int PortalId { get; set; }

        public virtual Faqcategory Faqcategory { get; set; }
        public virtual Portals Portal { get; set; }
    }
}
