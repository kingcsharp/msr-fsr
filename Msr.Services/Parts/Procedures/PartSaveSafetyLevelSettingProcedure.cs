using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Parts.Procedures
{
    [StoredProcedure("A_SP_PART_SAVE_SAFETY_LEVEL_SETTINGS")]
    public class PartSaveSafetyLevelSettingProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "locID")]
        public string LocId { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, ParameterName = "MIN_LEVEL")]
        public double? MinLevel { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, ParameterName = "MIN_WARNING_LEVEL")]
        public double? MinWarningLevel { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, ParameterName = "MAX_WARNING_LEVEL")]
        public double? MaxWarningLevel { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, ParameterName = "MAX_LEVEL")]
        public double? MaxLevel { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "partObjID")]
        public string PartObjId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string StrNTLogin { get; set; }
    }
}
