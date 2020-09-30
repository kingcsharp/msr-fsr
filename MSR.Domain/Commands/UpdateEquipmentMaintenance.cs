
using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class UpdateEquipmentMaintenance : Command
    {
        public int? Id { get; set; }

        public int? LocationId { get; set; }

        public int? AssignedToId { get; set; }

        public bool? TroubleState { get; set; }

        public string MaintenanceTask { get; set; }

        public int? StatusId { get; set; }

        public DateTime? PemLastCompletedDate { get; set; }

        public int? FrequencyField { get; set; }

        public string Comments { get; set; }
    }
}
