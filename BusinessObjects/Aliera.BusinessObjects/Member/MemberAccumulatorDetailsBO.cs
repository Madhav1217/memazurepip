using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class MemberAccumulatorDetailsBO
    {
        public string AccumulatorType { get; set; }
        public decimal MaxValue { get; set; }
        public decimal UsedValue { get; set; }
        public decimal Percentage { get; set; }
        public decimal RemainingPercentage { get; set; }
        public string AccumulatorName { get; set; }
        public string NetworkTier { get; set; }
        public bool IsFamilyAccumulator { get; set; }
    }
}