using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Models.Parts
{
    public class PartsSafetyStock
    {
        public PartsSafetyStock()
        {
            RolesAssignedToWarn = new List<string>();
            RolesAssignedToFail = new List<string>();
        }

        public string Id { get; set; }
        public string LocationId { get; set; }
        public double? MinLevel { get; set; }
        public double? MinWarningLevel { get; set; }
        public double? MaxWarningLevel { get; set; }
        public double? MaxLevel { get; set; }
        public string LocationName { get; set; }    
        public List<string> RolesAssignedToWarn { get; set; }
        public List<string> RolesAssignedToFail { get; set; }
    }
}
