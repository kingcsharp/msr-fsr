using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(EquipmentMaintenance))]
    public class EquipmentMaintenance : TrackableEntity
    {
        public int LocationId { get; set; }

        public int? AssignedToId { get; set; }

        public bool? TroubleState { get; set; }

        [StringLength(50)]
        public string MaintenanceTask { get; set; }

        public int StatusId { get; set; }

        public DateTime? PemLastCompletedDate { get; set; }

        public int? FrequencyField { get; set; }

        [StringLength(4000)]
        public string Comments { get; set; }

        public virtual Location Location { get; set; }

        public virtual User AssignedTo { get; set; }

        public virtual Status Status { get; set; }
    }
}
