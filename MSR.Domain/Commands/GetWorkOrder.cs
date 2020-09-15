using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class GetWorkOrder : Command
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public int? LocationId { get; set; }
        public DateTime? InvoiceDate { get; set; }

        /// <summary>
        /// Only include work orders with the following statuses
        /// </summary>
        public ICollection<StatusModel> statuses { get; set; }

        /// <summary>
        /// If true, include work orders NOT IN set of statuses
        /// </summary>
        /// <description>
        /// This is required because work order status is calculated
        /// based on the cumulative state of ALL the tasks.  To avoid
        /// the work order being listed in both "history" and "menu",
        /// the sets need not intersect.  This can be set to true
        /// to accomplish this.
        /// </description>
        public bool invertStatusSet { get; set; }
    }
}
