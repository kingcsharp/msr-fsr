using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Procedures.Procedures
{
    [StoredProcedure("Portal_ProcedureUpdate")]
    public class SaveProcedureProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newObjID", Direction = ParameterDirection.Output)]
        public string NewObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "objID")]
        public string ObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "COMPANY")]
        public string Company { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "VERB")]
        public string Verb { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "NAME")]
        public string Name { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 4000, ParameterName = "COMMENTS")]
        public string Comments { get; set; }

        [StoredProcedureParameter(SqlDbType.Int, ParameterName = "STEPS_IN_AP")]
        public decimal? StepInAp { get; set; }

        [StoredProcedureParameter(SqlDbType.Int, ParameterName = "WIP_MSG")]
        public decimal? WipMsg { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SECURITY_LEVEL")]
        public string SecurityLevel { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SYSTEM_ID")]
        public string SystemId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, ParameterName = "DURATION")]
        public double? Duration { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "DURATION_TYPE")]
        public string DurationType { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }

        [StoredProcedureParameter(SqlDbType.Int, ParameterName = "threshold")]
        public int? Threshold { get; set; }
    }
}