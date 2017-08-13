using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.PrePro.Procedure
{
    [StoredProcedure("A_SP_PREPOP_UPDATE_ONE_PREPOP")]
    public class PreProSelectProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newObjID", Direction = ParameterDirection.Output)]
        public string newObjID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string messages { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "objID")]
        public string objID { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "procStepID")]
        public string procStepID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string strNTLogin { get; set; }

       
    }
}
