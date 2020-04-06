using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Aliera.Utilities.Enumerations;

namespace Aliera.DatabaseEntities.Models
{
    public class MemberStatusGraph
    {
        [Key]
        public long Id { get; set; }
        public MemberStatus Status { get; set; }
        public decimal Percentage { get; set; }
        public int Count { get; set; }        
    }
}
