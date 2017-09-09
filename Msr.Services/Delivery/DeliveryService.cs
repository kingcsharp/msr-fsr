using System.Linq;
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
            var sql = string.Format(@"SELECT DISTINCT t.TASK_ID as TaskId,t.PURCHASE_ITEM_ID as PurchaseItemId,t.CUST_PURCH_NUM as CustPurchNum,t.COMPANY_PART_NUMBER as PartNumber,t.CUST_NAME as CustName,t.PART_DESC as PartName,t.PRODUCT_NAME as ProductName,t.LOCATION_NAME as LocationName, t.DUE_DATE FROM A_V_SHIPPER_TASKS t
                WHERE SYSTEM_TASK = 'SYS_RECEIVE' AND (  (REQUESTEE_ID IS NULL AND GROUP_REQUESTEE_ID IN ('','1502','1490','1498','1506','1510','1494')) OR  REQUESTEE_ID = '{0}' ) 
                UNION SELECT DISTINCT t.TASK_ID as TaskId,t.PURCHASE_ITEM_ID as PurchaseItemId,t.CUST_PURCH_NUM as CustPurchNum,t.COMPANY_PART_NUMBER as PartNumber,t.CUST_NAME as CustName,t.PART_DESC as PartName,t.PRODUCT_NAME as ProductName,t.LOCATION_NAME as LocationName, t.DUE_DATE FROM A_V_SHIPPING_TASK_FROM_BATCH  t WHERE SYSTEM_TASK = 'SYS_RECEIVE' AND
                ((REQUESTEE_ID IS NULL AND GROUP_REQUESTEE_ID IN ('','1502','1490','1498','1506','1510','1494')) OR  REQUESTEE_ID = '{0}' ) ORDER BY DUE_DATE",loginId);

            var result = _dbContext.Database.SqlQuery<DeliveryScreenView>(sql).ToList().AsQueryable();

            return result;
        }
    }
}
