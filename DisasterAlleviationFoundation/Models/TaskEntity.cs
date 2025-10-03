using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisasterAlleviationFoundation.Models
{
    public class TaskEntity
    {
        [Key]
        public int TaskID { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        // Foreign key to Crisis
        [Required]
        public int CrisisID { get; set; }

        [ForeignKey("CrisisID")]
        public Crisis Crisis { get; set; }

        // Optional: Assign a volunteer
        public int? VolunteerID { get; set; }

        [ForeignKey("VolunteerID")]
        public Volunteer? Volunteer { get; set; }

        // Optional: Track completion status
        public bool IsCompleted { get; set; } = false;
    }
}
