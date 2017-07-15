using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Msr.Models.Companies
{
    public class Company
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CoType { get; set; }
        public string Parent { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string LocationName { get; set; }
        public string ParentName { get; set; }
        public DateTime Drcm { get; set; }
        public string ModBy { get; set; }
        public string ObjectId { get; set; }
        public string RootCoID { get; set; }
    }
}
