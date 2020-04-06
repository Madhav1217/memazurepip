using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class ProductCommissionBO
    {
        public ProductCommissionBO()
        {
            ProductBasedCommission = new List<AgentCommissionBO>();
            ApplicationBasedCommission = new List<AgentCommissionBO>();
        }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public List<AgentCommissionBO> ProductBasedCommission { get; set; }
        public List<AgentCommissionBO> ApplicationBasedCommission { get; set; }
    }
}
