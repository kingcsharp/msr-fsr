using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class GetWorkflowModel : Command<ICollection<WorkflowModel>>
    {
        public int? Id { get; set; }
    }
}
