using EntityFrameworkExtras.EF6;
using System;
using System.Data;

namespace Msr.Services.Parts.Procedures
{
    [StoredProcedure("A_SP_PARTS_UPDATE_ONE_PART")]
    public class SavePartProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newObjID", Direction = ParameterDirection.Output)]
        public string NewObjID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "objID")]
        public string ObjID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "COMPANY")]
        public string Company { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "COMPANY_PART_NUMBER")]
        public string CompanyPartNumber { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "OEM_PART_NUMBER")]
        public string OemPartNumber { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "NAME")]
        public string Name { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PART_TYPE")]
        public string PartType { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SPARE")]
        public string Spare { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "CONSUMABLE")]
        public string Consumable { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "UNIT")]
        public string Unit { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "UNIT_SHIPPING_WEIGHT")]
        public double? UnitShippingWeight { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "subParts")]
        public string SubParts { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "CUSTOMER_SEE_AVAILABILITY")]
        public Int16? CustomerSeeAvailability { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "SUPPLIER_SEE_AVAILABILITY")]
        public Int16? SupplierSeeAvailability { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "SUPPLIER_SEE_INSTALL_BASE")]
        public Int16? SupplierSeeInstallBase { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "INTERNAL_EQUAL_PARTS")]
        public string InternalEqualParts { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "WEIGHT_TYPE")]
        public string WeightType { get; set; }

        [StoredProcedureParameter(SqlDbType.TinyInt, ParameterName = "CREATE_PROD")]
        public byte? CreateProd { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "SUPPLIER_CO")]
        public string SupplierCo { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "PRODUCT_TYPE")]
        public string ProductType { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "PROC_VERB")]
        public string ProcVerb { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "SPECIAL_CUSTOMER")]
        public string SpecialCustomers { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "CUSTOMER_EXCEPTIONS")]
        public string CustomerExceptions { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "CUSTOMERS")]
        public string Customers { get; set; }

        [StoredProcedureParameter(SqlDbType.Decimal, ParameterName = "PRICE")]
        public decimal? Price { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }

    }
}
