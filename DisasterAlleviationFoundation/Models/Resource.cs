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
        public int Quantity { get; set; }
    }
}
