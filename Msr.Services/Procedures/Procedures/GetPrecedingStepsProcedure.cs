using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Procedures.Procedures
{
    [StoredProcedure("A_SP_PROCEDURE_STEP_GET_PRECEDING_STEPS")]
    public class GetPrecedingStepsProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "stepID")]
        public string CurStepID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
