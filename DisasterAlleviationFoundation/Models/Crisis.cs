using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class Crisis
    {
        [Key]
        public int CrisisID { get; set; }

        [Required, MaxLength(100)]
        public string Type { get; set; }

        [Required, MaxLength(200)]
        public string Location { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; }

        public virtual ICollection<TaskEntity> Tasks { get; set; }
    }
}
