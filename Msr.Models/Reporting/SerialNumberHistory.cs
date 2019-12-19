using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Reporting
{

    public class SerialNumberHistory
    {
        [Key]
        public Int64 Id { get; set; }
        public string SerialNumber { get; set; }
        public string WONumber { get; set; }
        public DateTime? WOCreationDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public string MSRFSRFacility { get; set; }
        public string CustomerName { get; set; }
        public string specno { get; set; }
        public string KitName { get; set; }
        public string NCRNumber { get; set; }
        public string pono { get; set; }
        public string mttn { get; set; }
        public int CycleCount { get; set; }

    }
}