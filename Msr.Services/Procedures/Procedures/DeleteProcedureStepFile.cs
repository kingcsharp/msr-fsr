using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Procedures.Procedures
{
    [StoredProcedure("A_SP_PROCEDURE_STEP_DELETE_FILE_LINKS")]
    public class DeleteProcedureStepFile
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "stepID")]
        public string StepId { get; set; }
        
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
