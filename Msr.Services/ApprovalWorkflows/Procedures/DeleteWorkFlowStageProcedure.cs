using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.ApprovalWorkflows.Procedures
{
    [StoredProcedure("A_SP_WF_DELETE_STAGES")]
    public class DeleteWorkFlowStageProcedure
    {       
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ID")]
        public string WfId { get; set; }       

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }

    }
}
