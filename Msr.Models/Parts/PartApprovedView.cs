using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Parts
{
    public class PartApprovedView
    {
        public string Id { get; set; }
        public string PartsHistoryId { get; set; }
        public string Unit { get; set; }
        public string ComapnyPartNumber { get; set; }
        public string Name { get; set; }
        public string PartType { get; set; }
        public string Spare { get; set; }
        public string Consumable { get; set; }
        public short? TrackFromStart { get; set; }
        public DateTime? Drcm { get; set; }
        public string ModBy { get; set; }
        public string Comapny { get; set; }
        public string ObjectId { get; set; }
        public string PartTypeName { get; set; }
        public double? UnitShippingWeight { get; set; }
        public string CreatingCo { get; set; }
        public string WeightType { get; set; }
        public Int16? CustomerSeeAvailability { get; set; }
        public Int16? SupplierSeeAvailability { get; set; }
        public Int16? SupplierSeeInstallBase { get; set; }
        public string RootCoName { get; set; }
        public string ComapnyName { get; set; }
        public string WtTypeName { get; set; }
        public string Status { get; set; }
        public string ObjId { get; set; }
    }
}
