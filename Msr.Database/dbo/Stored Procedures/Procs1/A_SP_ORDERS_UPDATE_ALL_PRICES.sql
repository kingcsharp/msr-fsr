CREATE PROCEDURE dbo.A_SP_ORDERS_UPDATE_ALL_PRICES
AS
declare @curs cursor,@it varchar(50)
set @curs = CURSOR for SELECT DISTINCT OBJECT_ID FROM A_ORDERS_HISTORY
open @curs
fetch next from @curs into @it
while @@fetch_status = 0 
begin
	exec A_SP_ORDER_ASSIGN_PRICE @it
	fetch next from @curs into @it
end
close @curs
deallocate @curs

