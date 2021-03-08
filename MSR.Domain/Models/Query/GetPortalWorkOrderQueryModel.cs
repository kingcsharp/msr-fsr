using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models.Query
{
    public class GetPortalWorkOrderQueryModel: QueryBase
    {
        public int CustomerId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SubPartName { get; set; }
        public int? PartId { get; set; }
    }
}
