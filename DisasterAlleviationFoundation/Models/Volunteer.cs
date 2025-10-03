using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisasterAlleviationFoundation.Models
{
    public class Volunteer
    {
        [Key]
        public int VolunteerID { get; set; }

        // Foreign key to Identity User
        [Required]
        public string UserID { get; set; }

        // Navigation property (link to Identity User table)
        [ForeignKey("UserID")]
        public User User { get; set; }

        // Optional fields for volunteers
        [StringLength(200)]
        public string? Skills { get; set; }  // e.g., First Aid, Logistics, IT

        public bool IsAvailable { get; set; } = true; // Available for assignments

        // Add this collection to enable reverse navigation
        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}
