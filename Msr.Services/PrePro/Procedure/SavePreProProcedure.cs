using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.PrePro.Procedure
{
    [StoredProcedure("A_SP_PROCEDURE_STEP_UPDATE_ONE_STEP")]
    public class SavePreProProcedure
    {

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newID ", Direction = ParameterDirection.Output)]
        public string newID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string messages { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ID")]
        public string ID { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "STEP_TEXT")]
        public string STEP_TEXT { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PROC_OBJ_ID")]
        public string PROC_OBJ_ID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "COMMENTS")]
        public string COMMENTS { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "START_ON_COUNTER")]
        public string START_ON_COUNTER { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "COUNTER_VALUE")]
        public string COUNTER_VALUE { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "COUNTER_UNIT")]
        public string COUNTER_UNIT { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "FROM_START_OR_STOP")]
        public string FROM_START_OR_STOP { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "REL_OR_ABS")]
        public string REL_OR_ABS { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SYSTEM_TASK")]
        public string SYSTEM_TASK { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "DESTINATION")]
        public string DESTINATION { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "SPECIFIC_LOCATION")]
        public string SPECIFIC_LOCATION { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "REFERENCE_VERB")]
        public string REFERENCE_VERB { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "REFERENCE_OBJECT")]
        public string REFERENCE_OBJECT { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "REFERENCE_THEORIES")]
        public string REFERENCE_THEORIES { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "GOTO_STEP")]
        public string GOTO_STEP { get; set; }

        [StoredProcedureParameter(SqlDbType.TinyInt, ParameterName = "GOTO_STEP_ID")]
        public string GOTO_STEP_ID { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "CYCLES")]
        public string CYCLES { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "CYCLE_ON_COUNTER")]
        public string CYCLE_ON_COUNTER { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "CYCLE_COUNT")]
        public string CYCLE_COUNT { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "CYCLE_UNIT")]
        public string CYCLE_UNIT { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "ReferenceProcs")]
        public string ReferenceProcs { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "precedingSteps")]
        public string precedingSteps { get; set; }
     
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "DURATION ")]
        public float? DURATION { get; set; }
       
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "DURATION_TYPE  ")]
        public string DURATION_TYPE { get; set; }
        
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }

    }
}
