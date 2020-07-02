using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commands
{
    public class UpdateCustomer: Command
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? LocationId { get; set; }
        public int? PrimaryContactUserId { get; set; }
        public int? SecondaryContactUserId { get; set; }
        public int CustomerId { get; set; }

        public bool? IsActive { get; set; }
    }
}
