using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class TreeNodeBO
    {
        public TreeNodeBO()
        {
            Children = new List<TreeNodeBO>();
        }
        public BrokerTreeBO Data { get; set; }
        public List<TreeNodeBO> Children { get; set; }
    }
}
