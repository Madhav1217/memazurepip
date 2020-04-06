using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class ProviderNetwork
    {
        public ProviderNetwork()
        {
            ProductProviderNetwork = new HashSet<ProductProviderNetwork>();
            TemplateDetails = new HashSet<TemplateDetails>();
        }

        public int ProviderNetworkId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<ProductProviderNetwork> ProductProviderNetwork { get; set; }
        public virtual ICollection<TemplateDetails> TemplateDetails { get; set; }

    }
}
