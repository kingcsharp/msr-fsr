using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Procedures.Procedures
{
    [StoredProcedure("A_SP_PROCEDURE_FIX_PRECEDING_STEPS_BASED_ON_PRINT_ORDER")]
    public class ProcedureStepBasedonPrintOrders
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "procHistID")]
        public string ProcHistId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTlogin")]
        public string NTLogin { get; set; }
    }
}
