using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Procedures
{
    public class VerbType
    {
        public string ID { get; set; }

        public string NAME { get; set; }
        
        public string Verbtype { get; set; }
        
        public string ShowName { get; set; }

        public string DRCM { get; set; }

        public string MODBY { get; set; }
        
        public string ObjectId { get; set; }
    }
}
