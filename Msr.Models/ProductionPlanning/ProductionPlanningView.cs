using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.ProductionPlanning
{
   public class ProductionPlanningView
    {
        public string Id { get; set; }
        
        public string Respresentative { get; set; }

        public string Company { get; set; }

        public string DivisionFab { get; set; }
       
        public string ShortDescription { get; set; }

        public string PartKitNo { get; set; }
       
        public string SubmittedBy { get; set; }

        public DateTime? SubmittedDate { get; set; }

        public string Status { get; set; }

    }
}
