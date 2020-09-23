using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Domain.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ProcedureStepModel
    {
        public ProcedureStepModel() { }
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Procedure that contains this step
        /// </summary>
        public Procedure Procedure { get; set; }

        /// <summary>
        /// Gets or Sets ProcedureId
        /// </summary>
        public int? ProcedureId { get; set; }

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets StepText
        /// </summary>
        public string StepText { get; set; }

        /// <summary>
        /// Gets or Sets Duration
        /// </summary>
        public double? Duration { get; set; }

        /// <summary>
        /// Gets or Sets DurationType
        /// </summary>
        public string DurationType { get; set; }

        /// <summary>
        /// Procedure Step Type
        /// </summary>
        public string ProcedureStepType { get; set; }

        /// <summary>
        /// Procedure Step Type
        /// </summary>
        public string ProcedureStepTypeId { get; set; }

        /// <summary>
        /// Gets or Sets PrintOrder
        /// </summary>
        public int? PrintOrder { get; set; }

        /// <summary>
        /// Gets or Sets PredecessorStepId
        /// </summary>
        public int? PredecessorStepId { get; set; }

        /// <summary>
        /// Gets or Sets LaborTime
        /// </summary>
        public int? LaborTime { get; set; }

        /// <summary>
        /// Gets or Sets EquipmentTime
        /// </summary>
        public int? EquipmentTime { get; set; }

        /// <summary>
        /// Gets or Sets ReplacementCost
        /// </summary>
        public double? ReplacementCost { get; set; }

        /// <summary>
        /// Gets or Sets UtilizationTime
        /// </summary>
        public float? UtilizationTime { get; set; }

        /// <summary>
        /// Gets or Sets UsefulLife
        /// </summary>
        public int? UsefulLife { get; set; }

        /// <summary>
        /// Gets or Sets ReferenceFiles
        /// </summary>
        public List<FileModel> ReferenceFiles { get; set; }

        /// <summary>
        /// Gets or Sets Roles
        /// </summary>
        public List<Role> Roles { get; set; }
    }
}
