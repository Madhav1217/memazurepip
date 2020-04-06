using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class GroupDetailsBO
    {
        public int TotalGroups { get; set; }
        public List<GroupBO> Groups { get; set; }
    }
}
