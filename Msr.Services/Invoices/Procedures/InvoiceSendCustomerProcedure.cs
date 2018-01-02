using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Invoices.Procedures
{
    [StoredProcedure("A_SP_ACCOUNT_INVOICES_MARK_AS_INVOICED")]
  public class InvoiceSendCustomerProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "msgs", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ACCOUNT_ID")]
        public string AccountId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "INVOICE_ID")]
        public string InvoiceId { get; set; }

        [StoredProcedureParameter(SqlDbType.DateTime, ParameterName = "DATE_INVOICED")]
        public DateTime? DateInvoiced { get; set; }

        [StoredProcedureParameter(SqlDbType.DateTime, ParameterName = "DUE_DATE")]
        public DateTime? DueDate { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string strNTLogin { get; set; }
    }
}
