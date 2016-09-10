CREATE PROCEDURE DBO.A_SP_PROD_PRICE_LIST_EXTRA_COST_UPDATE_PRICE_TABLE_ROW
@newID varchar(50) OUTPUT,
@msg varchar(500) OUTPUT,
@PPLEC_ID varchar(50),
@ID varchar(50),
@minNum float,
@maxNum float,
@perUnitCost varchar(50),
@flatRate varchar(50),
@perUnitOver float,
@strNTLogin varchar(50)
AS
print 'Inside A_SP_PROD_PRICE_LIST_EXTRA_COST_UPDATE_PRICE_TABLE_ROW'
print 'First lets delete it if there is no pricing info'
if @perUnitCost is null and @flatRate is null
	begin
	print 'This data is null so it is out of here'
	DELETE FROM A_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE WHERE ID = @ID
	goto fin
	end
print 'must not have deleted it so update it'
UPDATE A_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE
SET
MIN_NUM = @minNum,
MAX_NUM = @maxNum,
PER_UNIT_COST = convert(money,@perUnitCost),
FLAT_RATE = convert(money,@flatRate),
PER_UNIT_APPLIES_OVER = @perUnitOver,
DRCM = getDate(),
modby = @strNTLogin
WHERE ID = @ID



fin:



