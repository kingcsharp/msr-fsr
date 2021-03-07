using System;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class GetPurchasesRequest: BaseApiModel
    {
        public int? Id { get; set; }
        [StringLength(10)]
        public string Mttn { get; set; }
        public string PurchaseOrderProductName { get;set;}
        public string[]? StatusId { get;set;}
        public DateTime? CreatedOn { get;set;}
    }
}
