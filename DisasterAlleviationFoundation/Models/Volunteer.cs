using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class Volunteer
    {
        public int VolunteerID { get; set; }

        [Required]
        public string UserID { get; set; }

        public User User { get; set; }

        public ICollection<TaskEntity> Tasks { get; set; }
    }
}
