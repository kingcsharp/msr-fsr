using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.PrePro.Procedure
{
    [StoredProcedure("A_SP_PROCEDURE_STEP_APPLICABLE_OBJECT_UPDATE")]
    public class SaveUpdateApplicableObjectsProcedure
    {
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 500, ParameterName = "msgs", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "stepID")]
        public string StepId { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "linkID")]
        public string LinkId { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, ParameterName = "QTY")]
        public string Quantity { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "OBJ_ID")]
        public string ObjectId { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
