using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Aliera.DatabaseEntities.Models
{
    public partial class AgentUserBridge
    {
        public long AgentId { get; set; }
        public long UserId { get; set; }

        public virtual Broker Agent { get; set; }
        [JsonIgnore]
        public virtual Users User { get; set; }
    }
}
