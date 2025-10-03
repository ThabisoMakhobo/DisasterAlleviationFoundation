using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string Role { get; set; }

    }
}
