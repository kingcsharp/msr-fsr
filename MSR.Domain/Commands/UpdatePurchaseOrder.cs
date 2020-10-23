namespace MSR.Domain.Commands
{
    public class UpdatePurchaseOrder: CreatePurchaseOrder
    {
        public int Id { get; set; }
        public bool ClosePurchaseOrder { get; set; }
    }
}
