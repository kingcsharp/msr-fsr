using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureStepTemplate))]
    public partial class ProcedureStepTemplate : TrackableEntity
    {
        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Text, aka StepText
        /// </summary>
        public string StepText { get; set; }

        /// <summary>
        /// Gets or Sets SystemTaskId, aka ProcedureStepTypeId
        /// </summary>
        public int? SystemTaskId { get; set; }

        /// <summary>
        /// LaborTime
        /// </summary>
        public double? LaborTime { get; set; }

        /// <summary>
        /// EquipmentTime
        /// </summary>
        public double? EquipmentTime { get; set; }

        /// <summary>
        /// Gets or Sets ReplacementCost
        /// </summary>
        [Column(TypeName = "money")]
        public decimal? ReplacementCost { get; set; }

        /// <summary>
        /// Utilization Time
        /// </summary>
        public float? Utilization { get; set; }

        /// <summary>
        /// Gets or Sets UsefulLife
        /// </summary>
        public int? UsefulLife { get; set; }

        /// <summary>
        /// Role list
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// Gets or Sets Comments
        /// </summary>
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
