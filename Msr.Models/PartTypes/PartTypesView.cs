using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Parts
{
    public class PartTypesView
    {
        public string LockedBy { get; set; }
        public string UnlockedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Root { get; set; }
        public string RevlInfo { get; set; }
        public string CreatingCo { get; set; }
        public string Status { get; set; }
        public int? Rev { get; set; }
        public string WfsId { get; set; }
        public string LockedByName { get; set; }
        public string CreatingCoName { get; set; }
        public string ApprovalActivity { get; set; }


        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime? Drcm { get; set; }
        public Char? ModBy { get; set; }
        public string ObjectId { get; set; }
        public string  Unit { get; set; }
        public string UnitShippingWeight { get; set; }
        public string Spare { get; set; }
        public string Consumable { get; set; }

    }
}
