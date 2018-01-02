using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Services.Invoices.ViewModel
{
   public class InvoiceDetailEditViewModel
    {
        public InvoiceDetailEditViewModel()
        {
            TypeList = new List<SelectListItem>();
        }
        public string ID { get; set; }

        [DisplayName("Invoice Number :")]
        public string INVOICE_ID { get; set; }

        [DisplayName("Account Number :")]
        public string ACCOUNT_ID { get; set; }

        public string PURCH_ITEM_ID { get; set; }

        public string STATUS { get; set; }

        public DateTime? DRCM { get; set; }

        public string MODBY { get; set; }

        [DisplayName("Total Amount :")]
        public Decimal? AMOUNT { get; set; }

        [DisplayName("Description :")]
        public string DESCRIPTION { get; set; }

        public DateTime? DATE_INVOICED { get; set; }

        public Decimal? AMT_PAID { get; set; }

        public Decimal? LATE_FEES { get; set; }

        public Decimal? TOTAL { get; set; }

        public string PURCHASER_ID { get; set; }

        public string DISPUTED_LINK { get; set; }

        [DisplayName("Comments :")]
        public string COMMENTS { get; set; }

        public string PURCHASE_ID { get; set; }


        [DisplayName("Type :")]
        public string ITEM_TYPE { get; set; }

        public string CUSTOMER_CO { get; set; }

        public string SUPPLIER_CO { get; set; }

        public string QUOTE_ID { get; set; }

        [DisplayName("Post Date :")]
        public DateTime? DATE_POSTED { get; set; }

        [DisplayName("Customer PO number :")]
        public string CUST_SINGLE_PO { get; set; }

        [DisplayName("Customer Line Item :")]
        public string CUST_LINE_ITEM { get; set; }

        public Byte? FAILED_MONITOR { get; set; }

        public string FILL_ID { get; set; }

        public Decimal? TAX { get; set; }

        [DisplayName("Tax Percentage :")]
        public Double? TAX_RATE { get; set; }

        [DisplayName("Qty :")]
        public Double? QTY { get; set; }

        [DisplayName("Unit Price :")]
        public Decimal? UNIT_PRICE { get; set; }

        public List<SelectListItem> TypeList { get; set; }

        public void SetUp(InvoicesService invoicesService, string invoiceId, string accId, string ntLogin)
        {
            ACCOUNT_ID = accId;

            INVOICE_ID = invoiceId;

            QTY = 1;

            TypeList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "DEBIT",
                    Value = "INV_ITEM_DEBIT",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "CREADIT",
                    Value = "INV_ITEM_CREDIT"
                },

            };
        }
    }
}
