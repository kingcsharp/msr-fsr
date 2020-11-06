using MSR.Domain.Commanding;
using System.Collections.Generic;
using MSR.Domain.Models;

namespace MSR.Domain.Commands
{
    public class UpdateProcedureStepTemplate : Command
    {
        public UpdateProcedureStepTemplate()
        {
            ReferenceFileIds = new List<int>();
            ReferenceFiles = new List<FileModel>();
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Text, aka StepText
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or Sets SystemTaskId, aka ProcedureStepTypeId
        /// </summary>
        public int? SystemTaskId { get; set; }

        /// <summary>
        /// LaborTime
        /// </summary>
        public double? LaborTime { get; set; }

        /// <summary>
        /// Gets or Sets ReferenceDocuments
        /// </summary>
        public List<int> ReferenceDocuments { get; set; }

        /// <summary>
        /// EquipmentTime
        /// </summary>
        public double? EquipmentTime { get; set; }

        /// <summary>
        /// Gets or Sets ReplacementCost
        /// </summary>
        public double? ReplacementCost { get; set; }

        /// <summary>
        /// Utilization Time
        /// </summary>
        public double? Utilization { get; set; }

        /// <summary>
        /// Gets or Sets UsefulLife
        /// </summary>
        public int? UsefulLife { get; set; }

        /// <summary>
        /// Gets or Sets Comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// RoleIds list
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
