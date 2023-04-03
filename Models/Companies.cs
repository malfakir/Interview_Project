using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   
        public class Companies
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("Plan")]
        public Guid PlanID { get; set; }
        public Plan Plan { get; set; }
        public List<Users> Users { get; set; }
    }

    
}
