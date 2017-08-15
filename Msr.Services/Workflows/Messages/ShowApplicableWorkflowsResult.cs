using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Workflows.Messages
{
   public class ShowApplicableWorkflowsResult
    {
        public string Wf_Id { get; set; }

        public string Act_Id { get; set; }

        public string Activity { get; set; }

        public string Creating_Co { get; set; }

        public string WF_Name { get; set; }
    }
}
