using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Templates
    {
        public Templates()
        {
            TemplateDetails = new HashSet<TemplateDetails>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<TemplateDetails> TemplateDetails { get; set; }
    }
}
