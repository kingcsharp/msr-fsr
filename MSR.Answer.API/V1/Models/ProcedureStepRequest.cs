using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Text;

namespace MSR.Answer.API.V1.Models
{
    /// <summary>
    ///
    /// </summary>
    [DataContract]
    public partial class ProcedureStepRequest
    {
        public ProcedureStepRequest() { }
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets ProcedureId
        /// </summary>
        [DataMember(Name="procedureId")]
        public int ProcedureId { get; set; }

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        [DataMember(Name="title")]
        public string Title { get; set; }
        /// <summary>
        /// Gets or Sets StepText
        /// </summary>
        [DataMember(Name="stepText")]
        public string StepText { get; set; }

        /// <summary>
        /// Gets or Sets Duration
        /// </summary>
        [DataMember(Name="duration")]
        public double Duration { get; set; }

        /// <summary>
        /// Gets or Sets DurationType
        /// </summary>
        [DataMember(Name="durationType")]
        public string DurationType { get; set; }

        /// <summary>
        /// Gets or Sets PrintOrder
        /// </summary>
        [DataMember(Name="printOrder")]
        public int PrintOrder { get; set; }

        /// <summary>
        /// Gets or Sets PredecessorStepId
        /// </summary>
        [DataMember(Name="predecessorStepId")]
        public int? PredecessorStepId { get; set; }

        /// <summary>
        /// Gets or Sets LaborTime
        /// </summary>
        [DataMember(Name="laborTime")]
        public double? LaborTime { get; set; }

        /// <summary>
        /// Gets or Sets ReplacementCost
        /// </summary>
        [DataMember(Name="replacementCost")]
        public decimal? ReplacementCost { get; set; }

        /// <summary>
        /// Gets or Sets UtilizationTime
        /// </summary>
        [DataMember(Name="utilization")]
        public float? Utilization { get; set; }

        /// <summary>
        /// Gets or Sets EquipmentTime
        /// </summary>
        [DataMember(Name="equipmentTime")]
        public int? EquipmentTime { get; set; }

        /// <summary>
        /// Gets or Sets UsefulLife
        /// </summary>
        [DataMember(Name="usefulLife")]
        public int? UsefulLife { get; set; }

        /// <summary>
        /// Gets or Sets ReferenceFiles
        /// </summary>
        [DataMember(Name="referenceFiles")]
        public List<FileRequest> ReferenceFiles { get; set; }

        /// <summary>
        /// Gets or Sets Roles
        /// </summary>
        [DataMember(Name="roles")]
        public List<RoleRequest> Roles { get; set; }
    }
}
