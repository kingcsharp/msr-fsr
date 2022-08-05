using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class PurchaseRequest
    {
        public int PurchaseOrderId { get; set; }

        public int StatusId { get; set; }

        public int PurchaseOrderProductId { get; set; }

        public string CustomerPurchaseNumber { get; set; }

        public int LocationId { get; set; }

        public List<string> SerialNumbers { get; set; }

        public List<string> CustomerLineNumbers { get; set; }

        public int Qty { get; set; }

        public string MTTN { get; set; }

        public DateTime DueDate { get; set; }

        public decimal PurchasePrice { get; set; }

        public bool SerializeIndividually { get; set; }
    }
}
