using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Correspondence
{
    public class RulesBO
    {
        public int ID { get; set; }

        public string Name { get; set; }
        
        public int TemplateID { get; set; }

        public string API { get; set; }

        public DateTime? StartTime { get; set; }

    }
}
