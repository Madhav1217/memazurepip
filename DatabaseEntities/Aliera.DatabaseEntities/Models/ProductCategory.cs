using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Product = new HashSet<Product>();
        }

        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public bool IsPrimary { get; set; }
        public string PlanCategory { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsMigrated { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
