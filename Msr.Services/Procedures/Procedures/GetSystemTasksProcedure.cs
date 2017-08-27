using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Procedures.Procedures
{
    [StoredProcedure("A_SP_DD_SYSTEM_TASKS")]
    public class GetSystemTasksProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
