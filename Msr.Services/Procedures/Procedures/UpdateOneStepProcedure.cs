using System.Data;
using System.Web.UI;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Procedures.Procedures
{
    [StoredProcedure("Portal_A_SP_PROCEDURE_STEP_UPDATE_ONE_STEP")]
    public class UpdateOneStepProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ID")]
        public string Id { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "STEP_TEXT")]
        public string StepText { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "PROC_OBJ_ID")]
        public string ProcObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 2000, ParameterName = "COMMENTS")]
        public string Comments { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "START_ON_COUNTER")]
        public string StartOnCounter { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "COUNTER_VALUE")]
        public string CounterValue { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "COUNTER_UNIT")]
        public string CounterUnit { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "FROM_START_OR_STOP")]
        public string FromStartOrStop { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "REL_OR_ABS")]
        public string RelOrAbs { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SYSTEM_TASK")]
        public string SystemTask { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "DESTINATION")]
        public string Destination { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SPECIFIC_LOCATION")]
        public string SpecificLocation { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "REFERENCE_VERB")]
        public string ReferenceVerb { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "REFERENCE_OBJECT")]
        public string ReferenceObject { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "REFERENCE_THEORIES")]
        public string ReferenceTheories { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "GOTO_STEP")]
        public string GoToStep { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "GOTO_STEP_ID")]
        public string GoToStepId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "CYCLES")]
        public string Cycles { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "CYCLE_ON_COUNTER")]
        public string CycleOnCounter { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "CYCLE_COUNT")]
        public string CycleCount { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "CYCLE_UNIT")]
        public string CycleUnit { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ReferenceProcs")]
        public string ReferenceProcs { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "precedingSteps")]
        public string PrecedingSteps { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, ParameterName = "DURATION")]
        public double? Duration { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "DURATION_TYPE")]
        public string DurationType { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ReplacementCost")]
        public decimal? ReplacementCost { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "Utilization")]
        public decimal? Utilization { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "UsefulLife")]
        public decimal? UsefulLife { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "EquipExpensePerMinute")]
        public decimal? EquipExpensePerMinute { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "AnnualRM")]
        public decimal? AnnualRM { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "RMPerMinute")]
        public decimal? RMPerMinute { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 5000, ParameterName = "Title")]
        public string Title { get; set; }
    }
}
