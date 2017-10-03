using System;
using System.Data;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.EquipmentMaintenances.Procedures
{
    [StoredProcedure("Portal_CreateEquipmentProcedure")]
    public class CreateEquipmantMaintenanceProcedure
    {
        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewObjID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "messages", Direction = ParameterDirection.Output)]
        public string Messages { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "Id")]
        public string Id { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "ObjectId")]
        public string ObjID { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ScanBarcode")]
        public string ScanBarcode { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "PrimaryLocationId")]
        public string PrimaryLocationId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "SubLocationFirstId")]
        public string SubLocationFirstId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SubLocationSecondId")]
        public string SubLocationSecondId { get; set; }

        [StoredProcedureParameter(SqlDbType.DateTime, ParameterName = "DateTime")]
        public DateTime? DateTime { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "Technician")]
        public string Technician { get; set; }

        [StoredProcedureParameter(SqlDbType.Bit, ParameterName = "TroubleState")]
        public bool? TroubleState { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "MaintenanceTask")]
        public string MaintenanceTask { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 8000, ParameterName = "Comments")]
        public string Comments { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "Status")]
        public string Status { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
