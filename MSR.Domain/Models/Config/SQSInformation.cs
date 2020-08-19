namespace MSR.Domain.Models.Config
{
    public class SQSInformation
    {
        public string QueueName { get; set; }
        public int LongPollingInSeconds { get; set; }
    }
}
