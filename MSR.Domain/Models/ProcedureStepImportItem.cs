using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models
{
    public class ProcedureStepImportItem
    {
        public int? Id { get; set; }
        public int ProcedureId { get; set; }
        public string ProcedureName { get; set; }
        public string Title { get; set; }
        public string StepText { get; set; }
        public int ProcedureStepTypeId { get; set; }
        public int PrintOrder { get; set; }
        public double? LaborTime { get; set; }
        public double? EquipmentTime { get; set; }
        public decimal? RepacementCost { get; set; }
        public Single? Utilization { get; set; }
        public int? UsefulLife { get; set; }
        public string RoleIds { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
