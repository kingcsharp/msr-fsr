using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? PrimaryContactUserId { get; set; }
        public int? SecondaryContactUserId { get; set; }
        public int? LocationId { get; set; }
    }
}
