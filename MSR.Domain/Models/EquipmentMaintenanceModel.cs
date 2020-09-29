using MSR.Domain.Models.BaseModels;
using System;

namespace MSR.Domain.Models
{
    public class EquipmentMaintenanceModel : TrackableModel
    {
        public int LocationId { get; set; }

        public int? AssignedToId { get; set; }

        public bool? TroubleState { get; set; }

        public string MaintenanceTask { get; set; }

        public int StatusId { get; set; }

        public DateTime? PemLastCompletedDate { get; set; }

        public int? FrequencyField { get; set; }

        public string Comments { get; set; }

        public virtual LocationModel Location { get; set; }

        public virtual UserModel AssignedTo { get; set; }

        public virtual StatusModel Status { get; set; }
    }
}
