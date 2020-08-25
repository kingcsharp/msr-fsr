using Amazon.S3.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureStepTemplate))]
    public partial class ProcedureStepTemplate: TrackableEntity
    {
        public string Title { get; set; }
        public string StepText { get; set; }
        public int? SystemTaskId { get; set; }
        public double? LaborTime { get; set; }
        public double? EquipmentTime { get; set; }
        public decimal? ReplacementCost { get; set; }
        public float? Utilization { get; set; }
        public int? UsefulLife { get; set; }
        public string Roles { get; set; }
        public string Comments { get; set; }

        // TODO: There does not seem to be a way to include
        // a piece of static data so it can be used as a foreign key.
        // The "EntityTableName" is always the same here, but cannot be used
        // because it doesn't exist in the database.
        // Therefore, this list of files will be all files with this entity ID,
        // and must be further filtered in the query to get the real list.
        public ICollection<FileEntityMap> ReferenceFiles { get; set; }
    }
}
