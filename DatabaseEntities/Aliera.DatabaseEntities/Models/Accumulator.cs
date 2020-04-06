using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Accumulator
    {
        public Accumulator()
        {
            MemberAccumulatorDetails = new HashSet<MemberAccumulatorDetails>();
            ProductAccumulator = new HashSet<ProductAccumulator>();
        }

        public long AccumulatorId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MemberAccumulatorDetails> MemberAccumulatorDetails { get; set; }
        public virtual ICollection<ProductAccumulator> ProductAccumulator { get; set; }
    }
}
