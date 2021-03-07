using System;
using System.Collections.Generic;

namespace MSR.Answer.API.V1.Models
{
    public class GetPurchaseOrderRequest:BaseApiModel
    {
        public int? Id { get; set; }
        public string Name { get;set;}
        public string CustomerReferencePO { get;set;}
        public decimal? InvoicedBalance { get;set;}
        public decimal? UninvoicedBalance { get;set;}
        public decimal? Balance { get;set;}
        public string CustomerName { get;set;}
        public DateTime? OpenDate { get;set;}
        public DateTime? CloseDate { get;set;}
        public decimal? TotalPurchaseLimit { get;set;}
        public decimal? UnusedAmount { get;set;}
        public int? Revision { get;set;}
        public string[]? Status { get; set; }

    } 
}
