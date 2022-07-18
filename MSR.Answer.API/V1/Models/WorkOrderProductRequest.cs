using System.Collections.Generic;

namespace MSR.Answer.API.V1.Models
{
    public class WorkOrderProductRequest
    {
        public int ProductId { get; set; }
        public bool SerializeIndividually { get; set; }
        public ICollection<string> SerialNumbers { get; set; }
        public ICollection<string> CustomerLineNumbers { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
