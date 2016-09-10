


CREATE   PROCEDURE dbo.A_SP_PURCHASE_QUOTE_SET_ACCOUNT_AND_QTY
@strID varchar(50) OUTPUT,
@strMsgs nvarchar(4000) OUTPUT,
@purchaseID varchar(50),
@quoteID varchar(50),
@strQty float,
@strAcctID varchar(50),
@strDestination varchar(50),
@strNTLogin nvarchar(50)
AS
print 'Setting the account for this purchase and quote'

UPDATE A_ORDER_ITEMS SET ACCOUNT_ID = @strAcctID WHERE PURCHASE_HIST_ID = @purchaseID AND QUOTE_ID = @quoteID AND ADD_COST_ID is not null
UPDATE A_ORDER_ITEMS SET TO_LOC = @strDestination WHERE PURCHASE_HIST_ID = @purchaseID AND QUOTE_ID = @quoteID AND DEST = 'to' AND PROC_SYS_ID = 'SYS_SHIPPING'

declare @curs as cursor,@it as varchar(50),@curQty float,@mult float
set @curs = CURSOR FOR SELECT ID,QTY FROM A_ORDER_ITEMS WHERE PURCHASE_HIST_ID = @purchaseID AND QUOTE_ID =  @quoteID AND PARENT IS NULL
open @curs
fetch next from @curs into @it,@curQty
while @@fetch_status = 0
	begin 
	set @mult = @strQty / @curQty
	exec A_SP_ORDER_ITEM_CHANGE_QTY_WITH_MULTIPLER @it,@mult,@strNTLogin
	fetch next from @curs into @it,@curQty
	end
close @curs
deallocate @curs





