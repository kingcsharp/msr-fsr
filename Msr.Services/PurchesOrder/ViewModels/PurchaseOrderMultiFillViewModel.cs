using Msr.Models.PurchesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Msr.Services.PurchesOrder.ViewModels
{
    public class PurchaseOrderMultiFillViewModel
    {
        public PurchaseOrderMultiFillViewModel()
        {
            PurchaseApprovedData = new PurchaseApprovedData();
            FillList = new List<PurchaseOrderFillViewModel>();
            KitCountList = new List<SelectListItem>();
        }
        public PurchaseApprovedData PurchaseApprovedData { get; set; }
        public List<PurchaseOrderFillViewModel> FillList { get; set; }
        public string KitCount { get; set; }
        public List<SelectListItem> KitCountList { get; set; }

        public void SetSelectListsForAllPurchaseOrders(List<PurchaseOrderFill> purchaseOrderFill)
        {

        }
        /// <summary>
        /// For every purchaseOrderFill if the Qty needs filling is >1 it creates a new purchaseOrderFillViewModel
        /// For every purchaseOrder it sets 2 select Lists
        /// </summary>
        /// <param name="purchaseOrderFill"></param>
        /// <returns></returns>
        public void SetList(List<PurchaseOrderFillViewModel> purchaseOrderFill, List<SelectListItem> locationList, List<SelectListItem> ownerList)
        {
            var purchaseOrderFillViewModelList = new List<PurchaseOrderFillViewModel>();

            foreach (var item in purchaseOrderFill)
            {
                for (int i = 0; i < item.QTY_NEEDS_FILLING; i++)
                {
                    if (i > 0)
                    {
                        var purchaseOrderFillViewModel = new PurchaseOrderFillViewModel
                        {
                            CUST_NAME = item.CUST_NAME,
                            SUP_NAME = item.SUP_NAME,
                            OBJ_DESC = item.OBJ_DESC,
                            FULL_NAME = item.FULL_NAME,
                            PRODUCT_ID = item.PRODUCT_ID,
                            PURCHASER_ID = item.PURCHASER_ID,
                            PROC_NAME = item.PROC_NAME,
                            PROC_ID = item.PROC_ID,
                            SYS_PROC_ID = item.SYS_PROC_ID,
                            APP_OBJ_DESC = item.APP_OBJ_DESC,
                            PRICING_TABLE_ID = item.PRICING_TABLE_ID,
                            CUSTOMER = item.CUSTOMER,
                            SUPPLIER = item.SUPPLIER,
                            PURCHASE_QTY = item.PURCHASE_QTY,
                            UNIT_PRICE = item.UNIT_PRICE,
                            TOTAL_PRICE = item.TOTAL_PRICE,
                            DEST = item.DEST,
                            FROM_LOC = item.FROM_LOC,
                            TO_LOC = item.TO_LOC,
                            PURCHASE_HIST_ID = item.PURCHASE_HIST_ID,
                            ACCOUNT_ID = item.ACCOUNT_ID,
                            TOT_QTY = item.TOT_QTY,
                            PARENT_QTY = item.PARENT_QTY,
                            WEIGHT = item.WEIGHT,
                            WEIGHT_UNIT = item.WEIGHT_UNIT,
                            OBJ_PROD_APPLIES_TO = item.OBJ_PROD_APPLIES_TO,
                            PROCEDURE_HIST_ID = item.PROCEDURE_HIST_ID,
                            STEPS_IN_AP = item.STEPS_IN_AP,
                            PROD_HIST_ID = item.PROD_HIST_ID,
                            CUSTOMER_PERSON = item.CUSTOMER_PERSON,
                            PURCHASE_ID = item.PURCHASE_ID,
                            ID = item.ID,
                            PURCH_ITEM_ID = item.PURCH_ITEM_ID,
                            FILL_BY = item.FILL_BY,
                            FILL_OBJ_ID = item.FILL_OBJ_ID,
                            FILL_QTY = item.FILL_QTY,
                            FILLER = item.FILLER,
                            TASK_ID = item.TASK_ID,
                            QTY_FILLED = item.QTY_FILLED,
                            QTY_NEEDS_FILLING = item.QTY_NEEDS_FILLING,
                            SUB_FILL_FOR = item.SUB_FILL_FOR,
                            FILL_DATE = item.FILL_DATE,
                            PURCHASE_ITEM_PARENT_ID = item.PURCHASE_ITEM_PARENT_ID,
                            PROD_PRICE_LIST = item.PROD_PRICE_LIST,
                            CUST_LINE_ITEM = item.CUST_LINE_ITEM,
                            BATCH_FILL = item.BATCH_FILL,
                            BATCH_PARENT = item.BATCH_PARENT,
                            LocationList = locationList,
                            OwnerList = ownerList,
                        };
                        this.FillList.Add(purchaseOrderFillViewModel);
                    }
                    else
                    {
                        item.LocationList = locationList;
                        item.OwnerList = ownerList;
                        this.FillList.Add(item);
                    }
                }
            }

            this.FillList = purchaseOrderFill;
            SetUp();
        }


        public void SetUp()
        {
            KitCountList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "1",
                    Value = "1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "2",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "3",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = "4",
                    Value = "4"
                },
                new SelectListItem
                {
                    Text = "5",
                    Value = "5"
                },
                new SelectListItem
                {
                    Text = "6",
                    Value = "6"
                },
                new SelectListItem
                {
                    Text = "7",
                    Value = "7"
                },
                new SelectListItem
                {
                    Text = "8",
                    Value = "8"
                },
                new SelectListItem
                {
                    Text = "9",
                    Value = "9"
                },
                new SelectListItem
                {
                    Text = "10",
                    Value = "10"
                }
            };
        }
    }
}
