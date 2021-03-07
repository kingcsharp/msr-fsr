using MSR.Domain.Commanding;
using System;
using System.ComponentModel.DataAnnotations;

namespace MSR.Domain.Commands
{
    public class GetPurchases : PagingCommand
    {
        public int? Id;
        [StringLength(10)]
        public string Mttn { get; set; }
        public string PurchaseOrderProductName { get; set; }
        public string[]? StatusId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
