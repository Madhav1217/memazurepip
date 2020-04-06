using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class State
    {
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string Timezone { get; set; }
        public string ZipCodeRange { get; set; }
    }
}
