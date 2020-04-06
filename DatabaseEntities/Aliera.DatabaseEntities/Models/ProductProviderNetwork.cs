using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class ProductProviderNetwork
    {
        public int ProductProviderNetworkId { get; set; }
        public int ProductId { get; set; }
        public int ProviderNetworkId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProviderNetwork ProviderNetwork { get; set; }
    }
}
