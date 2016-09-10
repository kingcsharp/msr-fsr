
CREATE  PROCEDURE dbo.A_SP_ACCOUNT_INVOICE_ITEM_CREATE_NAME
@newName nvarchar(2000) OUTPUT,
@invoiceItemID varchar(50)
AS
declare @purchItemID varchar(50),@taskID varchar(50),@purchHistID varchar(50),@purchID varchar(50)
SELECT @purchItemID = PURCH_ITEM_ID FROM A_ACCOUNT_INVOICE_ITEMS 
	WHERE ID = @invoiceItemID
SELECT @purchHistID = PURCHASE_HIST_ID FROM A_ORDER_ITEMS WHERE ID = @purchItemID
SELECT @purchID = ID FROM A_PURCHASES WHERE HISTORY_REF_ID = @purchHistID

print '@purchItemID = ' + @purchItemID
print '@purchHistID = ' + @purchHistID
print '@purchID = ' + @purchID


--SELECT @purchItemID = PARENT FROM A_ORDER_ITEMS WHERE ID = @purchItemID
SELECT @newName = isNull(PROD_SHOW_NAME,'') + ' ' + isNull(EX_DESC,'') FROM A_V_ORDER_ITEMS_ALL_DATA WHERE ID = @purchItemID
print '@newName = ' + @newName


