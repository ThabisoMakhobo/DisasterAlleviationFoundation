
namespace DisasterAlleviationFoundation.Models
{
    public class Distribution
    {
        [Key]
        public int DistributionID { get; set; }

        [Required]
        public int CrisisID { get; set; }
        public virtual Crisis Crisis { get; set; }

        [Required]
        public int ResourceID { get; set; }


}
