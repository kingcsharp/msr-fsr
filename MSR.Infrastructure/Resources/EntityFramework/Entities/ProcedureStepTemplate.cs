using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureStepTemplate))]
    public partial class ProcedureStepTemplate : TrackableEntity
    {
        public string Title { get; set; }
        public string StepText { get; set; }
        public int? SystemTaskId { get; set; }
        public double? LaborTime { get; set; }
        public double? EquipmentTime { get; set; }

        [Column(TypeName = "money")]
        public decimal? ReplacementCost { get; set; }
        public Single? Utilization { get; set; }
        public int? UsefulLife { get; set; }
        public string Roles { get; set; }
        public string Comments { get; set; }
    }
}
