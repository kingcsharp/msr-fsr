using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.ActualParts.Procedures
{
    [StoredProcedure("A_SP_OBJECT_UNLOCK_AND_DELETE")]
    public class DeleteActualPartProcedure
    {
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "ID")]
        public string Id { get; set; }
    }
}
