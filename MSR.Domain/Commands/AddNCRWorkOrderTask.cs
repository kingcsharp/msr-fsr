using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class AddNCRWorkOrderTask : Command
    {
        public int WorkOrderId { get; set; }
        public int ProcedureId { get; set; }
        public int UserId { get; set; }
    }
}
