using System;
using System.Linq;
using System.Text;
using Msr.Models.Delivery;
using Msr.Repositories;

namespace Msr.Services.Delivery
{
    public class DeliveryService
    {
        private readonly MsrDbContext _dbContext;

        public DeliveryService()
        {
            _dbContext = new MsrDbContext();

        }

        public IQueryable<DeliveryScreenView> GetDeliveryScreenDataToView(string loginId)
        {
            var sql = $@"SELECT DISTINCT t.TASK_ID as TaskId,t.PURCHASE_ITEM_ID as PurchaseItemId,t.CUST_PURCH_NUM as CustPurchNum,t.COMPANY_PART_NUMBER as PartNumber,t.CUST_NAME as CustName,t.PART_DESC as PartName,t.PRODUCT_NAME as ProductName,t.LOCATION_NAME as LocationName, t.DUE_DATE FROM A_V_SHIPPER_TASKS t
                WHERE SYSTEM_TASK = 'SYS_RECEIVE' AND (  (REQUESTEE_ID IS NULL AND GROUP_REQUESTEE_ID IN ('','1502','1490','1498','1506','1510','1494')) OR  REQUESTEE_ID = '{loginId}' ) 
                UNION SELECT DISTINCT t.TASK_ID as TaskId,t.PURCHASE_ITEM_ID as PurchaseItemId,t.CUST_PURCH_NUM as CustPurchNum,t.COMPANY_PART_NUMBER as PartNumber,t.CUST_NAME as CustName,t.PART_DESC as PartName,t.PRODUCT_NAME as ProductName,t.LOCATION_NAME as LocationName, t.DUE_DATE FROM A_V_SHIPPING_TASK_FROM_BATCH  t WHERE SYSTEM_TASK = 'SYS_RECEIVE' AND
                ((REQUESTEE_ID IS NULL AND GROUP_REQUESTEE_ID IN ('','1502','1490','1498','1506','1510','1494')) OR  REQUESTEE_ID = '{loginId}' ) ORDER BY DUE_DATE";

            var result = _dbContext.Database.SqlQuery<DeliveryScreenView>(sql).ToList().AsQueryable();

            return result;


            ////data = new List<DeliveryScreenViewModel>
            ////{
            ////    new DeliveryScreenViewModel
            ////    {
            ////        TaskId = "1",
            ////        CustName = "ss",
            ////        CustPurchNum = "100",
            ////        LocationName = "abc",
            ////        PartName = "text",
            ////        PartNumber = "200",
            ////        ProcedureName = "Pro",
            ////        ProductName = "Product",
            ////        PurchaseItemId = "300"
            ////    },
            ////    new DeliveryScreenViewModel
            ////    {
            ////        TaskId = "2",
            ////        CustName = "ss",
            ////        CustPurchNum = "100",
            ////        LocationName = "abc",
            ////        PartName = "text",
            ////        PartNumber = "200",
            ////        ProcedureName = "Pro",
            ////        ProductName = "Product",
            ////        PurchaseItemId = "300"
            ////    }

            ////};
        }
    }
}
