using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class PurchaseProductMap: Entity
    {
        [ForeignKey("PurchaseId")]
        public virtual Purchase Purchase { get; set; }
        public int PurchaseId { get; set; }

        [ForeignKey("PurchaseOrderProductId")]
        public virtual PurchaseOrderProduct PurchaseOrderProduct { get; set; }
        public int PurchaseOrderProductId { get; set; }
    }
}
