using MSR.Domain.Models.BaseModels;

namespace MSR.Domain.Models
{
    public class PurchaseOrderProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Revision { get; set; }
    }
}
