using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetPurchaseOrder : Command
    {
        public int? Id { get; set; }
    }
}
