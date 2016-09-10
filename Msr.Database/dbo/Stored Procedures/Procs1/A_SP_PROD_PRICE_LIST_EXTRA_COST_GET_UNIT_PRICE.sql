CREATE PROCEDURE DBO.A_SP_PROD_PRICE_LIST_EXTRA_COST_GET_UNIT_PRICE
@ID varchar(50)
AS
print 'Finding the unit price'
declare @flatRate money,@perUnitCost money
SELECT top 1 @flatRate = FLAT_RATE, @perUNitCost = PER_UNIT_COST 
	FROM A_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE WHERE
		(
			(MIN_NUM < 1.0 AND MAX_NUM >= 1.0) OR
			(MIN_NUM is NULL AND MAX_NUM IS NULL) OR
			(MIN_NUM < 1.0 AND MAX_NUM IS NULL) 
		)
		and PPLEC_ID = @ID

if @flatRate is null and @perUnitCost is null
	begin
	print 'Could not find a price for 1'
	SELECT TOP 1 @flatRate = FLAT_RATE, @perUNitCost = PER_UNIT_COST 
		FROM A_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE WHERE PPLEC_ID = @ID ORDER BY MIN_NUM,MAX_NUM
	end

if @flatRate is not null
	begin
	UPDATE A_PROD_PRICE_LIST_EXTRA_COSTS SET UNIT_PRICE = @flatRate WHERE ID = @ID
	end
else
	begin
	UPDATE A_PROD_PRICE_LIST_EXTRA_COSTS SET UNIT_PRICE = @perUnitCost WHERE ID = @ID
	end

declare @pplID varchar(50)
SELECT @pplID = PROD_PRICE_LIST FROM A_PROD_PRICE_LIST_EXTRA_COSTS WHERE ID = @ID
exec A_SP_PROD_PRICE_LIST_SET_MY_PRICE @pplID





