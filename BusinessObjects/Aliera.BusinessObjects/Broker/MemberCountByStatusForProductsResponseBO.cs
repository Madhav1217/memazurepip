using System.Collections.Generic;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberCountByStatusForProductsResponseBO
    {
        public string ProductName { get; set; } = string.Empty;
        public int Active { get; set; } = 0;
        public int OnHold { get; set; } = 0;
        public int Inactive { get; set; } = 0;
        public int Pending { get; set; } = 0;
        public int UnderReview { get; set; } = 0;
        public int Total { get; set; } = 0;
    }
}
