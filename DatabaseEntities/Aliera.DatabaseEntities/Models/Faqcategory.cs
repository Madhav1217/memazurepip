using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Faqcategory
    {
        public Faqcategory()
        {
            Faq = new HashSet<Faq>();
        }

        public int FaqcategoryId { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public int? PortalId { get; set; }

        public virtual ICollection<Faq> Faq { get; set; }
    }
}
