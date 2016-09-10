
CREATE  PROCEDURE dbo.A_SP_ACCOUNT_PURCHASE_UPDATE_ITEM
@purchObjID varchar(50),
@orderItemID varchar(50),
@qty float,
@acctID varchar(50),
@strNTLogin varchar(50)
AS
print 'Updating an Order Item on a purchase'
print 'Order Item ID = ' + isNull(@orderItemID,'NULL')
declare @phID varchar(50)
if @orderItemID is null
	begin
	SELECT @phID = ID FROM A_PURCHASES_HISTORY WHERE OBJECT_ID = @purchObjID
	SELECT @orderItemID = ID FROM A_ORDER_ITEMS WHERE PURCHASE_HIST_ID = @phID and PARENT IS NULL 
	end
else
	begin
	SELECT @phID = ID FROM A_PURCHASES_HISTORY WHERE OBJECT_ID = @purchObjID
	end
print '--------------Updating the item'
exec A_SP_PURCHASE_ITEM_UPDATE_ITEM 
null, --@newID varchar(50) OUTPUT,
null, --@messages nvarchar(2000) OUTPUT,
@orderItemID, --@ID varchar(50),
@qty, --@qty Float,
null, --@acct varchar(50),
null, --@toLoc varchar(50),
null, --@fromLoc
@strNTLogin --@strNTLogin varchar(50)
SELECT * FROM A_ORDER_ITEMS 
		WHERE PURCHASE_HIST_ID = @phID and ADD_COST_ID IS not NULL
declare @curs as cursor, @it varchar(50)
set @curs = CURSOR FOR 
	SELECT ID FROM A_ORDER_ITEMS 
		WHERE PURCHASE_HIST_ID = @phID and ADD_COST_ID IS not NULL
open @curs
fetch next from @curs into @it
while @@fetch_status = 0
	begin
	print '------------UPDATING ORDER ITEM -------- ' + @it
	UPDATE A_ORDER_ITEMS SET ACCOUNT_ID = @acctID WHERE ID = @it
	fetch next from @curs into @it
	end
close @curs
deallocate @curs

exec A_SP_PURCHASE_QUOTE_UPDATE_STATUS @phID,null,@strNTLogin




