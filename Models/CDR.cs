using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CDR
    {
        public string CarrierReference { get; set; }        
        public DateTime ConnectDateTime { get; set; }
        public int Duration { get; set; }
        public string SourceNumber { get; set; }
        public string DestinationNumber { get; set; }
        public string Direction { get; set; }
    }

}
