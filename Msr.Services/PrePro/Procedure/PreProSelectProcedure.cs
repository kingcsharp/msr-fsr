using EntityFrameworkExtras.EF6;
using System.Data;

namespace Msr.Services.PrePro.Procedure
{
    [StoredProcedure("A_SP_PREPOP_UPDATE_ONE_PREPOP")]
    public class PrepopUpdateOnePrepop
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newObjID", Direction = ParameterDirection.Output)]
        public string NewObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "objID")]
        public string ObjID { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "procStepID")]
        public string ProcStepID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string StrNTLogin { get; set; }
    }
}
