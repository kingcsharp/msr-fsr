using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Invoices.Procedures
{
    [StoredProcedure("A_SP_ACCOUNTS_DELETE_PAYMENT")]
    public class InvoiceDetailDeleteProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "paymentID")]
        public string PaymentId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string strNTLogin { get; set; }
    }
}
