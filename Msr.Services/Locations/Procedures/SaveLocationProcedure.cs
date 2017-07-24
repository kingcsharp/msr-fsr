using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Locations.Procedures
{
    [StoredProcedure("A_SP_LOCATIONS_UPDATE_ONE_LOCATION")]
    public class SaveLocationProcedure
    {
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "newObjID", Direction = ParameterDirection.Output)]
        public string NewObjID { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 500, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "objID")]
        public string ObjID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 1000, ParameterName = "NAME")]
        public string Name { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "PARENT")]
        public string Parent { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 300, ParameterName = "ADDRESS_1")]
        public string Address1 { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 300, ParameterName = "ADDRESS_2")]
        public string Address2 { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 150, ParameterName = "CITY")]
        public string City { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "STATE")]
        public string State { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "COUNTRY")]
        public string Country { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "POSTAL_CODE")]
        public string PostalCode { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "REGION")]
        public string Region { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 300, ParameterName = "INTERNAL_ADDRESS")]
        public string InternalAddress { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "strNTlogin")]
        public string NTLogin { get; set; }
    }
}
