using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisasterAlleviationFoundation.Models
{
    public class Donation
    {
        [Key]
        public int DonationID { get; set; }

        [Required]
        public string UserID { get; set; } // FK to User

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public int? ResourceID { get; set; } // optional FK to Resource

        [ForeignKey("ResourceID")]
        public virtual Resource Resource { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string DonorName { get; set; } // optional for external donors
    }
}
