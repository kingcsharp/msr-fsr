using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Parts.Procedures
{
    [StoredProcedure("A_SP_PART_TYPES_UPDATE_ONE_PART_TYPE")]
    public class SavePartTypeProcedure
    {
        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "newObjID", Direction = ParameterDirection.Output)]
        public string NewObjID { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "objID")]
        public string ObjID { get; set; }      

        [StoredProcedureParameter(SqlDbType.NVarChar, ParameterName = "NAME")]
        public string Name { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "SPARE")]
        public string Spare { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "CONSUMABLE")]
        public string Consumable { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "UNIT")]
        public string Unit { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "UNIT_SHIPPING_WEIGHT")]
        public string UnitShippingWeight { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "strNTlogin")]
        public string StrNTlogin { get; set; }
    }
}
