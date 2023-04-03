using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Plan
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Companies> Companies { get; set; }
        public List<Rates> Rates { get; set; }
    }

}
