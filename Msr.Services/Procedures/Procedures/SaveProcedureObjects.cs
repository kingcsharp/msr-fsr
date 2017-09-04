using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Procedures.Procedures
{
    [StoredProcedure("A_SP_PROCEDURE_OBJECT_LINK_ADD")]
    public class SaveProcedureObjects
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ID")]
        public string Id { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PROCEDURE_OBJ_ID")]
        public string ProcedureObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PROC_ID")]
        public string ProcId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "STEP_ID")]
        public string StepId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 4000, ParameterName = "APPROVED_OBJECT_ID")]
        public string ApprovedObjectId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, ParameterName = "QTY ")]
        public string Qty { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, ParameterName = "QTY_TYPE")]
        public string QtyType { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "RELATIONSHIP ")]
        public string Relationship { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "LABOR_ROLE")]
        public string LaborRole { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
