using System.Collections.Generic;
using Msr.Models.Orders;
using Msr.Services.Orders.Procedures;

namespace Msr.Services.Orders.Messaging
{
    public class WipStepDetailsResponse
    {
        public WipStepDetailsResponse()
        {
            TaskEditDataResult = new TaskEditDataResult();
            MonitorTemplateResult = new MonitorTemplateResult();
        }

        public int StepId { get; set; }
        public int PhStepId { get; set; }
        public TaskEditDataResult TaskEditDataResult { get; set; }
        public MonitorTemplateResult MonitorTemplateResult { get; set; }
    }
}
