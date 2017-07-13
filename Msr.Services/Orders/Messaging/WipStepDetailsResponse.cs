using System.Collections.Generic;
using Msr.Services.Orders.Procedures;
using Msr.Services.Orders.ViewModels;

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
        public List<TaskItemPart> TaskItemParts { get; set; }
        public ImageViewModel Images { get; set; }
    }
}
