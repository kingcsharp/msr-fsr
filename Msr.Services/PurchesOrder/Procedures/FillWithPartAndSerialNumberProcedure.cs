using EntityFrameworkExtras.EF6;
using System;
using System.Data;

namespace Msr.Services.PurchesOrder.Procedures
{
    [StoredProcedure("A_SP_FILL_FILL_WITH_PART_AND_SERIAL_NUMBER")]
    public class FillWithPartAndSerialNumberProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "fillID")]
        public string FillId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strPartID")]
        public string StrPartId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 1000, ParameterName = "strSN")]
        public string StrSN { get; set; }

        [StoredProcedureParameter(SqlDbType.Float, ParameterName = "strQty")]
        public Double? StrQty { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "strLoc")]
        public string StrLoc { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strOwner")]
        public string StrOwner { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string StrNtLogin { get; set; }

    }
}
