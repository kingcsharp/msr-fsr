using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetPurchaseOrderProduct : Command
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
    }
}
