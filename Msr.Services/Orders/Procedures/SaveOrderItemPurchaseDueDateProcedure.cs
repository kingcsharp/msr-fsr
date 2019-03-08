using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Orders.Procedures
{
    [StoredProcedure("A_SP_PURCHASE_ITEM_UPDATE_DUE_DATE")]
    public class SaveOrderItemPurchaseDueDateProcedure
    {
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "purchItemID")]
        public string purchItemID { get; set; }

        [StoredProcedureParameter(SqlDbType.DateTime, ParameterName = "myDate")]
        public DateTime DueDate { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
