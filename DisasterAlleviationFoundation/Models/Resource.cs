using System;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class Resource
    {
        [Key]
        public int ResourceID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        // Category: e.g., Food, Medical, Shelter, Funds
        [MaxLength(50)]
        public string Category { get; set; }

        // Date the resource was added
        public DateTime DateAdded { get; set; } = DateTime.Now;

        // Optional: track who donated/provided the resource
        public string? DonorName { get; set; }
    }
}
