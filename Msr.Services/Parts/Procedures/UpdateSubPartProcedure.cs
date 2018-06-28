using EntityFrameworkExtras.EF6;
using System.Data;

namespace Msr.Services.Parts.Procedures
{
    [StoredProcedure("A_SP_PARTS_SUB_PART_UPDATE_ONE")]
    public class UpdateSubPartProcedure
    {

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "msgs", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "parentObjID")]
        public string ParentObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ID")]
        public string Id { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "PART_ID")]
        public string PartId { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, Size = 100, ParameterName = "QTY")]
        public double? Qty { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "NICK_NAME")]
        public string NickName { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NtLogin { get; set; }

    }
}
