CREATE PROCEDURE dbo.A_SP_QUOTE_ITEM_GET_INITIAL_DATA_FROM_PROD_PRICE_LIST
@qItemID varchar(50),
@strNTLogin varchar(50)
AS
declare @leadTime float,@capacity float,@minQty float,@pplID varchar(50),
	@leadTimeUnit varchar,@capacityUnit varchar
SELECT @pplID = PROD_PRICE_LIST FROM A_QUOTE_ITEMS WHERE ID = @qItemID
if @pplID is not null
	begin
	SELECT @leadTime = PRODUCTION_TIME,
		@leadTimeUnit = PRODUCTION_TIME_UNIT,
		@capacity = CAPACITY,
		@capacityUnit = CAPACITY_UNIT,
		@minQty = MIN_QUANTITY
	FROM A_V_PROD_PRICE_LIST_BY_APPROVED_ID WHERE ID = @pplID
	UPDATE A_QUOTE_ITEMS SET TYP_PROD_TIME = @leadTime,
		TYP_PROD_TIME_UNIT = @leadTimeUnit,
		TYP_PROD_CAPACITY = @capacity,
		TYP_PROD_CAPACITY_UNIT = @capacityUnit,
		MIN_QTY = @minQty WHERE ID = @qItemID
	end

