using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.QueryFilters
{
    public class InvoiceFilter
    {
        public int? Id { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string LastUpdatedByName { get; set; }
        public decimal? Amount { get; set; }
        public int? StatusId { get; set; }
    }
}
