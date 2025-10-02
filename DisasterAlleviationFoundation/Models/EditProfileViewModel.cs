// Models/EditProfileViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class EditProfileViewModel
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Skills { get; set; }
        public string Email { get; set; }
    }
}

