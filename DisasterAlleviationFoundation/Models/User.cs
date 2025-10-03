using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DisasterAlleviationFoundation.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Role { get; set; } = "User";
        public string Skills { get; set; } = string.Empty;

        public Volunteer Volunteer { get; set; }

        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public ICollection<IncidentReport> IncidentReports { get; set; } = new List<IncidentReport>();
    }
}
