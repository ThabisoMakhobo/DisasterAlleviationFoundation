using System;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class Donation
    {
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
    }
}
