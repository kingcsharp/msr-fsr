
namespace Msr.Services.Orders.Procedures
{
    public class TaskStepResult
    {
        public string TaskId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string StepId { get; set; }
        public string PhStepId { get; set; }

        public string Has_Child { get; set; }

        public string RawDescription { get; set; }
        public string StepTitle { get; set; }
    }
}
