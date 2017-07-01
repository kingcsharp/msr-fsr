using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Parts
{
    public class PartType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Spare { get; set; }
        public string Consumable { get; set; }
        public DateTime Drcm { get; set; }
        public string ModBy { get; set; }
        public string Object_Id { get; set; }
        public string Unit { get; set; }
        public string Unit_Shipping_Weight { get; set; }
    }
}
