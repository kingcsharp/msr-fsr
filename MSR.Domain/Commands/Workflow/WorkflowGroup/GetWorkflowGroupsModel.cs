using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class GetWorkflowGroupsModel : Command<ICollection<WorkflowGroupModel>>
    {
        public int? Id { get; set; }
    }
}
