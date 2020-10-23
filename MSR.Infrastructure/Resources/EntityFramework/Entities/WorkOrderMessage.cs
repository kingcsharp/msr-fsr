using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkOrderMessage))]
    public class WorkOrderMessage: CreatableEntity
    {
        public virtual int WorkOrderId { get; set; }
        [ForeignKey("WorkOrderId")]
        public virtual WorkOrder WorkOrder { get; set; }
        public string Message { get; set; }

    }
}
