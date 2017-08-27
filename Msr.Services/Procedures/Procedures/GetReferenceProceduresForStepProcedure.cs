using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Procedures.Procedures
{
    [StoredProcedure("A_SP_PROCEDURE_STEP_GET_REF_PROCEDURES")]
    public class GetReferenceProceduresForStepProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ID")]
        public string Id { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
