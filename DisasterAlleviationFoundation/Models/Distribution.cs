using System;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class Distribution
    {
        [Key]
        public int DistributionID { get; set; }

        [Required]
        public int CrisisID { get; set; }
        public virtual Crisis Crisis { get; set; }

        [Required]
        public int ResourceID { get; set; }
        public virtual Resource Resource { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
