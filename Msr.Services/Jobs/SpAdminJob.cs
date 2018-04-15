using Hangfire;
using Msr.Services.Workflows;

namespace Msr.Services.Jobs
{
    public class SpAdminJob
    {
        [AutomaticRetry(Attempts = 1, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public static void Run()

        {
            var workflowService = new WorkflowService();

            workflowService.SpRunAdminSql();
        }
    }
}
