using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.EquipmentMaintenances.Procedures
{
    [StoredProcedure("sp_GetUniqueID3")]
    public class GetIdProcedure
    {
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "ID", Direction = ParameterDirection.Output)]
        public string NewID { get; set; }
    }
}
