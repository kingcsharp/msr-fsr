using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Procedures.Procedures
{
    [StoredProcedure("A_SP_ADMIN_SQL_TO_RUN_QUE_UP")]
    public class AdminSqlToRunQueUpProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 7500, ParameterName = "mySQL")]
        public string MySql { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
