using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aliera.DatabaseEntities.Models
{
    public class EnrollmentGraph
    {
        [Key]
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Period { get; set; }
        public int OrderBy { get; set; }
        public bool? IsEnrollment { get; set; }
    }
}
