using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.ApprovalWorkflows
{
    public class ApprovalWorkflowsActivitiesView
    {

        public string WF_Id { get; set; }
        [Key]
        public string Act_Id { get; set; }
        public string Activity { get; set; }
       

    }
}
