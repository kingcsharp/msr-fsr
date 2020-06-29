using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands.Workflow
{
    public class CreateWorkflowStageModel : Command
    {
        public CreateWorkflowStageModel()
        {
        }
        
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
