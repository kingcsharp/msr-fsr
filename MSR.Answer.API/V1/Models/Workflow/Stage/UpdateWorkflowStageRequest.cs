using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models.Workflow
{
    public class UpdateWorkflowStageRequest : CreateWorkflowStageRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
