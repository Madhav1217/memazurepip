using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class DocumentAndFormDetailBO
    {
        public DocumentAndFormDetailBO()
        {
            DocumentAndForms = new List<DocumentAndFormBO>();
        }
        public int TotalCount { get; set; }
        public List<DocumentAndFormBO> DocumentAndForms { get; set; }
    }
}
