using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Companies.Procedures
{
    [StoredProcedure("A_SP_OBJECT_UNLOCK_AND_DELETE")]
    public class DeleteCompanyProcedure
    {      

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "objID")]
        public string ObjID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string StrNTlogin { get; set; }
    }
}
