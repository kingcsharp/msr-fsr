using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MSR.Domain.Models;
using Newtonsoft.Json;

namespace MSR.Answer.API.V1.Models
{
    /// <summary>
    ///
    /// </summary>
    public class CreateProcedureStepTemplateRequest
    {
        public CreateProcedureStepTemplateRequest()
        {
            ReferenceFileIds = new List<int>();
            ReferenceFiles = new List<FileModel>();
        }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// StepText
        /// </summary>
        public string StepText { get; set; }

        /// <summary>
        /// ProcedureStepTypeId (SystemTaskId in the DB)
        /// </summary>
        public int? ProcedureStepTypeId { get; set; }

        /// <summary>
        /// LaborTime
        /// </summary>
        public double? LaborTime { get; set; }

        /// <summary>
        /// ReferenceProcedures
        /// </summary>
        public List<int> ReferenceProcedures { get; set; }

        /// <summary>
        /// ReferenceDocuments
        /// </summary>
        public List<int> ReferenceDocuments { get; set; }

        /// <summary>
        /// EquipmentTime
        /// </summary>
        public double? EquipmentTime { get; set; }

        /// <summary>
        /// ReplacementCost
        /// </summary>
        public double? ReplacementCost { get; set; }

        /// <summary>
        /// Utilization Time
        /// </summary>
        public double? Utilization { get; set; }

        /// <summary>
        /// UsefulLife
        /// </summary>
        public int? UsefulLife { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Role list
        /// </summary>
        public List<int> Roles { get; set; }

        /// <summary>
        /// ReferenceFileIds - Existing fileIds
        /// </summary>
        public ICollection<int> ReferenceFileIds { get; set; }

        /// <summary>
        /// ReferenceFiles - new files
        /// </summary>
        public ICollection<FileModel> ReferenceFiles { get; set; }
    }
}
