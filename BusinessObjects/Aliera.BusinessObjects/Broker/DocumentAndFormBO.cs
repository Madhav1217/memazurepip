using System;
using System.Collections.Generic;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class DocumentAndFormBO
    {
        public int DocumentAndFormId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string DocumentName { get; set; }
        public string Ext { get; set; }
        public string URL { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int PortalId { get; set; }
        public long DocumentId { get; set; }
        public IEnumerable<int> ProductIds { set; get; }
        public DocumentType DocumentType { set; get; }
    }


}
