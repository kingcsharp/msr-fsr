using Msr.Models.Procedures;
using Msr.Services.Procedures.Messages;
using System.Collections.Generic;

namespace Answer.Web.ViewModel
{
    public class ProcedureViewModel
    {
        public ProcedureView ProcedureView { get; set; }
        public List<GetStepDataResult> StepDataList { get; set; }
    }
}