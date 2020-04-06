using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Portals
    {
        public Portals()
        {
            DocumentAndForm = new HashSet<DocumentAndForm>();
            Faq = new HashSet<Faq>();
            Messages = new HashSet<Messages>();
        }

        public int PortalId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DocumentAndForm> DocumentAndForm { get; set; }
        public virtual ICollection<Faq> Faq { get; set; }
        public virtual ICollection<Messages> Messages { get; set; }
    }
}
