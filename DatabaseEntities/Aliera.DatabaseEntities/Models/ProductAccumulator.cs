using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class ProductAccumulator
    {
        public long ProductAccumulatorId { get; set; }
        public long AccumulatorId { get; set; }
        public int ProductId { get; set; }

        public virtual Accumulator Accumulator { get; set; }
        public virtual Product Product { get; set; }
    }
}
