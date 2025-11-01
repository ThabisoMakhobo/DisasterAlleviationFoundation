using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisasterAlleviationFoundation.Models
{
    public class Donation
    {
        [Key]
        public int DonationID { get; set; }

        // This will be assigned automatically in the controller, so do NOT mark it [Required]
        public string? UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User? User { get; set; }

        // Optional foreign key
        public int? ResourceID { get; set; }

        [ForeignKey("ResourceID")]
        public virtual Resource? Resource { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string? DonorName { get; set; }
    }
}
