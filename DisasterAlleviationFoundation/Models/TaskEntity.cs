using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisasterAlleviationFoundation.Models
{
    public class TaskEntity
    {
        public int TaskID { get; set; }

        public string Description { get; set; }

        public int CrisisID { get; set; }

    }
}
