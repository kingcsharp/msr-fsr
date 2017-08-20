using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.People.Procedures
{
    [StoredProcedure("A_SP_PEOPLE_UPDATE_ONE_PERSON")]
    public class EditPeopleProcedure
    {
            [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "newObjID", Direction = ParameterDirection.Output)]
            public string NewObjId { get; set; }

            [StoredProcedureParameter(SqlDbType.VarChar, Size = 500, ParameterName = "messages", Direction = ParameterDirection.Output)]
            public string Messages { get; set; }

            [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "objID")]
            public string ObjID { get; set; }

            [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "LOGIN")]
            public string Login { get; set; }

            [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "NAME")]
            public string Name { get; set; }

            [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PASSWORD")]
            public string Password { get; set; }

            [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "LAST_NAME")]
            public string LastName { get; set; }

            [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "LANG")]
            public string Language { get; set; }

            [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "COMPANY")]
            public string Company { get; set; }

            [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "POSITION")]
            public string Position { get; set; }

            [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "BOSS")]
            public string Boss { get; set; }

            [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "TIME_ZONE")]
            public string TimeZone { get; set; }

            [StoredProcedureParameter(SqlDbType.DateTime, ParameterName = "HIRE_DATE")]
            public string HireDate { get; set; }

            [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "STATUS")]
            public string Status { get; set; }

            [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "SCREEN_TYPE")]
            public string ScreenType { get; set; }

            [StoredProcedureParameter(SqlDbType.VarChar, Size = 5, ParameterName = "IS_HEAD")]
            public Int16? IsHead { get; set; }

            [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "strNTLogin")]
            public string StrNTlogin { get; set; }
        }
}
