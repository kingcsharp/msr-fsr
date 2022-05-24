using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class UpdateWorkOrderPrice : Command
    {
        public int WorkOrderId { get; set; }
        public decimal Price { get; set; }
    }
}
