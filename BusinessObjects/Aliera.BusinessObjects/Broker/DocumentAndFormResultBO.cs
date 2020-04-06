using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class DocumentAndFormResultBO
    {
        public bool Status { set; get; }
        public int CategoryId { set; get; }
        public IEnumerable<int> ProductIds { set; get; }
        public int ErrorCode { set; get; }
    }


}
