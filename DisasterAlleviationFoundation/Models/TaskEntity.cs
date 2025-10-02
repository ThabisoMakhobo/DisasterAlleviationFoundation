using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisasterAlleviationFoundation.Models
{
    public class TaskEntity
    {
        [Key]  // Explicitly marks this as the primary key
        public int TaskID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        // Foreign Keys
        public int CrisisID { get; set; }
        public int VolunteerID { get; set; }

        // Navigation properties
        public Crisis Crisis { get; set; }
        public Volunteer Volunteer { get; set; }
    }
}
