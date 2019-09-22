using System.Collections.Generic;
using Msr.Services.Orders.Procedures;
using Msr.Services.Orders.ViewModels;
using Msr.Services.Users.Messages;
using Msr.Models.Orders;
using Msr.Models.Sensor;

namespace Msr.Services.Orders.Messaging
{
    public class WipStepDetailsResponse
    {

        public WipStepDetailsResponse()
        {
            WorkOrderDetailsResponse = new WorkOrderDetailsResponse();
            TaskEditDataResult = new TaskEditDataResult();
            MonitorTemplateResult = new List<MonitorTemplateResult>();
            ReferenceFiles = new List<GetReferenceFiles>();
            ReferenceTheories = new List<GetReferenceTheories>();
            DocLinkImages = new List<WorkOrderImageView>();
            SensorDataModels = new List<SensorDataModel>();
        }

        public string LoginId { get; set; }

        public int StepId { get; set; }
        public int PhStepId { get; set; }
        public int FillId { get; set; }
        public string ParentId { get; set; }
        public string ParentPartId { get; set; }

        public bool HasStepRoles { get; set; }
        public bool HasStepRolesCertification { get; set; }
        public bool HasPreviousStepCompleted { get; set; }

        public LoggedUserIdResult LoggedUserIdResult { get; set; }
        public WorkOrderDetailsResponse WorkOrderDetailsResponse { get; set; }
        public TaskEditDataResult TaskEditDataResult { get; set; }
        public TaskLogDto TaskRunningDto { get; set; }
        public List<WorkOrderImageView> DocLinkImages { get; set; }
        public ImageViewModel Images { get; set; }

        public List<SensorDataModel> SensorDataModels { get; set; }

        public List<TaskItemPart> TaskItemParts { get; set; }
        public List<MonitorTemplateResult> MonitorTemplateResult { get; set; }
        public List<GetReferenceFiles> ReferenceFiles { get; set; }
        public List<GetReferenceTheories> ReferenceTheories { get; set; }

    }
}
