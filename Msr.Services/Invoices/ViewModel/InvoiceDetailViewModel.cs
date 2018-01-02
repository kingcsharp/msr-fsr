using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Services.Invoices.ViewModel
{
   public class InvoiceDetailViewModel
    {
        public InvoiceDetailViewModel()
        {
            InvoiceStatusList = new List<SelectListItem>();
        }

        [Key]
        public string INVOICE_ID { get; set; }

        public string ACCT_NAME { get; set; }

        public DateTime? INVOICE_DATE { get; set; }

        public string STATUS { get; set; }

        public Decimal? AMT_PAID { get; set; }

        public Decimal? NEW_ITEMS_AMT { get; set; }

        public Decimal? PREVIOUS_BALANCE { get; set; }

        public Decimal? PAYMENT_AMOUNT { get; set; }

        public Decimal? DISPUTED_AMOUNT { get; set; }

        public Decimal? TOTAL_DUE { get; set; }

        public DateTime? DUE_DATE { get; set; }

        public string ACCT_TYPE { get; set; }

        public string CREATING_CO { get; set; }

        public string REFERENCE_PO { get; set; }

        public string REFERENCE_NAME { get; set; }

        public DateTime? OPEN_DATE { get; set; }

        public DateTime? CLOSE_DATE { get; set; }

        public string SUPPLIER_CO { get; set; }

        public string CUSTOMER_CO { get; set; }

        public string CUSTOMER_BILL_CO { get; set; }

        public Decimal? TOTAL_PURCHASE_LIMIT { get; set; }

        public Decimal? CREDIT_LIMIT { get; set; }

        public string SUPPLIER_NAME { get; set; }

        public string CUSTOMER_NAME { get; set; }

        public string CUST_BILL_NAME { get; set; }

        public string PRODUCT_ID { get; set; }

        public string PRODUCT_NAME { get; set; }

        public string ACCOUNT_ID { get; set; }

        public Decimal? LATE_FEES { get; set; }

        public Decimal? TOTAL_PURCHASES { get; set; }

        public DateTime? DATE_SENT_TO_CUSTOMER { get; set; }

        public string PURCHASE_ID { get; set; }

        public string INVOICE_NAME { get; set; }

        public DateTime? CREATE_DATE { get; set; }

        public string INVOICE_TYPE { get; set; }

        public int? MAXIMUM_USES { get; set; }

        public string INVOICE_TRIGGER { get; set; }

        public string INVOICE_PERIOD_NUMBER { get; set; }

        public string INVOICE_PERIOD_TYPE { get; set; }

        public DateTime? FIRST_INVOICE_DATE { get; set; }

        public DateTime? NEXT_INVOICE_DATE { get; set; }

        public int? PAYMENT_GRACE_PERIOD { get; set; }

        public Single? LATE_FEE_PERCENTAGE { get; set; }

        public Decimal? AMT_INVOICED { get; set; }

        public Decimal? BALANCE { get; set; }

        public Decimal? INVOICED_BALANCE { get; set; }

        public Decimal? UNINVOICED_BALANCE { get; set; }

        public string BILLING_EMAIL { get; set; }

        public Decimal? TOTAL_DEBITS { get; set; }

        public Decimal? TOTAL_CREDITS { get; set; }

        public string CUST_PURCH_NUM { get; set; }

        public Decimal? INVOICE_BALANCE { get; set; }

        public Decimal? TOTAL_TAX { get; set; }

        public string PO_NUMBER { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public string SALES_TAX { get; set; }

        public string ADD_EMAILS { get; set; }

        public string CUST_EMAIL { get; set; }

        public List<InvoiceDetailListViewModel> invoiceDetailList { get; set; }

        public List<SelectListItem> InvoiceStatusList { get; set; }

     
        public void SetUp(InvoicesService invoicesService, string ntLogin)
        {
            invoiceDetailList = invoicesService.InvoiceDetailListById(id: INVOICE_ID).ToList();


            InvoiceStatusList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Creating",
                    Value = "CREATING",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "Invoiced",
                    Value = "INVOICED"
                },
                new SelectListItem
                {
                    Text = "SentToCust",
                    Value = "SENT_TO_CUSTOMER"
                },
                new SelectListItem
                {
                    Text = "PaidInFull",
                    Value = "PAID_IN_FULL"
                },
            };
        }

      
    }
}
