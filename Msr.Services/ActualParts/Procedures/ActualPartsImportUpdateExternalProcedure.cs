using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.ActualParts.Procedures
{
    [StoredProcedure("A_SP_ACTUAL_PARTS_IMPORT_AND_UPDATE_AN_EXTERNAL_ACTUAL_PART")]
    public class ActualPartsImportUpdateExternalProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "msgs", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "externalActualPartUniqueID")]
        public string Id { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "externalPartID")]
        public string PartId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "externalOwnerID")]
        public string PartOwner { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "externalPartSN")]
        public string PartSN { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "externalPartNickName")]
        public string PartNickName { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, ParameterName = "qty")]
        public string PartQty { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "externalLocID")]
        public string LocationId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "parentID")]
        public string ParentId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NtLogin { get; set; }
    }
}
