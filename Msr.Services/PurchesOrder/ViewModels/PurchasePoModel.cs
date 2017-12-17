using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Services.PurchesOrder.ViewModels
{
  public class PurchasePoModel
    {
        public PurchasePoModel()
        {
            NewPoList = new List<SelectListItem>();
            AcctForAllList = new List<SelectListItem>();
            OrderItems = new List<PurchasePoModel>();
        }
        public string ID { get; set; }

        public DateTime? DATE_CREATED { get; set; }

        public string PURCHASE_STATUS { get; set; }

        public string oldId { get; set; }

        public string newId { get; set; }

        public string ORDER_ID { get; set; }

        public DateTime? DRCM { get; set; }

        public string MODBY { get; set; }

        public string OBJECT_ID { get; set; }

        public string LOCKED_BY { get; set; }

        public string UNLOCKED_BY { get; set; }

        public string CREATED_BY { get; set; }

        public DateTime? CREATE_DATE { get; set; }

        public string ROOT { get; set; }

        public string CREATING_CO { get; set; }

        public string STATUS { get; set; }

        public string LOCKED_BY_NAME { get; set; }

        public string CREATING_CO_NAME { get; set; }

        public string CUSTOMER_PERSON { get; set; }

        public string CUSTOMER_CO { get; set; }

        public string DESCRIPTION { get; set; }

        public Decimal? PURCHASE_TOTAL { get; set; }

        public string PURCHASER { get; set; }

        public string PURCHASING_CO { get; set; }

        public string CUST_PURCH_NUM { get; set; }

        public string SUP_PURCH_NUM { get; set; }

        public int? REV { get; set; }

        public string CUST_LINE_ITEM { get; set; }

        public string PROD_SHOW_NAME { get; set; }

        public string ACCT_FOR_ALL { get; set; }

        public DateTime? DUE_DATE { get; set; }

        public string PRODUCT_NAME { get; set; }

        public string PROC_NAME { get; set; }

        public string ACCT_NAME { get; set; }

        public Double? QTY { get; set; }

        public Decimal? TOTAL_PRICE { get; set; }

        public Decimal? UNIT_PRICE { get; set; }

        public string NewPo { get; set; }

        public DateTime? all_date { get; set; }

       public List<PurchasePoModel> OrderItems { get; set; }

        public List<SelectListItem> NewPoList { get; set; }

        public List<SelectListItem> AcctForAllList { get; set; }

        public void Setup(PurchesOrderService purchesOrderService)
        {

            NewPoList = purchesOrderService.PurchasedOrderPoDropDownList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            AcctForAllList = purchesOrderService.PurchasedOrderPoAcctForAllList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();

            ACCT_FOR_ALL = oldId;
        }


        }
    }
