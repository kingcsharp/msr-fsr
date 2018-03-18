using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Parts.Procedures
{
    [StoredProcedure("A_SP_PARTS_IMPORT_AND_UPDATE_AN_EXTERNAL_PART")]
    public class PartsImportAndUpdateExternalPartProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "retPartID", Direction = ParameterDirection.Output)]
        public string RetPartId { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "externalPartID")]
        public string ExternalPartId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "partName")]
        public string PartName { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string StrNtLogin { get; set; }

    }
}
