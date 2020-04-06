using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class ProductSubCategory
    {
        public ProductSubCategory()
        {
            Product = new HashSet<Product>();
        }

        public int ProductSubCategoryId { get; set; }
        public string ProductSubCategoryName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
