using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class DocumentCategoryDetailBO {
        public DocumentCategoryDetailBO()
        {
            DocumentCategories = new List<DocumentCategoryBO>();
        }
        public int TotalCount { get; set; }
        public List<DocumentCategoryBO> DocumentCategories { get; set; }
    }

}
