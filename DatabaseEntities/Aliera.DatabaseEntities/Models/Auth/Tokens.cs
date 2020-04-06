using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models.Auth
{
    public partial class Tokens
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string AccessToken { get; set; }
        public DateTime Validity { get; set; }
        public string IpAddress { get; set; }
    }
}
