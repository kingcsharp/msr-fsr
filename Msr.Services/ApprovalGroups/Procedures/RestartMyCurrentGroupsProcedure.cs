using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.ApprovalGroups.Procedures
{
    [StoredProcedure("A_SP_WF_GROUP_RESTART_ALL_MY_CURRENT_GROUPS_THAT_ARE_STARTED")]
    public class RestartMyCurrentGroupsProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "groupID")]
        public string GroupId { get; set; }
    }
}
