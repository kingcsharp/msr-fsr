using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CancelWorkOrder : Command
    {
        public int WorkOrderId { get; set; }
        public bool Invoiceable { get; set; }
    }
}
