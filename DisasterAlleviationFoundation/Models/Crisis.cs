using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class Crisis
    {
        [Key]
        public int CrisisID { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Status { get; set; }

        // Navigation property
        public virtual ICollection<TaskEntity> Tasks { get; set; }
        public virtual ICollection<Distribution> Distributions { get; set; }
    }
}
