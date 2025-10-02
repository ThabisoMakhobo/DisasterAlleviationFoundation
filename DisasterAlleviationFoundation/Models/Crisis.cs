using System;
using System.Collections.Generic;

namespace DisasterAlleviationFoundation.Models
{
    public class Crisis
    {
        public int CrisisID { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        // Navigation property
        public ICollection<TaskEntity> Tasks { get; set; }
    }
}
