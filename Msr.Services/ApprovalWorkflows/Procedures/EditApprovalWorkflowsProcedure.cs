using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.ApprovalWorkflows.Procedures
{
    [StoredProcedure("A_SP_WF_UPDATE_ONE_WORKFLOW")]
    public class EditApprovalWorkflowsProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "msg", Direction = ParameterDirection.Output)]
        public string msg { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "objID", Direction = ParameterDirection.Output)]
        public string objID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string newID { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 500, ParameterName = "NAME")]
        public string Name { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "ID")]
        public string Id { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 500, ParameterName = "AP_STAMP")]
        public string AP_STAMP { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "AP_STAMP_PIC")]
        public string AP_STAMP_PIC { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }

    }
}
