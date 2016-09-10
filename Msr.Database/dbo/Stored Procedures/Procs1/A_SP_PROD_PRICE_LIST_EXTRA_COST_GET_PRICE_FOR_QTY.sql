


CREATE    PROCEDURE DBO.A_SP_PROD_PRICE_LIST_EXTRA_COST_GET_PRICE_FOR_QTY
@totalPrice money OUTPUT,
@flatRate money OUTPUT,
@perUNitCost money OUTPUT,
@ID varchar(50),
@qty float,
@wt float,
@wtType varchar(50)
AS
print 'Finding the price for the qty = ' + isnull(convert(varchar(50),@qty),'NULL') + 
	'and weight = ' + isnull(convert(varchar(50),@wt),'NULL') + 
	'and weight type = ' + isnull(convert(varchar(50),@wtType),'NULL')

declare @priceType varchar(50),@standardizedWt float,@perUnitOver float,@stdUnitQty float
declare @unitsToMultiply float

SELECT @priceType = PRICE_LIST_TYPE FROM A_PROD_PRICE_LIST_EXTRA_COSTS WHERE ID = @ID

SELECT @standardizedWt = (STD_UNIT_QTY * @wt) 
	FROM A_Z_UNITS_BASE_UNIT_CONVERTER 
	WHERE FROM_UNIT = @wtType


if @priceType = 'QTY_BASED'
	begin
	print 'Qty Based'
	SELECT top 1 @flatRate = FLAT_RATE, @perUNitCost = PER_UNIT_COST ,
		@perUnitOver = PER_UNIT_APPLIES_OVER
		FROM A_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE WHERE
		(
			(MIN_NUM < @qty AND MAX_NUM >= @qty) OR
			(MIN_NUM is NULL AND MAX_NUM IS NULL) OR
			(MIN_NUM < @qty AND MAX_NUM IS NULL) 
		)
		and PPLEC_ID = @ID

		if @flatRate is null and @perUnitCost is null
			SELECT TOP 1 @flatRate = FLAT_RATE, @perUNitCost = PER_UNIT_COST , @perUnitOver = PER_UNIT_APPLIES_OVER
				FROM A_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE WHERE PPLEC_ID = @ID ORDER BY MIN_NUM,MAX_NUM
		set @unitsToMultiply = @qty - isNull(@perUnitOver,0)
	end
else
	begin
	print 'Weight Based'
	SELECT top 1 @flatRate = FLAT_RATE, @perUNitCost = PER_UNIT_COST ,
		@perUnitOver = PER_UNIT_APPLIES_OVER, @stdUnitQty = STD_UNIT_QTY
		FROM A_V_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE_STANDARDIZED WHERE
		(
			(MIN_NUM < @standardizedWt AND MAX_NUM >= @standardizedWt) OR
			(MIN_NUM is NULL AND MAX_NUM IS NULL) OR
			(MIN_NUM < @standardizedWt AND MAX_NUM IS NULL) 
		)
		and PPLEC_ID = @ID


	if @flatRate is null and @perUnitCost is null
		SELECT TOP 1 @flatRate = FLAT_RATE, @perUNitCost = PER_UNIT_COST , @perUnitOver = PER_UNIT_APPLIES_OVER, @stdUnitQty = STD_UNIT_QTY
			FROM A_V_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE_STANDARDIZED WHERE PPLEC_ID = @ID ORDER BY MIN_NUM,MAX_NUM

	set @unitsToMultiply = (@standardizedWt - isNull(@perUnitOver,0))/@stdUnitQty

	print 'The data I am using to figure this is : ' +
			'Flat Rate = ' + isNull(convert(varchar(50),@flatRate),'NULL') + 
			'Per Unit Cost = ' + isNull(convert(varchar(50),@perUNitCost),'NULL') + 
			'Per Unit Over = ' + isNull(convert(varchar(50),@perUnitOver),'NULL') + 
			'Std Unit Qty = ' + isNull(convert(varchar(50),@stdUnitQty),'NULL') + 
			'Units to multiply = ' + isNull(convert(varchar(50),@unitsToMultiply),'NULL')

	end


if @unitsToMultiply > 0 
	set @totalPrice = isNull(@flatRate,0) + (isNull(@perUnitCost,0) * convert(money,@unitsToMultiply))
else
	set @totalPrice = isNull(@flatRate,0)
	





