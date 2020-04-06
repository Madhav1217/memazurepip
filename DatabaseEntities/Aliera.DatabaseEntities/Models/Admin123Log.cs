using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Admin123Log
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public string Response { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
