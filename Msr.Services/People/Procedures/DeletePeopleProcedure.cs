using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.People.Procedures
{
    [StoredProcedure("A_SP_OBJECT_UNLOCK_AND_DELETE")]
    public class DeletePeopleProcedure
    {
          
            [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "objID")]
            public string ObjID { get; set; }

            [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
            public string StrNTlogin { get; set; }
        }
}
