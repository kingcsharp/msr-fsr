using System.Collections.Generic;

namespace MSR.Domain.Views
{
    public class PurchaseOrderProductView
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Total Sale Price
        /// </summary>
        public decimal TotalSalePrice { get; set; }
    }
}
