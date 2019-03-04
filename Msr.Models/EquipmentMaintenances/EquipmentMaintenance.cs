using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Msr.Models.EquipmentMaintenances
{
    public class EquipmentMaintenance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ObjectId { get; set; }
        public string ScanBarcode { get; set; }
        public string RoomEquipment { get; set; }
        public DateTime? DateTime { get; set; }
        public string RequestedById { get; set; }
        public bool TroubleState { get; set; }
        public string MaintenanceTask { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public string StrNTLogin { get; set; }
        public string AssignedToId { get; set; }
        public int? FrequencyField { get; set; }
        public DateTime? PemLastCompletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
