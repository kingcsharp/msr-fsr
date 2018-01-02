using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Invoices.Procedures
{
    [StoredProcedure("A_SP_ACCOUNT_INVOICE_EDIT_CREDIT_OR_DEBIT")]
    public class InvoiceEditCreaditDebitProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "ACCOUNT_ID")]
        public string AccountId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "INVOICE_ID")]
        public string InvoiceId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "TYPE")]
        public string Type { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "ID")]
        public string Id { get; set; }

        [StoredProcedureParameter(SqlDbType.Decimal, ParameterName = "UNIT_PRICE")]
        public Decimal? UnitPrice { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, ParameterName = "QTY")]
        public Double? Qty { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "COMMENT")]
        public string Comment { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "DESCRIPTION")]
        public string Description { get; set; }

        [StoredProcedureParameter(SqlDbType.DateTime,  ParameterName = "DATE_POSTED")]
        public DateTime? DatePosted { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "CUST_SINGLE_PO")]
        public string CustSinglePo { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "CUST_LINE_ITEM")]
        public string CustLineNum { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, ParameterName = "TAX_RATE")]
        public Double? TaxRate { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string strNTLogin { get; set; }
    }
}
