using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.ActualParts.Procedures
{
    [StoredProcedure("A_SP_TASK_DELETE_TASK_FOREVER")]
    public class DeleteHistoryProcedure
    {
          
            [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ID")]
            public string ID { get; set; }

            [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
            public string StrNTlogin { get; set; }
        }
}
