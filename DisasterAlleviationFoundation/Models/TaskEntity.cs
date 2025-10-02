using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class TaskEntity
    {
        [Key]
        public int TaskID { get; set; }

        [Required, MaxLength(200)]
        public string Description { get; set; }

        // FK → Crisis
        [Required]
        public int CrisisID { get; set; }
        public virtual Crisis Crisis { get; set; }

        // FK → Volunteer
        [Required]
        public int VolunteerID { get; set; }
        public virtual Volunteer Volunteer { get; set; }
    }
}
