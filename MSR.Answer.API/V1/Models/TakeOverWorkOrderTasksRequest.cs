using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class TakeOverWorkOrderRequest
    {
        public int WorkOrderId { get; set; }
        public int UserId { get; set; }
    }
}
