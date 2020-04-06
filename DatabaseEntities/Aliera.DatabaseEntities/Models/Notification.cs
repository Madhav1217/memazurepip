using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Notification
    {
        public int Type { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string From { get; set; }
    }
}
