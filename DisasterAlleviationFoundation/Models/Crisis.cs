using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisasterAlleviationFoundation.Models
{
    public class Crisis
    {
        [Key]
        public int CrisisID { get; set; }

        [Required]
        [Display(Name = "Crisis Type")]
        public string Type { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Reported")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Crisis Status")]
        public string Status { get; set; }

        // Relationships
        [Display(Name = "Linked Resource (Optional)")]
        public int? ResourceID { get; set; }
        [ForeignKey("ResourceID")]
        public virtual Resource Resource { get; set; }

        [Display(Name = "Linked Donation (Optional)")]
        public int? DonationID { get; set; }
        [ForeignKey("DonationID")]
        public virtual Donation Donation { get; set; }

        // Existing navigation properties
        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<Distribution> Distributions { get; set; }
    }
}
