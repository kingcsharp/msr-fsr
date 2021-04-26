namespace MSR.Answer.API.V1.Models
{
    public class AddNCRWorkOrderTaskRequest
    {
        public int WorkOrderId { get; set; }
        public int ProcedureId { get; set; }
        public int UserId { get; set; }
    }
}