using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisasterAlleviationFoundation.Models
{
    public class Distribution
    {
        [Key]
        public int DistributionID { get; set; }

        // Foreign key to Crisis
        [Required]
        public int CrisisID { get; set; }

        [ForeignKey("CrisisID")]
        public virtual Crisis Crisis { get; set; }

        // Foreign key to Resource
        [Required]
        public int ResourceID { get; set; }

        [ForeignKey("ResourceID")]
        public virtual Resource Resource { get; set; }

        // Optional: track quantity distributed
        [Required]
        public int Quantity { get; set; }
    }
}
