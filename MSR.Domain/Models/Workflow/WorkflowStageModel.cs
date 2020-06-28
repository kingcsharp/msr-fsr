using MSR.Domain.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Models.Workflow
{
    public class WorkflowStageModel : DeletableModel
    {
        public string Name { get; set; }
        public string LastUpdatedByName { get; set; }
        public string CreatedByName { get; set; }
    }
}
