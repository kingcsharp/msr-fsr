using System.Collections.Generic;
using Msr.Services.Orders.Procedures;
using Msr.Services.Orders.ViewModels;
using Msr.Services.Users.Messages;

namespace Msr.Services.Orders.Messaging
{
    public class WipStepDetailsResponse
    {
        public WipStepDetailsResponse()
        {
            TaskEditDataResult = new TaskEditDataResult();
            MonitorTemplateResult = new List<MonitorTemplateResult>();
            ReferenceFiles = new List<GetReferenceFiles>();
            ReferenceTheories = new List<GetReferenceTheories>();
        }

        public int StepId { get; set; }
        public int FillId { get; set; }
        public int PhStepId { get; set; }
        public string LoginId { get; set; }
        public TaskEditDataResult TaskEditDataResult { get; set; }
        public List<MonitorTemplateResult> MonitorTemplateResult { get; set; }
        public List<TaskItemPart> TaskItemParts { get; set; }
        public ImageViewModel Images { get; set; }
        public List<GetReferenceFiles> ReferenceFiles { get; set; }
        public List<GetReferenceTheories> ReferenceTheories { get; set; }
        public LoggedUserIdResult LoggedUserIdResult { get; set; }
        public TaskLogDto TaskRunningDto { get; set; }
        public bool HasStepRoles { get; set; }
        public string ParentPartId { get; set; }
        public string ParentId { get; set; }
    }
}
