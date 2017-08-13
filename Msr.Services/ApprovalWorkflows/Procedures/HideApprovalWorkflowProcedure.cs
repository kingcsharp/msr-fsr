using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.ApprovalWorkflows.Procedures
{
    [StoredProcedure("Portal_HideApprovalWorkflow")]
    class HideApprovalWorkflowProcedure
    {
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "Id")]
        public string Id { get; set; }
    }
}
