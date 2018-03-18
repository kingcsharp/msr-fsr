namespace Msr.Services.ProductionPlanning.ViewModels
{
    public class ProductImportViewModel
    {
        public string ExternalProductId { get; set; }
        public string ExternalCustId { get; set; }
        public string ExternalPartId { get; set; }
        public string ExternalProcedureId { get; set; }
        public string ExternalProductSupplierId { get; set; }
        public string ExternalAccountSupplierId { get; set; }
        public string InternalCustomerId { get; set; }
        public string InternalProductSupplierId { get; set; }
        public string InternalAccountSupplierId { get; set; }
        public string InternalRoleId { get; set; }
        public string InternalPartId { get; set; }
        public string ProductName { get; set; }
        public string PartName { get; set; }
        public string Oem { get; set; }
        public string Model { get; set; }
        public string Area { get; set; }
        public string Cu { get; set; }
        public string Mm { get; set; }
        public string Price { get; set; }
        public string ResponseTime { get; set; }
        public string SalesTax { get; set; }
        public string InternalProcedureId { get; set; }
        public string IsKit { get; set; }
        public string KitId { get; set; }
        public string KitQty { get; set; }
    }
}

