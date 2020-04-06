using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class ExternalProviderUserTokens
    {
        public long UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
