CREATE PROCEDURE dbo.A_SP_PURCHASE_ITEM_DELETE_ONE
@ID varchar(50)
AS
declare @curs as cursor,@it varchar(50)
set @curs = CURSOR FOR SELECT ID FROM A_ORDER_ITEMS WHERE PARENT = @ID
open @curs
fetch next from @curs into @it
while @@fetch_status = 0
	begin
	print 'DEleting child'
	exec A_SP_PURCHASE_ITEM_DELETE_ONE @it
	fetch next from @curs into @it
	end

DELETE FROM A_ORDER_ITEMS WHERE ID = @ID

