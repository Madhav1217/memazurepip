using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class FaqBO
    {
        public int FaqId { get; set; }
        public int CategoryId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
