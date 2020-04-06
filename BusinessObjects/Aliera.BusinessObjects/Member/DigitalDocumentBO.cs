using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class DigitalDocumentBO
    {
        public string DocumentType { get; set; }
        public long DocumentId { get; set; }
        public long Identifier { get; set; }       
        public string MemberType { get; set; }
    }
}
