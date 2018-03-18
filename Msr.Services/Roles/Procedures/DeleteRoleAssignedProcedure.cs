using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Roles.Procedures
{
    [StoredProcedure("A_SP_ROLES_DELETE_ONE_ROLES_ASSIGNEES")]
    public class DeleteRoleAssignedProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strID")]
        public string ObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
