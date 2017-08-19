using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Roles.Procedures
{
    [StoredProcedure("A_SP_OBJECT_CHECK_FOR_VALIDITY")]
   public class SaveRoleProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 1000, ParameterName = "returnVal", Direction = ParameterDirection.Output)]
        public string Returnid { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 1000, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Message { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "objID")]
        public string Id { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
