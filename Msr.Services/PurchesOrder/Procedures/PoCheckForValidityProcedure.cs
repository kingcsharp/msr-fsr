using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.PurchesOrder.Procedures
{
    [StoredProcedure("A_SP_OBJECT_CHECK_FOR_VALIDITY")]
    public class PoCheckForValidityProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "returnVal")]
        public string returnValue { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "messages")]
        public string messages { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "objID")]
        public string objId { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "strNTLogin")]
        public string strNTLogin { get; set; }

    }
}
