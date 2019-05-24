namespace Msr.Models.Orders
{
    public static class WorkItemStatusConstants
    {
        public static readonly string Accepted = "ACCEPTED";
        public static readonly string WaitingToStart = "PENDING_PARENT_ACCEPTANCE";
        public static readonly string Requested = "REQUESTED";
        public static readonly string Finished = "FINISHED";
        public static string Closed = "CLOSED";
    }
}
