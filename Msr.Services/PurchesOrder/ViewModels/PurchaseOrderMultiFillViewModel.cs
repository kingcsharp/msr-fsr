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

        public List<PurchaseOrderFillViewModel> MapToDto(List<PurchaseOrderFill> purchaseOrderFill)
        {
            var purchaseOrderFillViewModelList = new List<PurchaseOrderFillViewModel>();

            foreach (var item in purchaseOrderFill)
            {
                for (int i = 0; i < item.QTY_NEEDS_FILLING; i++)
                {
                    var purchaseOrderFillViewModel = new PurchaseOrderFillViewModel();
                    purchaseOrderFillViewModel.CUST_NAME = item.CUST_NAME;
                    purchaseOrderFillViewModel.SUP_NAME = item.SUP_NAME;
                    purchaseOrderFillViewModel.OBJ_DESC = item.OBJ_DESC;
                    purchaseOrderFillViewModel.FULL_NAME = item.FULL_NAME;
                    purchaseOrderFillViewModel.PRODUCT_ID = item.PRODUCT_ID;
                    purchaseOrderFillViewModel.PURCHASER_ID = item.PURCHASER_ID;
                    purchaseOrderFillViewModel.PROC_NAME = item.PROC_NAME;
                    purchaseOrderFillViewModel.PROC_ID = item.PROC_ID;
                    purchaseOrderFillViewModel.SYS_PROC_ID = item.SYS_PROC_ID;
                    purchaseOrderFillViewModel.APP_OBJ_DESC = item.APP_OBJ_DESC;
                    purchaseOrderFillViewModel.PRICING_TABLE_ID = item.PRICING_TABLE_ID;
                    purchaseOrderFillViewModel.CUSTOMER = item.CUSTOMER;
                    purchaseOrderFillViewModel.SUPPLIER = item.SUPPLIER;
                    purchaseOrderFillViewModel.PURCHASE_QTY = item.PURCHASE_QTY;
                    purchaseOrderFillViewModel.UNIT_PRICE = item.UNIT_PRICE;
                    purchaseOrderFillViewModel.TOTAL_PRICE = item.TOTAL_PRICE;
                    purchaseOrderFillViewModel.DEST = item.DEST;
                    purchaseOrderFillViewModel.FROM_LOC = item.FROM_LOC;
                    purchaseOrderFillViewModel.TO_LOC = item.TO_LOC;
                    purchaseOrderFillViewModel.PURCHASE_HIST_ID = item.PURCHASE_HIST_ID;
                    purchaseOrderFillViewModel.ACCOUNT_ID = item.ACCOUNT_ID;
                    purchaseOrderFillViewModel.TOT_QTY = item.TOT_QTY;
                    purchaseOrderFillViewModel.PARENT_QTY = item.PARENT_QTY;
                    purchaseOrderFillViewModel.WEIGHT = item.WEIGHT;
                    purchaseOrderFillViewModel.WEIGHT_UNIT = item.WEIGHT_UNIT;
                    purchaseOrderFillViewModel.OBJ_PROD_APPLIES_TO = item.OBJ_PROD_APPLIES_TO;
                    purchaseOrderFillViewModel.PROCEDURE_HIST_ID = item.PROCEDURE_HIST_ID;
                    purchaseOrderFillViewModel.STEPS_IN_AP = item.STEPS_IN_AP;
                    purchaseOrderFillViewModel.PROC_NAME = item.PROC_NAME;
                    purchaseOrderFillViewModel.PROD_HIST_ID = item.PROD_HIST_ID;
                    purchaseOrderFillViewModel.CUSTOMER_PERSON = item.CUSTOMER_PERSON;
                    purchaseOrderFillViewModel.PURCHASE_ID = item.PURCHASE_ID;
                    purchaseOrderFillViewModel.ID = item.ID;
                    purchaseOrderFillViewModel.PURCH_ITEM_ID = item.PURCH_ITEM_ID;
                    purchaseOrderFillViewModel.FILL_BY = item.FILL_BY;
                    purchaseOrderFillViewModel.FILL_OBJ_ID = item.FILL_OBJ_ID;
                    purchaseOrderFillViewModel.FILL_QTY = item.FILL_QTY;
                    purchaseOrderFillViewModel.FILLER = item.FILLER;
                    purchaseOrderFillViewModel.TASK_ID = item.TASK_ID;
                    purchaseOrderFillViewModel.QTY_FILLED = item.QTY_FILLED;
                    purchaseOrderFillViewModel.QTY_NEEDS_FILLING = item.QTY_NEEDS_FILLING;
                    purchaseOrderFillViewModel.SUB_FILL_FOR = item.SUB_FILL_FOR;
                    purchaseOrderFillViewModel.FILL_DATE = item.FILL_DATE;
                    purchaseOrderFillViewModel.PURCHASE_ITEM_PARENT_ID = item.PURCHASE_ITEM_PARENT_ID;
                    purchaseOrderFillViewModel.PROD_PRICE_LIST = item.PROD_PRICE_LIST;
                    purchaseOrderFillViewModel.CUST_LINE_ITEM = item.CUST_LINE_ITEM;
                    purchaseOrderFillViewModel.BATCH_FILL = item.BATCH_FILL;
                    purchaseOrderFillViewModel.BATCH_PARENT = item.BATCH_PARENT;

                    purchaseOrderFillViewModel.SetUp(new PurchesOrderService());

                    purchaseOrderFillViewModelList.Add(purchaseOrderFillViewModel);
                }
            }
            return purchaseOrderFillViewModelList;
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
