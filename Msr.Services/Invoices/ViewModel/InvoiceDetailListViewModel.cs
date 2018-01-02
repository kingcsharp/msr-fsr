using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Invoices.ViewModel
{
   public class InvoiceDetailListViewModel
    {
        public string PURCHASER_NAME { get; set; }

        public string CUST_PURCH_NUM { get; set; }

        public string SUP_PURCH_NUM { get; set; }

        public DateTime? DATE_CREATED { get; set; }

        public string PURCHASE_STATUS { get; set; }

        public string ID { get; set; }

        public string INVOICE_ID { get; set; }

        public string ACCOUNT_ID { get; set; }

        public string PURCH_ITEM_ID { get; set; }

        public string STATUS { get; set; }

        public DateTime? DRCM { get; set; }

        public string MODBY { get; set; }

        public Decimal? AMOUNT { get; set; }

        public string DESCRIPTION { get; set; }

        public DateTime? DATE_INVOICED { get; set; }

        public Decimal? AMT_PAID { get; set; }

        public Decimal? LATE_FEES { get; set; }

        public Decimal? TOTAL { get; set; }

        public string PURCHASER_ID { get; set; }

        public string DISPUTED_LINK { get; set; }

        public string COMMENTS { get; set; }

        public string PURCHASE_ID { get; set; }

        public string ITEM_TYPE { get; set; }

        public string CUSTOMER_CO { get; set; }

        public string SUPPLIER_CO { get; set; }

        public string QUOTE_ID { get; set; }

        public DateTime? DATE_POSTED { get; set; }

        public string CUST_LINE_ITEM { get; set; }

        public string FILL_ID { get; set; }

        public Byte? FAILED_MONITOR { get; set; }

        public string CUSTOMER_NAME { get; set; }

        public string SUPPLIER_NAME { get; set; }

        public Decimal? TAX { get; set; }

        public Double? TAX_RATE { get; set; }

        public Double? QTY { get; set; }

        public Decimal? UNIT_PRICE { get; set; }



    }
}
