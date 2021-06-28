using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class GetPurchaseOrderDBView: PagingCommand
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string CustomerReferencePO { get; set; }
        public decimal? InvoicedBalance { get; set; }
        public decimal? UninvoicedBalance { get; set; }
        public decimal? Balance { get; set; }
        public string CustomerName { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public decimal? TotalPurchaseLimit { get; set; }
        public decimal? UnusedAmount { get; set; }
        public int? Revision { get; set; }
        public string[]? Status { get; set; }
    }
}
