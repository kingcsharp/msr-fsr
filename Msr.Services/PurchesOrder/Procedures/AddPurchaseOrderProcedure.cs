using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.PurchesOrder.Procedures
{    
        [StoredProcedure("A_SP_ACCOUNTS_UPDATE_ACCOUNT")]
        public class AddPurchaseOrderProcedure
        {           
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "NewID", Direction = ParameterDirection.Output)]
        public string NewID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "Messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ObjID")]
        public string ObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ID")]
        public string Id { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "NAME")]
        public string Name { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 200, ParameterName = "REFERENCE_PO")]
        public string ReferencePO { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 200, ParameterName = "REFERENCE_NAME")]
        public string ReferenceName { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "REFERENCE_FILES")]
        public string ReferenceFiles { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "OPEN_DATE")]
        public string OpenDate { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "CLOSE_DATE")]
        public string CloseDate { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SUPPLIER_CO")]
        public string SupplierCo { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "CUSTOMER_CO")]
        public string CustomerCo { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "CUSTOMER_BILL_CO")]
        public string CustomerBillCo { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "OBJECT_APPLIES_TO")]
        public string ObjectAppliesTo { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "MAXIMUM_USES")]
        public string MaximumUses { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "TOTAL_PURCHASE_LIMIT")]
        public string TotalPurchaseLimit { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "CREDIT_LIMIT")]
        public string CreditLimit { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "APPROVAL_WF")]
        public string ApprovalWF { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "INVOICE_TRIGGER")]
        public string InvoiceTrigger { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "INVOICE_PERIOD_TYPE")]
        public string InvoicePeriodType { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "INVOICE_PERIOD_NUMBER")]
        public string InvoicePeriodNumber { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "FIRST_INVOICE_DATE")]
        public string FirstInvoiceDate { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PAYMENT_GRACE_PERIOD")]
        public string PaymentGracePeriod { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "LATE_FEE_PERCENTAGE")]
        public string LateFeePercentage { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "REAPPLY_LATE_FEE")]
        public string ReapplyLateFee { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "ACCT_TYPE")]
        public string AcctType { get; set; }

        [StoredProcedureParameter(SqlDbType.SmallInt, ParameterName = "LABOR_INCLUDED")]
        public string LaborIncluded { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "LABOR_EXCEPTIONS")]
        public string LaborExceptions { get; set; }

        [StoredProcedureParameter(SqlDbType.SmallInt, ParameterName = "CONSUMABLES_INCLUDED")]
        public string ConsumablesIncluded { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "CONSUMABLE_EXCEPTIONS")]
        public string ConsumableExceptions { get; set; }

        [StoredProcedureParameter(SqlDbType.SmallInt, ParameterName = "NONCONSUMABLE_INCLUDED")]
        public string NonconsumableIncluded { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "NONCONSUMABLE_EXCEPTIONS")]
        public string NonconsumableExceptions { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 500, ParameterName = "BILLING_EMAIL")]
        public string BillingEmail { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, ParameterName = "TAX_RATE")]
        public string TaxRate { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "StrNTLogin")]
        public string Strntlogin { get; set; }

    }
}
