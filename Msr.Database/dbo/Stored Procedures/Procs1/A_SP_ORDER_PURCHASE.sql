
CREATE              PROCEDURE [dbo].[A_SP_ORDER_PURCHASE] 
@newID varchar(50) OUTPUT,
@msg nvarchar(4000) OUTPUT,
@orderID varchar(50),
@strNTLogin varchar(50)
AS
begin Transaction
declare @purchID varchar(50)
print 'Purchasing order number = ' + @orderID
print 'so first we need to make a purchase'
exec sp_getUniqueID3 @purchID OUTPUT
INSERT INTO A_PURCHASES_HISTORY 
	(ID,DATE_CREATED,PURCHASE_STATUS,ORDER_ID,DRCM,MODBY,PURCHASER)
VALUES
	(@purchID,getDate(),'Creating',@orderID,getDate(),@strNTLogin,@strNTLogin)
if @@ERROR <> 0 goto problem

print 'Created order, adding precedents'
INSERT INTO A_ORDER_ITEM_PRECEDENTS (ID,PREV,FOL,DRCM,MODBY)
SELECT newID(),PREV_ID,FOL_ID,getDate(),@strNTLogin FROM A_V_QUOTE_PRECEDENTS_FROM_ORDER_IDS WHERE PURCHASE_HIST_ID = @purchID

print 'Now I need to set up all the parent child relationships'
UPDATE A_ORDER_ITEMS SET PARENT = dbo.A_FN_QUOTE_ITEM_GET_PARENT_USING_ORDER_ITEM(ID) WHERE PURCHASE_HIST_ID = @purchID
if @@error <> 0 goto PROBLEM


print 'Update the status of the whole purchase now'
exec A_SP_PURCHASE_UPDATE_STATUS @purchID


fin:
print 'Finished the Creating the Purchase with no problem'
SELECT @newID =  OBJECT_ID FROM A_PURCHASES_HISTORY WHERE ID = @purchID
if @@trancount > 0 COMMIT TRANSACTION

return 0

PROBLEM:
print 'There was some sort of problem makig the quote and we are going to not finish'
if @@TRANCOUNT > 0 ROLLBACK TRANSACTION
return 1











