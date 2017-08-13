using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Roles.Procedures
{
    [StoredProcedure("A_SP_OBJECT_UNLOCK_AND_DELETE")]
    public class DeleteRoleProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "objID")]
        public string ObjID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
