using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Procedures.Procedures
{
    [StoredProcedure("A_SP_MONITOR_TEMPLATES_UPDATE_TEMPLATE")]
    public class AddProcedureMonitorProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ID")]
        public string Id { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "MONITOR_TYPE")]
        public string Monitor_Type { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "Input_Type")]
        public string Input_Type { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "LIST_SOURCE")]
        public string List_Source { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "DESCRIPTION")]
        public string Description { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "START_SYSTEM_TASK")]
        public string Start_System_Task { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "START_TYPE")]
        public string Start_Type { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "STOP_SYSTEM_TASK")]
        public string Stop_System_Task { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "STOP_TYPE")]
        public string Stop_Type { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "COUNTER_OR_CLOCK")]
        public string Counter_Or_Clock { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "CLOCK_UNIT")]
        public string Clock_Unit { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "HIGHEST_THRESHOLD")]
        public float? Highest_Threshold { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "HIGH_THRESHOLD")]
        public float? High_Threshold { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "TARGET")]
        public float? Target { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "LOW_THRESHOLD")]
        public float? Low_Threshold { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "LOWEST_THRESHOLD")]
        public float? Lowest_Threshold { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SHOULD_BE")]
        public string Should_Be { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "OPINION")]
        public string Opinion { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "HIDE_TARGET")]
        public string Hide_Target { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "USE_RESULT")]
        public string Use_Result { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "FAIL_STOP")]
        public string Fail_Stop { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "STEP_ID")]
        public string Step_Id { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "CORRECT_ANSWER")]
        public string Correct_Answer { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "TEXT_TARGET")]
        public string Text_Target { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "TASK_ID")]
        public string Task_Id { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "TOLERANCE")]
        public string Tolerance { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "RELATED_OBJECT_ID")]
        public string Related_Object_Id { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "FAIL_ACTION")]
        public string Fail_Action { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "TARGET_OBJECT_TYPE")]
        public string Target_Object_Type { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "TARGET_OBJECT")]
        public string Target_Object { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "SKIP_MODE")]
        public string Skip_Mode { get; set; }

        [StoredProcedureParameter(SqlDbType.TinyInt, ParameterName = "CANT_CHANGE")]
        public byte? Cant_Change { get; set; }

        [StoredProcedureParameter(SqlDbType.TinyInt, ParameterName = "ALWAYS_PASS")]
        public byte? Always_Pass { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "StrNTLogin")]
        public string StrNTLogin { get; set; }
    }
}
