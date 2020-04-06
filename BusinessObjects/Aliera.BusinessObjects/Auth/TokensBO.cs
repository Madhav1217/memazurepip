using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Auth
{
    public class TokensBO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string AccessToken { get; set; }
        public DateTime Validity { get; set; }
        public string IpAddress { get; set; }
    }
}