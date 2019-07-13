using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Invoices
{
    public class InvoiceQuickbooksFileModel
    {

        public InvoiceView InvoiceView { get; set; }

        public string InvoiceFileName
        {
            get
            {
                return string.Format("{0}.iif", InvoiceView.InvoiceNumber.Trim().ToUpper());
            }
        }

        public string InvoiceQuickbooksFileText { get; set; }

    }
}
