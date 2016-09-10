
CREATE  PROCEDURE DBO.A_SP_PROD_PRICE_LIST_EXTRA_COST_UPDATE
@newID varchar(50) OUTPUT,
@msgs varchar(500) OUTPUT,
@pplID varchar(50),
@ID varchar(50),
@desc varchar(2000),
@priceListType varchar(50),
@unit varchar(50),
@action varchar(50),
@strNTLogin varchar(50)
AS
print 'In A_SP_PROD_PRICE_LIST_EXTRA_COST_UPDATE'

if @ID is null
	begin
		print 'ID is Null we need to create this Extra Cost'
		exec sp_GetUniqueID3 @newID OUTPUT
		INSERT INTO A_PROD_PRICE_LIST_EXTRA_COSTS (ID,PROD_PRICE_LIST,MODBY,DRCM) 
			VALUES(@newID,@pplID,@strNTLogin,getDATE())
		set @ID = @newID
	end
set @newID = @ID
print 'Updating ' + @ID
UPDATE A_PROD_PRICE_LIST_EXTRA_COSTS SET 
DESCRIPTION = @desc,
PRICE_LIST_TYPE = @priceListType,
UNIT = @unit,
DRCM = getDate(),
MODBY = @strNTLogin
WHERE ID = @ID


if @action = 'addRow'
	begin
	print 'adding a new row to the pricing table'
	INSERT INTO A_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE (ID,PPLEC_ID)
		VALUES(newID(),@ID)
	end
exec A_SP_PROD_PRICE_LIST_EXTRA_COST_GET_UNIT_PRICE @ID


