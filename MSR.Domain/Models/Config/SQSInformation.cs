namespace MSR.Domain.Models.Config
{
    public class SQSInformation
    {
        public string QueueName { get; set; }
        public string QueueURL { get; set; }
        public string DLQueueName { get; set; }
        public string DLQueueURL { get; set; }
        public int LongPollingInSeconds { get; set; }
    }
}
