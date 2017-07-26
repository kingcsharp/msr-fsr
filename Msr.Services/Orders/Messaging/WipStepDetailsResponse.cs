using System.Collections.Generic;
using Amazon.Runtime.Internal;
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
            ReferenceFiles = new List<GetReferenceFiles>();
            ReferenceTheories = new List<GetReferenceTheories>();
        }

        public int StepId { get; set; }
        public int PhStepId { get; set; }
        public TaskEditDataResult TaskEditDataResult { get; set; }
        public MonitorTemplateResult MonitorTemplateResult { get; set; }
        public List<TaskItemPart> TaskItemParts { get; set; }
        public ImageViewModel Images { get; set; }
        public List<GetReferenceFiles> ReferenceFiles { get; set; }
        public List<GetReferenceTheories> ReferenceTheories { get; set; }
    }
}
