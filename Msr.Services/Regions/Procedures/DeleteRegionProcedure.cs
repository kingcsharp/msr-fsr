using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Regions.Procedures
{
    [StoredProcedure("A_SP_REGIONS_UPDATE_ONE_REGION")]
    public class DeleteRegionProcedure
    {
        //work on delete is still pending
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "objID")]
        public string ObjID { get; set; }
    }
}
