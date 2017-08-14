using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.People.Procedures
{
    [StoredProcedure("A_SP_PEOPLE_UPDATE_PASSWORD_BY_OBJ_ID")]
    public class SavePasswrodProcedure
    {
           
            [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "objID")]
            public string ObjID { get; set; }

            [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "password")]
            public string Password { get; set; }

            [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "strNTLogin")]
            public string StrNTlogin { get; set; }
        }
}
