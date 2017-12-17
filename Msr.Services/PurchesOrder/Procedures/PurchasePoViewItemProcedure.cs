using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.PurchesOrder.Procedures
{
    [StoredProcedure("A_SP_ACCOUNT_PURCHASE_UPDATE_ITEM")]
    public class PurchasePoViewItemProcedure
    {
      
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "purchObjID")]
        public string purchObjID { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "orderItemID")]
        public string orderItemID { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, Size = 50, ParameterName = "qty")]
        public string qty { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "acctID")]
        public string acctID { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "strNTLogin")]
        public string strNTLogin { get; set; }
    }
}
