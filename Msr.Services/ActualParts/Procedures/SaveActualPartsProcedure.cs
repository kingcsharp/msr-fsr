using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.ActualParts.Procedures
{
    [StoredProcedure("A_SP_ACTUAL_PARTS_UPDATE_PART")]
    public class SaveActualPartsProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Message { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "objID")]
        public string ObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PART_ID")]
        public string PartId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "QTY")]
        public double? Qty { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 1000, ParameterName = "SERIAL")]
        public string Serial { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 1000, ParameterName = "NICK_NAME")]
        public string NickName { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "LOCATION_ID")]
        public string LocationId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "CUR_OWNER")]
        public string CurOwner { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "AP_STATUS")]
        public string ApStatus { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 8000, ParameterName = "PRODUCTS")]
        public string Products { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PARENT_ID")]
        public string ParentId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 10, ParameterName = "subpartAction")]
        public string SubPartAction { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "responsiblePerson")]
        public string ResponsiblePerson { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
