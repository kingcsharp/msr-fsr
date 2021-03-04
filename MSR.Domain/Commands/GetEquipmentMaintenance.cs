
using MSR.Domain.Commanding;
using System;
using System.ComponentModel.DataAnnotations;

namespace MSR.Domain.Commands
{
    public class GetEquipmentMaintenance : PagingCommand
    {
        public int? Id { get; set; }
        public string LocationName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedFullName { get; set; }
        public string AssignedToFullName { get; set; }
        public bool? TroubleState { get; set; }
        [StringLength(50)]
        public string MaintenanceTask { get; set; }
        public DateTime? PemLastCompletedDate { get; set; }
        public int? FrequencyField { get; set; }
        [StringLength(4000)]
        public string Comments { get; set; }
        public string StatusName { get; set; }
    }
}
