namespace Msr.Services.Workflows.ViewModels
{
    public class DeniedWorkflowViewModel : BaseNotificationRequest
    {
        public string Wfsid { get; set; }
        public string WfStageId { get; set; }
        public string WfGroupId { get; set; }
        public string Approver { get; set; }
        public string NtLogin { get; set; }
        public string Reason { get; set; }
    }
}
