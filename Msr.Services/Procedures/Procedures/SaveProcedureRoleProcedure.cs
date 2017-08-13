using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Procedures.Procedures
{
    [StoredProcedure("A_SP_PROCEDURES_ADD_ROLE_TO_VIEW")]
    class SaveProcedureRoleProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "procedureObjID")]
        public string ObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.Char, Size = 20, ParameterName = "roleID")]
        public string RoleId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
