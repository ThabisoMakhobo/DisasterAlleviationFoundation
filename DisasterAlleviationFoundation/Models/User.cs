using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class User : IdentityUser
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Role { get; set; }

        [MaxLength(200)]
        public string Skills { get; set; }

        // Navigation properties
        public virtual Volunteer Volunteer { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
    }
}
