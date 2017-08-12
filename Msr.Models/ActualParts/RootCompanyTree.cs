using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.ActualParts
{
    public class RootCompanyTree
    {
        public Int32 Tree_Level { get; set; }
        public Int16 Tree_Has_Child { get; set; }
        public Int16 Expanded { get; set; }
        public string Id { get; set; }
        public string History_Ref_Id { get; set; }
        public string Name { get; set; }
        public string Co_Type { get; set; }
        public string Parent { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Location_Name { get; set; }
        public DateTime? Drcm { get; set; }
        public string ModBy { get; set; }
        public string Object_Id { get; set; }
        public string Root_Co { get; set; }
        public string Status { get; set; }
        public string Top_Company { get; set; }
        public string Parent_Name { get; set; }
    }
}
