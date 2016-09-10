
CREATE   PROCEDURE DBO.A_SP_PURCHASE_ITEM_UPDATE_QTY
@ID varchar(50),
@strNTLogin varchar(50)
AS
print 'entering A_SP_PURCHASE_ITEM_UPDATE_QTY '
declare @parentPurchItem varchar(50)
SELECT @parentPurchItem = PARENT_ID FROM A_PURCHASE_ITEMS WHERE ID = @ID
if @parentPurchItem is not null
	begin
	declare @quoteItemID varchar(50),@purchID varchar(50)
	SELECT @quoteItemID = QUOTE_ITEM_ID, @purchID = PURCHASE_ID FROM A_PURCHASE_ITEMS WHERE ID = @ID
	declare @qty float
	SELECT @qty = QTY FROM A_QUOTE_ITEMS WHERE ID = @quoteItemID
	UPDATE A_PURCHASE_ITEMS SET QTY = @qty * (SELECT QTY FROM A_PURCHASE_ITEMS WHERE ID = @parentPurchItem) WHERE ID = @ID
	end
else
	begin
	declare @curs as cursor
	declare @it varchar(50)
	set @curs = cursor for SELECT ID FROM A_PURCHASE_ITEMS WHERE PARENT_ID = @ID
	open @curs
	fetch next from @curs into @it
	while @@fetch_status = 0
		begin
		exec A_SP_PURCHASE_ITEM_UPDATE_QTY @it,@strNTLogin
		fetch next from @curs into @it
		end
	close @curs
	deallocate @curs
	end
print 'Exiting A_SP_PURCHASE_ITEM_FIGURE_PRICE'
