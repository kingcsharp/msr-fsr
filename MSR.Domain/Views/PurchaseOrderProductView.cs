using System.Collections.Generic;

namespace MSR.Domain.Views
{
    public class PurchaseOrderProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalSalePrice { get; set; }
    }
}
