CREATE PROCEDURE dbo.A_SP_PROD_PRICE_LIST_QUICK_ADD_UNIT_COST 
@pplObjID varchar(50),
@UNIT_COST varchar(50),
@DESC nvarchar(50),
@costType varchar(50),
@UNIT varchar(50),
@strNTLogin varchar(50)
AS
print 'UNIT COST = ' + @UNIT_COST
declare @pplID varchar(50)
SELECT @pplID = ID FROM A_PROD_PRICE_LIST_HISTORY WHERE OBJECT_ID = @pplObjID
print 'Adding a unit cost for ppl obj ID = ' + @pplObjID
declare @newID varchar(50),@msgs nvarchar(500)
exec A_SP_PROD_PRICE_LIST_EXTRA_COST_UPDATE 
	@newID OUTPUT,@msgs OUTPUT,
	@pplID,NULL,@DESC,@costType,@UNIT,NULL,@strNTLogin
declare @lineID varchar(50)
exec sp_GetUniqueID3 @lineID OUTPUT
print 'Adding the row'
INSERT INTO A_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE (ID,PPLEC_ID,PER_UNIT_COST)
		VALUES(@lineID,@newID,convert(money,@UNIT_COST))

declare @nID varchar(50)
EXEC A_SP_PROD_PRICE_LIST_EXTRA_COST_UPDATE_PRICE_TABLE_ROW
@nID OUTPUT,@msgs OUTPUT,
@newID ,@lineID,null,NULL,@UNIT_COST,null,null,@strNTLogin


exec A_SP_PROD_PRICE_LIST_EXTRA_COST_GET_UNIT_PRICE @NewID

