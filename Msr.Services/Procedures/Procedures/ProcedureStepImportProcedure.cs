using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Procedures.Procedures
{
    [StoredProcedure("A_SP_PROCEDURE_IMPORT_STEP_FROM_EXTERNAL_SOURCE")]
    public class ProcedureStepImportProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "msgs", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "PROCEDURE_HIST_ID")]
        public string ProcedureHistId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 4000, ParameterName = "STEP_TEXT")]
        public string StepText { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "Title")]
        public string Title { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PRINT_ORDER")]
        public string PrintOrder { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "REF_DOC_ID")]
        public string RefDocId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "COMMENT")]
        public string Comment { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, ParameterName = "STEP_TIME")]
        public Single StepTime { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2400, ParameterName = "EXTRA_NOTE")]
        public string ExteraNote { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "DEFAULT_ROLE_ID")]
        public string DefaultRoleId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SERIALIZE")]
        public string Serialize { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SUCCESS_MONITOR")]
        public string SuccessMonitor { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "INTERNAL_LOCATION")]
        public string InternalLocation { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "LOC_TYPE")]
        public string LocType { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NtLogin { get; set; }
    }
}
