using System;

namespace Msr.Models.EquipmentMaintenances
{
    public class EquipmentMaintenanceView
    {
        public string Id { get; set; }
        public string ObjectId { get; set; }
        public string PrimaryLocation { get; set; }
        public string SubLocationFirst { get; set; }
        public string SubLocationSecond { get; set; }
        public DateTime? DateTime { get; set; }
        public string Technician { get; set; }
        public bool TroubleState { get; set; }
        public string MaintenanceTask { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
    }
}