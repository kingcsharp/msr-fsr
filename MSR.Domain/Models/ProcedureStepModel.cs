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
        public ProcedureStepModel()
        {
            ReferenceFiles = new List<FileModel>();
            ReferenceDocumentIds = new List<int>();
        }

        public int Id { get; set; }
        public Procedure Procedure { get; set; }
        public int? ProcedureId { get; set; }
        public string Title { get; set; }
        public string StepText { get; set; }
        public double? Duration { get; set; }
        public string DurationType { get; set; }
        public string ProcedureStepType { get; set; }
        public int? ProcedureStepTypeId { get; set; }
        public int? PrintOrder { get; set; }
        public int? PredecessorStepId { get; set; }
        public double? LaborTime { get; set; }
        public int? EquipmentTime { get; set; }
        public double? ReplacementCost { get; set; }
        public float? Utilization { get; set; }
        public int? UsefulLife { get; set; }
        public List<FileModel> ReferenceFiles { get; set; }
        public List<Role> Roles { get; set; }
        public List<int> ReferenceDocumentIds { get; set; }
        public List<FileModel> ReferenceDocument { get; set; }
        public string ApprovalStatus { get; set; }
        public bool IsUsed { get; set; }
        public List<ProcedureStepMonitor> ProcedureStepMonitors { get; set; }
    }
}
