using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Invoices.ViewModel
{
   public class InvoiceViewPurchaseItemViewModels
    {
        public int? SRT { get; set; }

        public int? TREE_LEVEL { get; set; }

        public Int16? TREE_HAS_CHILD { get; set; }

        public Int16? EXPANDED { get; set; }

        public string TRAV_FROM_ID { get; set; }

        public string TRAV_FROM_NAME { get; set; }

        public string SHIP_TO_ID { get; set; }

        public string SHIP_TO_NAME { get; set; }

        public string TRAV_TO_ID { get; set; }

        public string TRAV_TO_NAME { get; set; }

        public string SHIP_FROM_ID { get; set; }

        public string SHIP_FROM_NAME { get; set; }

        public string PREC { get; set; }

        public string LOCKED_BY { get; set; }

        public string STATUS { get; set; }

        public string TO_LOC_NAME { get; set; }

        public string FROM_LOC_NAME { get; set; }

        public string PRODUCT_NAME { get; set; }

        public string SUPPLIER_ID { get; set; }

        public string SUPPLIER_NAME { get; set; }

        public string PROCEDURE_ID { get; set; }

        public string PROC_NAME { get; set; }

        public string OBJ_PROD_APPLIES_TO { get; set; }

        public string HISTORY_REF_ID { get; set; }

        public string PROC_TYPE { get; set; }

        public string ORDER_OBJ_ID { get; set; }

        public string PARENT_LIST { get; set; }

        public string SYSTEM_ID { get; set; }

        public string OBJ_TABLE { get; set; }

        public string WT_UNIT_NAME { get; set; }

        public string ID { get; set; }

        public string PARENT { get; set; }

        public Single? PARENT_QTY { get; set; }

        public string ORDER_ID { get; set; }

        public string PRODUCT_ID { get; set; }

        public string PROD_PRICE_LIST { get; set; }

        public string QUOTE_ID { get; set; }

        public string ADD_COST_ID { get; set; }

        public Single? TOTAL_QTY { get; set; }

        public Double? QTY { get; set; }

        public Decimal? UNIT_PRICE { get; set; }

        public Decimal? UNIT_ESTIMATE { get; set; }

        public string COMMENTS { get; set; }


        public Decimal? TOTAL_PRICE { get; set; }

        public string DEST { get; set; }

        public string FROM_LOC { get; set; }

        public string TO_LOC { get; set; }

        public Double? SPECIAL_DISCOUNT { get; set; }

        public string SPECIAL_DISC_REASON { get; set; }

        public Double? EXPEDITE_PRODUCTION { get; set; }

        public Decimal? FLAT_RATE { get; set; }

        public string EX_DESC { get; set; }

        public Double? EST_WEIGHT { get; set; }

        public string EST_WEIGHT_UNIT { get; set; }

        public string PPL_HIST_ID { get; set; }

        public Byte? RECURRING { get; set; }

        public string RECUR_PERIOD { get; set; }

        public int? RECUR_COUNT { get; set; }


        public DateTime? RECUR_START_DATE { get; set; }

        public DateTime? RECUR_STOP_DATE { get; set; }

        public string RECUR_ACCOUNT { get; set; }

        public Byte? RECUR_AUTO_FILL { get; set; }

        public string SOURCE_ID { get; set; }

        public string PURCHASE_HIST_ID { get; set; }

        public string ACCOUNT_ID { get; set; }

        public string ACCT_NAME { get; set; }

        public Int16? PROC_SYS_ID { get; set; }

        public string MIN_QUANTITY { get; set; }

        public string CAPACITY { get; set; }

        public string CAPACITY_UNIT { get; set; }

        public Double? PRODUCTION_TIME { get; set; }

        public string PRODUCTION_TIME_UNTI { get; set; }

        public string PROD_UNIT { get; set; }

        public string PRICE_LIST_TYPE { get; set; }

        public string PROD_SHOW_NAME { get; set; }

        public string BILL_TYPE { get; set; }

        public string OBJ_PROD_APPLIES_TO_ID { get; set; }

        public string UNIT { get; set; }

        public string NAME { get; set; }

        public string COMAPNY_PART_NUMBER { get; set; }

        public DateTime? DUE_DATE { get; set; }

        public DateTime? ORIG_DUE_DATE { get; set; }

        public DateTime? ACT_DUE_DATE { get; set; }

        public Double? PROD_TIME { get; set; }

        public string PROD_TIME_UNIT { get; set; }

        public string CUST_LINE_ITEM { get; set; }

        public string APP_OBJ_DESC { get; set; }

        public string SUPPLIER_ROOT_CO_NAME { get; set; }

        public List<InvoiceViewPurchaseItemViewModels> invoiceViewItemList { get; set; }


        public void SetUp(InvoicesService invoicesService, string purchaseId, string ntLogin)
        {

            invoiceViewItemList = invoicesService.InvoiceViewItemById(strPurchaseID: purchaseId, ntLogin: ntLogin).ToList();
        }
    }
}
