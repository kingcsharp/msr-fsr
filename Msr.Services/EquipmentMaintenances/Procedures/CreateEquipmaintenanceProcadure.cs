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

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "ParentLocation")]
        public string PrimaryLocationId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 100, ParameterName = "SubLocationFirst")]
        public string SubLocationFirstId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "SubLocationSecond")]
        public string SubLocationSecondId { get; set; }

        [StoredProcedureParameter(SqlDbType.DateTime, ParameterName = "DateTime")]
        public DateTime? DateTime { get; set; }

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

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "ApprovedById")]
        public string ApprovedById { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "RequestedById")]
        public string RequestedById { get; set; }

        [StoredProcedureParameter(SqlDbType.DateTime, ParameterName = "PMLastCompletedDate")]
        public DateTime? PMLastCompletedDate { get; set; }

        [StoredProcedureParameter(SqlDbType.Int, ParameterName = "FrequencyField")]
        public int? FrequencyField { get; set; }
    }
}
