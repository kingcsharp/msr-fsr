using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.PurchesOrder.ViewModels
{
    public class PurchaseFormAccountViewModel
    {
        public PurchaseFormAccountViewModel()
        {
            Products = new List<string>();
        }

        public string OBJECT_ID { get; set; }

        public string NAME { get; set; }

        public string REFERENCE_PO { get; set; }

        public string REFERENCE_NAME { get; set; }

        public DateTime? OPEN_DATE { get; set; }

        public DateTime? CLOSE_DATE { get; set; }

        public string SUPPLIER_CO { get; set; }

        public string CUSTOMER_CO { get; set; }

        public string CUSTOMER_BILL_CO { get; set; }

        public int? MAXIMUM_USES { get; set; }

        public Decimal? TOTAL_PURCHASE_LIMIT { get; set; }

        public Decimal? CREDIT_LIMIT { get; set; }

        public string APPROVAL_WF { get; set; }

        public string INVOICE_TRIGGER { get; set; }

        public string INVOICE_PERIOD_NUMBER { get; set; }

        public string INVOICE_PERIOD_TYPE { get; set; }

        public DateTime? FIRST_INVOICE_DATE { get; set; }

        public DateTime? NEXT_INVOICE_DATE { get; set; }

        public int? PAYMENT_GRACE_PERIOD { get; set; }

        public Single? LATE_FEE_PERCENTAGE { get; set; }

        public DateTime? DRCM { get; set; }

        public string MODBY { get; set; }

        public string ACCT_TYPE { get; set; }

        public Decimal? TOTAL_PURCHASES { get; set; }

        public Decimal? BALANCE { get; set; }

        public Decimal? AMT_INVOICED { get; set; }

        public string ACCT_STATUS { get; set; }

        public string SUPPLIER_NAME { get; set; }

        public string CUSTOMER_NAME { get; set; }

        public string CUST_BILL_NAME { get; set; }

        public string ID { get; set; }

        public string HISTORY_REF_ID { get; set; }

        public int? REAPPLY_LATE_FEE { get; set; }

        public string PRODUCT_ID { get; set; }

        public string PARENT_ACCOUNT { get; set; }

        public string CREATING_CO { get; set; }

        public string STATUS { get; set; }

        public Decimal? INVOICED_BALANCE { get; set; }

        public Decimal? UNINVOICED_BALANCE { get; set; }

        public Byte? HAS_CHILD { get; set; }

        public Int16? NONCONSUMABLE_INCLUDED { get; set; }

        public Int16? CONSUMABLES_INCLUDED { get; set; }

        public Int16? LABOR_INCLUDED { get; set; }

        public Decimal? TOTAL_CREDITS { get; set; }

        public Decimal? TOTAL_DEBITS { get; set; }

        public string BILLING_EMAIL { get; set; }

        public Double? TAX_RATE { get; set; }

        public string Root { get; set; }

        public List<string> Products { get; set; }

        public List<PurchasePoViewModel> ProductPo { get; set; }

        public void Setup(PurchesOrderService purchesOrderService)
        {
            Products = purchesOrderService.PurchasedOrderProducts(OBJECT_ID).Select(x => x.Id).ToList();

            ProductPo = purchesOrderService.PurchasedOrderProductsPoList(id: ID).ToList();

            foreach (var x in ProductPo)
            {
                if (x.Qty == null || x.Qty <= 0)
                {
                    x.Qty = 0;
                }
            }
        }

    }
}
