using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models
{
    public class WorkOrderProduct
    {
        public int ProductId { get; set; }
        public bool SerializeIndividually { get; set; }
        public ICollection<string> SerialNumbers { get; set; }
        public ICollection<string> CustomerLineNumbers { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }

        public WorkOrderProduct(int productId, bool serializeIndividually, ICollection<string> serialNumbers, ICollection<string> customerLineNumbers, int qty, decimal price)
        {
            ProductId = productId;
            SerializeIndividually = serializeIndividually;
            SerialNumbers = serialNumbers;
            CustomerLineNumbers = customerLineNumbers;
            Qty = qty;
            Price = price;
        }   
    }
}
