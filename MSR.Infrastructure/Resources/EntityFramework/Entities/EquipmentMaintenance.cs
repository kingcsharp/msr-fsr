using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(EquipmentMaintenance))]
    public partial class EquipmentMaintenance
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string ObjectId { get; set; }

        [StringLength(130)]
        public string ScanBarcode { get; set; }

        [StringLength(100)]
        public string RoomEquipment { get; set; }

        public DateTime? DateTime { get; set; }

        [StringLength(50)]
        public string RequestedById { get; set; }

        [StringLength(50)]
        public string AssignedToId { get; set; }

        public bool? TroubleState { get; set; }

        [StringLength(50)]
        public string MaintenanceTask { get; set; }

        [StringLength(4000)]
        public string Comments { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Status { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string StrNTLogin { get; set; }

        public DateTime? PemLastCompletedDate { get; set; }

        public int? FrequencyField { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
