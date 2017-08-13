using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.ProcedureVerbs.Procedures
{
    [StoredProcedure("A_SP_TT_VERBS_UPDATE_ONE_VERB")]
    public class SaveProcedureVerbsProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newObjID", Direction = ParameterDirection.Output)]
        public string NewObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "objID")]
        public string ObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "NAME")]
        public string Name { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "VERB_TYPE")]
        public string VerbType { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
