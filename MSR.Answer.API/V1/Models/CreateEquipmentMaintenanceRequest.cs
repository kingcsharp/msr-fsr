using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateEquipmentMaintenanceRequest
    {
        public int? Id { get; set; }

        [Required]
        public int? LocationId { get; set; }

        public int? AssignedToId { get; set; }

        [Required]
        public bool? TroubleState { get; set; }

        public string MaintenanceTask { get; set; }

        [Required]
        public int? StatusId { get; set; }

        public DateTime? PemLastCompletedDate { get; set; }

        public int? FrequencyField { get; set; }

        public string Comments { get; set; }
    }
}
