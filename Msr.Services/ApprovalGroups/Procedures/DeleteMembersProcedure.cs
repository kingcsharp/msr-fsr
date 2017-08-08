using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.ApprovalGroups.Procedures
{
    [StoredProcedure("A_SP_WF_GROUP_DELETE_MEMBERS")]
    public class DeleteMembersProcedure
    {
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "ID")]
        public string Id { get; set; }
    }
}
