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
        public string PrimaryLocationId { get; set; }
        public string SubLocationFirstId { get; set; }
        public string SubLocationSecondId { get; set; }
        public DateTime? DateTime { get; set; }
        public string Technician { get; set; }
        public bool TroubleState { get; set; }
        public string MaintenanceTask { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
    }
}
