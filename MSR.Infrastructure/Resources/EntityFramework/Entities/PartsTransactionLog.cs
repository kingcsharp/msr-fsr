using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(PartsTransactionLog))]
    public partial class PartsTransactionLog
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PartId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string SerialNumber { get; set; }

        public int? PurchaseHistoryId { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaskId { get; set; }
    }
}
