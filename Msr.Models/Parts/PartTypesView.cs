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
        public string Id { get; set; }
        public string Name { get; set; }
        public string Spare { get; set; }
        public string Consumable { get; set; }
        public string Unit { get; set; }
        public int? Rev { get; set; }
        public string Status { get; set; }
        public string LockedByName { get; set; }
    }
}
