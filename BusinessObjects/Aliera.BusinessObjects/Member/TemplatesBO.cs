using System.Collections.Generic;

namespace Aliera.BusinessObjects.Member
{
    public class TemplatesBO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DocumentId { get; set; }
        public List<string> ProductId { get; set; }
    }
}
