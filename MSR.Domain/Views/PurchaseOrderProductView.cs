using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class PurchaseOrderProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalSalePrice { get; set; }
    }
}
