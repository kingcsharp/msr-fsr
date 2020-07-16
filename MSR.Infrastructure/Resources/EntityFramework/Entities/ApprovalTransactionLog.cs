using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ApprovalTransactionLog))]
    public class ApprovalTransactionLog: Entity
    {
        public string ApprovalEntity { get; set; }
        public int ApprovalEntityId { get; set; }
        public string ApprovalResult { get; set; }
        [ForeignKey("ProcessedById")]
        public virtual User ProcessedBy { get; set; }
        public string Comments { get; set; }
        public int ProcessedById { get; set; }
        public DateTimeOffset ProcessedOn { get; set; }
    }
}
