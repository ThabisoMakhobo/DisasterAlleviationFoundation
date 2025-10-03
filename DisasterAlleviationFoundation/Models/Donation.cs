using System;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class Donation
    {
        [Key]
        public int DonationID { get; set; }

        [Required]
        public string UserID { get; set; }

        public User User { get; set; }

        public int? ResourceID { get; set; }
        public Resource Resource { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Foreign key to User
        [Required]
        public string UserID { get; set; }
        public virtual User User { get; set; }
    }
}
