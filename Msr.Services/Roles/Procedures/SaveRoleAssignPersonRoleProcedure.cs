using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Roles.Procedures
{
    [StoredProcedure("A_SP_ROLES_ASSIGN_PERSON_TO_A_ROLE")]
    public class SaveRoleAssignPersonRoleProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "child")]
        public string Child { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strID")]
        public string StrId { get; set; }

        [StoredProcedureParameter(SqlDbType.DateTime, ParameterName = "StartDate")]
        public DateTime? StartDate { get; set; }

        [StoredProcedureParameter(SqlDbType.DateTime, ParameterName = "EndDate")]
        public DateTime? EndDate { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
