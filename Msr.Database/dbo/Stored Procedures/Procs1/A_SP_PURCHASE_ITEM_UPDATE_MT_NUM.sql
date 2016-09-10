


CREATE     procedure dbo.A_SP_PURCHASE_ITEM_UPDATE_MT_NUM
@purchItemID varchar(50),
@myNum varchar(50),
@updateAll varchar(50),
@strNTLogin varchar(50)
AS
print 'Updating a number'
declare @myPurchID varchar(50)
SELECT @myPurchID = PURCHASE_HIST_ID FROM A_ORDER_ITEMS WHERE ID = @purchItemID
if @updateAll = 'true'
	begin
	UPDATE A_ORDER_ITEMS SET MT_NUM = @myNum WHERE PURCHASE_HIST_ID = @myPurchID
	SELECT ID AS PURCHASE_ITEM_ID FROM A_ORDER_ITEMS WHERE PURCHASE_HIST_ID = @myPurchID
	end
else
	begin
	UPDATE A_ORDER_ITEMS SET MT_NUM = @myNum WHERE ID = @purchItemID
	SELECT ID AS PURCHASE_ITEM_ID FROM A_ORDER_ITEMS WHERE ID = @purchItemID
	end




fin:




