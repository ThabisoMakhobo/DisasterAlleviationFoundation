using DisasterAlleviationFoundation.Models;
using System.ComponentModel.DataAnnotations;

public class Volunteer
{
    [Key]
    public int VolunteerID { get; set; }

    // Foreign key to Identity User (string ID, not int!)
    [Required]
    public string UserID { get; set; }

    public virtual User User { get; set; }

    // Navigation for tasks
    public virtual ICollection<TaskEntity> Tasks { get; set; }
}
