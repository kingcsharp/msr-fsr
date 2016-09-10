

CREATE  PROCEDURE A_SP_PROD_PRICE_LIST_UPDATE_VOL_DISCOUNTS
@newID varchar(50) OUTPUT,
@id varchar(50),
@disc varchar(50),
@qty varchar(50),
@price_list varchar(50),
@strNTLogin varchar(50)
AS
declare @pID as varchar(50)
SELECT @pID = ID FROM A_PROD_PRICE_LIST_HISTORY WHERE OBJECT_ID = @price_list
print 'pID = ' + @pID
if @id is not null and @disc is null
	begin
	DELETE FROM A_PROD_PRICE_LIST_VOLUME_DISC WHERE ID = @ID
	goto fin
	end

if @id is null and @disc is null
	begin
	goto fin
	end

if @id is null
	begin
	exec sp_GetUniqueID3 @newID OUTPUT
	INSERT INTO A_PROD_PRICE_LIST_VOLUME_DISC (ID,PRICE_LIST_ID,DRCM,MODBY) VALUES (@newID,@pID,getDate(),@strNTLogin)
	end
else
	begin
	SELECT @newID = @id
	end
UPDATE A_PROD_PRICE_LIST_VOLUME_DISC SET
PRICE_LIST_ID = @pID,
DISCOUNT = @disc,
MIN_PUR_QTY = @qty,
DRCM = getDate(),
MODBY = @strNTLogin
WHERE ID = @newID

fin:


