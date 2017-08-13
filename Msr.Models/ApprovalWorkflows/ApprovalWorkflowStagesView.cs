using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.ApprovalWorkflows
{
    public class ApprovalWorkflowStagesView
    {

        public string WF_Id { get; set; }
        [Key]
        public string WF_Stage_Id { get; set; }
        public string WF_Stage_Name { get; set; }
       

    }
}
