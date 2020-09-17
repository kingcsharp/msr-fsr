using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeletePurchaseOrder: Command
    {
        public int Id { get; set; }
    }
}
