using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateWorkOrderTaskRequest
    {
        public int? WorkOrderTaskId { get; set; }
        public int? TaskStepOrder { get; set; }
        public int? AssignedUserId { get; set; }
        public bool? TaskIsRunning { get; set; }
        public DateTime TaskRunningSince { get; set; }
        public DateTime StartedOn { get; set; }
        public string Status { get; set; }
        public decimal? TotalTaskTime { get; set; }
        public ICollection<int> ReferenceFilesIds { get; set; }
        public ICollection<FileModel> ReferenceFiles { get; set; }
    }
}
