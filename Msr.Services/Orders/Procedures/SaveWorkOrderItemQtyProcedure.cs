using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Orders.Procedures
{
    [StoredProcedure("A_SP_FILL_ITEM_UPDATE_QTY")]
    public class SaveWorkOrderItemQtyProcedure
    {
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 500, ParameterName = "RESULT", Direction = ParameterDirection.Output)]
        public string Result { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "fillItemID")]
        public string ItemId { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, ParameterName = "qty")]
        public string Quanitiy { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
