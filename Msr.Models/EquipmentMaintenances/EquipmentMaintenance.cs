using System;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.EquipmentMaintenances
{
    public class EquipmentMaintenance
    {
        [Key]
        public string Id { get; set; }
        public string ObjectId { get; set; }
        public string ScanBarcode { get; set; }
        public string ParentLocation { get; set; }
        public string SubLocationFirst { get; set; }
        public string SubLocationSecond { get; set; }
        public DateTime? DateTime { get; set; }
        public string RequestedById { get; set; }
        public bool TroubleState { get; set; }
        public string MaintenanceTask { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public string StrNTLogin { get; set; }
        public string ApprovedById { get; set; }
    }
}
