
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
        public string Title { get; set; }
        public string Roles { get; set; }
        public int? PRINT_ORDER { get; set; }
        public int? Verb { get; set; }
        public string Verb_Name { get; set; }

        public bool IsNCRTask
        {
            get
            {
                const int NCR_VERB_ID = 123004;

                bool isNCRTask = false;

                if (Verb == NCR_VERB_ID)
                {
                    isNCRTask = true;
                }

                if (Verb_Name?.Trim().ToUpper().Contains("NCR") == true || Verb_Name?.Trim().ToUpper().Contains("NON-CONFIRMITY") == true)
                {
                    isNCRTask = true;
                }

                return isNCRTask;
            }
        }

    }
}
