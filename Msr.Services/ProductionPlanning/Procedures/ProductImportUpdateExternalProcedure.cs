using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.ProductionPlanning.Procedures
{
    [StoredProcedure("A_SP_PRODUCTS_IMPORT_AND_UPDATE_AN_EXTERNAL_PRODUCT")]
    public class ProductImportUpdateExternalProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "msgs", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "EXTERNAL_PRODUCT_ID")]
        public string ExternalProductId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "EXTERNAL_CUSTOMER_ID")]
        public string ExternalCustomerId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "EXTERNAL_PART_ID")]
        public string ExternalPartId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "EXTERNAL_PROCEDURE_ID")]
        public string ExternalProcedureId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "EXTERNAL_PRODUCT_SUPPLIER_ID")]
        public string ExternalProductSupplierId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "EXTERNAL_ACCOUNT_SUPPLIER_ID")]
        public string ExternalAccountSupplierId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "INTERNAL_CUSTOMER_ID")]
        public string InternalCustomerId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "INTERNAL_PRODUCT_SUPPLIER_ID")]
        public string InternalProductSupplierId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "INTERNAL_ACCOUNT_SUPPLIER_ID")]
        public string InternalAccountSupplierId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "INTERNAL_ROLE_ID")]
        public string InternalRoleId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "INTERNAL_PART_ID")]
        public string InternalPartId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PRODUCT_NAME")]
        public string ProductName { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PART_NAME")]
        public string PartName { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "OEM")]
        public string Oem { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "MODEL")]
        public string Model { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PROCESS_AREA")]
        public string Area { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "COPPER")]
        public string Cu { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "MM")]
        public string Mm { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PRICE")]
        public string Price { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "RESPONSE_TIME")]
        public string ResponseTime { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SALES_TAX")]
        public string SalesTax { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "INTERNAL_PROCEDURE_ID")]
        public string InternalProcedureId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "IS_KIT")]
        public string IsKit { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "KIT_ID")]
        public string KitId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "KIT_QTY")]
        public string KitQty { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string StrNtLogin { get; set; }
    }
}
