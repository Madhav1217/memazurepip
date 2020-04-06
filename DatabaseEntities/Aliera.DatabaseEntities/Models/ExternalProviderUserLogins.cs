using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class ExternalProviderUserLogins
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public long UserId { get; set; }
    }
}
