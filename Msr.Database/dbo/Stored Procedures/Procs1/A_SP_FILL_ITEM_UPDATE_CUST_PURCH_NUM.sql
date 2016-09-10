
CREATE  PROCEDURE dbo.A_SP_FILL_ITEM_UPDATE_CUST_PURCH_NUM
@fillItemID varchar(50),
@CUST_PURCH_NUM varchar(100),
@strNTLogin varchar(50)
AS
print 'Updating the Cust PO to ' + @CUST_PURCH_NUM + ' for fill item = ' + @fillItemID
declare @purchaseItemID varchar(50),@purchaseHistID varchar(50)
SELECT @purchaseItemID = PURCH_ITEM_ID	FROM A_FILLS WHERE ID = @fillItemID
SELECT @purchaseHistID = PURCHASE_HIST_ID FROM A_ORDER_ITEMS WHERE ID = @purchaseItemID
UPDATE A_PURCHASES_HISTORY SET CUST_PURCH_NUM = @CUST_PURCH_NUM WHERE ID = @purchaseHistID

SELECT 'SUCCESSFULLY UPDATED CUST_PURCH_NUM' AS RESULT

