using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.PurchesOrder.Procedures
{
    [StoredProcedure("A_SP_ORDER_PURCHASE")]
    public class PurchasePoProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "NewID", Direction = ParameterDirection.Output)]
        public string NewID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "msg", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "orderID")]
        public string Orderid { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "StrNTLogin")]
        public string Strntlogin { get; set; }
      
    }
}
