using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreateIndividualInvoices : Command
    {
        public ICollection<CreateOneInvoice> Invoices { get; set; }
    }
}
