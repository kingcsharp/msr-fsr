using System.Collections.Generic;

namespace MSR.Answer.API.V1.Models
{
    public class ReorderStepsRequest
    {
        public List<ReorderStepRequest> ProcedureSteps { get; set; }
    }
}
