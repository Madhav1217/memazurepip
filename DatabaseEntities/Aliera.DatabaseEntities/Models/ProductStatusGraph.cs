using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aliera.DatabaseEntities.Models
{
    public class ProductStatusGraph
    {
        [Key]
        public string ProductName { get; set; }
        public int Active { get; set; }
        public int OnHold { get; set; }
        public int Inactive { get; set; }
        public int Pending { get; set; }
        public int UnderReview { get; set; }
        public int Total { get; set; }
    }
}
