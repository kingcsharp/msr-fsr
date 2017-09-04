using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.ActualParts.Procedures
{
    [StoredProcedure("A_SP_TASKS_REASSIGN_TASK")]
    public class ReAssignTaskProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "NewID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "Messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ID")]
        public string Id { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "REQUESTEE_ID")]
        public string Requestee_Id { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "GROUP_REQUESTEE_ID")]
        public string Group_Requestee_Id { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 4000, ParameterName = "COMMENTS")]
        public string Comments { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "StrNTLogin")]
        public string StrNTLogin { get; set; }
    }
}
