using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Roles.Procedures
{
    [StoredProcedure("A_SP_ROLES_UPDATE_ONE_ROLE")]
    public class SaveUserRoleProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "returnID", Direction = ParameterDirection.Output)]
        public string ReturnID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "msg")]
        public string Message { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ID")]
        public string Id { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "NAME")]
        public string Name { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SECURITY_LEVEL")]
        public string SecurityLevel { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "REFERENCE_FILES")]
        public string ReferenceFiles { get; set; }
        
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 100, ParameterName = "TrainingIDRev")]
        public string TrainingIDRev { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 4000, ParameterName = "COMMENTS")]
        public string Comments { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
