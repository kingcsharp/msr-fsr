using EntityFrameworkExtras.EF6;
using System.Data;

namespace Msr.Services.PrePro.Procedure
{
    [StoredProcedure("A_SP_PROCEDURE_STEP_CREATE_FILE_LINK")]
    public class SaveProcedurePreProFileProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "stepID")]
        public string ObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "fileID")]
        public string DocId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NtLogin { get; set; }
    }
}
