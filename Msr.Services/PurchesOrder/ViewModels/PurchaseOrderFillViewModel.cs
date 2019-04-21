using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Services.PurchesOrder.ViewModels
{
    public class PurchaseOrderFillViewModel
    {
        public PurchaseOrderFillViewModel()
        {

        }
        public string CUST_NAME { get; set; }

        public string SUP_NAME { get; set; }

        public string OBJ_DESC { get; set; }

        public string FULL_NAME { get; set; }

        public string PRODUCT_ID { get; set; }

        public string PURCHASER_ID { get; set; }

        public string PROD_NAME { get; set; }

        public string PROC_ID { get; set; }

        public string SYS_PROC_ID { get; set; }

        public string APP_OBJ_DESC { get; set; }

        public string PRICING_TABLE_ID { get; set; }

        public string CUSTOMER { get; set; }

        public string SUPPLIER { get; set; }

        public Double? PURCHASE_QTY { get; set; }

        public decimal? UNIT_PRICE { get; set; }

        public Single? TOTAL_PRICE { get; set; }

        public string DEST { get; set; }

        public string FROM_LOC { get; set; }

        public string TO_LOC { get; set; }

        public string PURCHASE_HIST_ID { get; set; }

        public string ACCOUNT_ID { get; set; }

        public Single? TOT_QTY { get; set; }

        public Single? PARENT_QTY { get; set; }

        public Double? WEIGHT { get; set; }

        public string WEIGHT_UNIT { get; set; }

        public string OBJ_PROD_APPLIES_TO { get; set; }

        public string PROCEDURE_HIST_ID { get; set; }

        public decimal? STEPS_IN_AP { get; set; }

        public string PROC_NAME { get; set; }

        public string PROD_HIST_ID { get; set; }

        public string CUSTOMER_PERSON { get; set; }

        public string PURCHASE_ID { get; set; }

        public string ID { get; set; }

        public string PURCH_ITEM_ID { get; set; }

        public string FILL_BY { get; set; }

        public string FILL_OBJ_ID { get; set; }

        public Single? FILL_QTY { get; set; }

        public string FILLER { get; set; }

        public string TASK_ID { get; set; }

        public Double? QTY_FILLED { get; set; }

        public Double? QTY_NEEDS_FILLING { get; set; }

        public string SUB_FILL_FOR { get; set; }

        public DateTime? FILL_DATE { get; set; }

        public string PURCHASE_ITEM_PARENT_ID { get; set; }

        public string PROD_PRICE_LIST { get; set; }

        public string CUST_LINE_ITEM { get; set; }

        public Int16? BATCH_FILL { get; set; }

        public string BATCH_PARENT { get; set; }

        public string SERIAL_NUMBER { get; set; }

        public string CO_ID { get; set; }

        public string LOCATION_ID { get; set; }

        public List<SelectListItem> LocationList { get; set; }

        public List<SelectListItem> OwnerList { get; set; }
    }
}
