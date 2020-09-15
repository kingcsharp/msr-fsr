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
    }
}
