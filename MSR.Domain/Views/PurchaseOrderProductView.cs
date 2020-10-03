namespace MSR.Domain.Views
{
    public class PurchaseOrderProductView
    {
        /// <summary>
        /// Purchase Order Product Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string Name { get; set; }

        public string PartName { get; set; }

        public string PartNumber { get; set; }

        public string ProcedureName { get; set; }

        /// <summary>
        /// Total Sale Price
        /// </summary>
        public decimal TotalSalePrice { get; set; }
    }
}
