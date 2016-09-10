CREATE PROCEDURE dbo.A_SP_PURCHASES_UPDATE_ALL_ACCOUNTS_ON_PURCHASE_ITEMS
@pHistID varchar(50),
@strNTLogin varchar(50)
AS
declare @acctID varchar(50)
SELECT @acctID = ACCT_FOR_ALL FROM A_PURCHASES_HISTORY WHERE ID = @pHistID

print 'This runs after the purchase hist table is updated with the account for all things on this purchase'
declare @curs as cursor,@it varchar(50)
set @curs = CURSOR FOR SELECT ID FROM A_ORDER_ITEMS WHERE PURCHASE_HIST_ID = @pHistID
open @curs
fetch next from @curs into @it
while @@fetch_status = 0
	begin
	print 'Updating item = '+ @it
	UPDATE A_ORDER_ITEMS SET ACCOUNT_ID = @acctID, DRCM = getDate(), MODBY = @strNTLogin WHERE ID = @it
	fetch next from @curs into @it
	end


fin:
exec A_SP_PURCHASE_UPDATE_STATUS @pHistID--,@strNTLogin
SELECT * FROM A_PURCHASES_HISTORY WHERE ID = @pHistID


