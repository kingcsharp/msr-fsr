










CREATE            PROCEDURE DBO.A_SP_PURCHASE_ITEM_UPDATE_ITEM
@newID varchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@ID varchar(50),
@qty Float,
@acct varchar(50),
@toLoc varchar(50),
@fromLoc varchar(50),
@strNTLogin varchar(50)
AS
print 'Updating a Purchase item'
if not exists(SELECT * FROM A_ORDER_ITEMS WHERE ID = @ID)
	goto fin
declare @parentID varchar(50),@purchID varchar(50),@quoteID varchar(50)
SELECT @parentID = PARENT,
	@purchID = PURCHASE_HIST_ID,@quoteID = QUOTE_ID
	FROM A_ORDER_ITEMS 
	WHERE ID = @ID

if @qty = 0
	begin
	exec A_SP_PURCHASE_ITEM_DELETE_ONE @ID
	goto fin
	end


if @parentID is NULL 
	UPDATE A_ORDER_ITEMS SET 
		ACCOUNT_ID = @acct,
		QTY = @qty,
		TO_LOC = @toLoc,
		FROM_LOC = @fromLoc
		WHERE ID = @ID
else
	UPDATE A_ORDER_ITEMS SET 
		ACCOUNT_ID = @acct,
		TO_LOC = @toLoc,
		FROM_LOC = @fromLoc
		WHERE ID = @ID
	
exec A_SP_ORDER_ITEM_UPDATE_QTYS @ID


exec A_SP_PURCHASE_QUOTE_UPDATE_STATUS @purchID,@quoteID,@strNTLogin

fin:





