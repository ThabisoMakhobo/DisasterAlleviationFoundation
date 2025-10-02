using System;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class IncidentReport
    {
        [Key]
        public int Id { get; set; } // Primary key

        [Required]
        [StringLength(100)]
        public string ReportedBy { get; set; } // Instead of ReporterName

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string ReporterEmail { get; set; }

        [Required]
        [StringLength(100)]
        public string DisasterType { get; set; } // e.g., Flood, Fire, Earthquake

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Location { get; set; }

        // Foreign key to User
        [Required]
        public string UserId { get; set; }

        // Navigation property
        public User User { get; set; }

        // Admin approval flag
        public bool IsApproved { get; set; } = false; // false by default, true if admin approves
    }
}
