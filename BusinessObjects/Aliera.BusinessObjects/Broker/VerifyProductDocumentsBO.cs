using System.Collections.Generic;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class VerifyProductDocumentsBO
    {
        public IEnumerable<int> ProductIds { set; get; }
        public DocumentType DocumentType { set; get; }
    }


}
