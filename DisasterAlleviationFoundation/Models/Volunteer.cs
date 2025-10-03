using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
public class Volunteer
{
    [Key]
    public int VolunteerID { get; set; }

    // Foreign key to Identity User (string ID, not int!)
    [Required]
    public string UserID { get; set; }


}
