using System;

namespace Msr.Models.EquipmentMaintenances
{
    public class EquipmentMaintenanceView
    {
        public int Id { get; set; }
        public string ObjectId { get; set; }
        public string ParentLocation { get; set; }
        public string SubLocationFirst { get; set; }
        public string SubLocationSecond { get; set; }
        public DateTime? DateTime { get; set; }
        public bool TroubleState { get; set; }
        public string MaintenanceTask { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedById { get; set; }
        public string ApprovedById { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? PemLastCompletedDate { get; set; }
        public int? FrequencyField { get; set; }
    }
}