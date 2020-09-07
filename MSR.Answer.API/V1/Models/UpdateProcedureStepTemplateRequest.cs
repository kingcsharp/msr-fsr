using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MSR.Answer.API.V1.Models
{
    /// <summary>
    ///
    /// </summary>
    public class UpdateProcedureStepTemplateRequest
    {
        /// <summary>
        /// ProcedureStepTemplate Id
        /// </summary>
        [DataMember(Name="id")]
        public int Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [DataMember(Name="title")]
        public string Title { get; set; }

        /// <summary>
        /// StepText
        /// </summary>
        [DataMember(Name="stepText")]
        public string StepText { get; set; }

        /// <summary>
        /// ProcedureStepTypeId (SystemTaskId in the DB)
        /// </summary>
        [DataMember(Name="procedureStepTypeId")]
        public int? ProcedureStepTypeId { get; set; }

        /// <summary>
        /// LaborTime
        /// </summary>
        [DataMember(Name="laborTime")]
        public double? LaborTime { get; set; }

        /// <summary>
        /// ReferenceProcedures
        /// </summary>
        [DataMember(Name="referenceProcedures ")]
        public List<int> ReferenceProcedures { get; set; }

        /// <summary>
        /// ReferenceDocuments
        /// </summary>
        [DataMember(Name="referenceDocuments ")]
        public List<int> ReferenceDocuments { get; set; }

        /// <summary>
        /// ReferenceFiles
        /// </summary>
        [DataMember(Name="referenceFiles ")]
        public List<int> ReferenceFiles { get; set; }

        /// <summary>
        /// EquipmentTime
        /// </summary>
        [DataMember(Name="equipmentTime")]
        public double? EquipmentTime { get; set; }

        /// <summary>
        /// ReplacementCost
        /// </summary>
        [DataMember(Name="replacementCost")]
        public double? ReplacementCost { get; set; }

        /// <summary>
        /// Utilization Time
        /// </summary>
        [DataMember(Name="utilization")]
        public double? Utilization { get; set; }

        /// <summary>
        /// UsefulLife
        /// </summary>
        [DataMember(Name="usefulLife")]
        public int? UsefulLife { get; set; }

        /// <summary>
        /// Role list
        /// </summary>
        [DataMember(Name="roles")]
        public List<int> Roles { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [DataMember(Name="comments")]
        public string Comments { get; set; }
    }
}
