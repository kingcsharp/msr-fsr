using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class UpdatePurchase : Command
    {
        public int purchaseId { get; set; }
    }
}
