CREATE PROCEDURE dbo.A_SP_ORDER_ITEM_CHANGE_QTY_WITH_MULTIPLER
@ID varchar(50),
@mult Float,
@strNTLogin varchar(50)
AS
print ' In A_SP_ORDER_ITEM_CHANGE_QTY_WITH_MULTIPLER'
UPDATE A_ORDER_ITEMS SET QTY = (QTY * @mult) WHERE ID = @ID
exec A_SP_ORDER_ITEM_SET_PRICE @ID,@strNTLogin
print 'Now to update all my children and their children'
declare @c as CURSOR, @it varchar(50)
set @c = CURSOR FOR SELECT ID FROM A_ORDER_ITEMS WHERE PARENT = @ID
open @c
fetch next from @c INTO @it
while @@fetch_status = 0
	begin
	print 'Changing the qty of ' + @it
	exec A_SP_ORDER_ITEM_CHANGE_QTY_WITH_MULTIPLER @it,@mult,@strNTLogin
	exec A_SP_ORDER_ITEM_SET_PRICE @it,@strNTLogin
	print 'Changed the Qty of ' + @it
	fetch next from @c INTO @it
	end
close @c
deallocate @c

exec A_SP_ORDER_ITEM_SET_PRICE @ID,@strNTLogin


