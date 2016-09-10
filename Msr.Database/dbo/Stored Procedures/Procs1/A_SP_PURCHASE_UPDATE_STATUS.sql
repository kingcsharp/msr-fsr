




CREATE      PROCEDURE DBO.A_SP_PURCHASE_UPDATE_STATUS
@phID varchar(50)
AS
print 'We are updating the status for Purchase Hist ID = ' + isNULL(@phID,'NULL')
declare @tester varchar(50), @ret varchar(50)
declare @c as CURSOR, @ordID varchar(50)
set @c = CURSOR FOR SELECT ID FROM A_ORDER_ITEMS WHERE PURCHASE_HIST_ID = @phID
open @c
fetch next from @c INTO @ordID
while @@fetch_status = 0
	begin
	exec A_SP_PURCHASE_ITEM_GET_ACCOUNT_STATUS @ret OUTPUT,@ordID
	if @ret = 'ITEM_NEEDS_ACCOUNT'
		begin
		UPDATE A_PURCHASES_HISTORY SET PURCHASE_STATUS = @ret
		goto fin
		end
	fetch next from @c INTO @ordID
	end
close @c
deallocate @c

UPDATE A_PURCHASES_HISTORY SET PURCHASE_STATUS = @ret WHERE ID = @phID

fin:
exec A_SP_PURCHASE_UPDATE_FORECAST_FUNNEL_FOR_PURCHASE_ITEMS @phID,'SYSTEM'





