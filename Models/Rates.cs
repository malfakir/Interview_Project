using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Rates
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("Plan")]
        public Guid PlanID { get; set; }
        public Plan Plan { get; set; }
        public string RateType { get; set; }
        public int Priority { get; set; }
        public string Filter { get; set; }
        public decimal Rate { get; set; }
    }
}
