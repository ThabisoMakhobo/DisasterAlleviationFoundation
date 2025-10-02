using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Skills { get; set; }

        // ✅ Custom Role property (quick fix)
        public string Role { get; set; }

        // Relationships
        public Volunteer Volunteer { get; set; }
        public ICollection<Donation> Donations { get; set; }
        public ICollection<IncidentReport> IncidentReports { get; set; }
    }
}
