
CREATE  PROCEDURE DBO.A_SP_FILL_FIGURE_OUT_START_AND_STOP_DATES
@startDate datetime OUTPUT,
@stopDate datetime OUTPUT,
@purchItemID varchar(50)
AS
declare @leadTime varchar(50),@pplID varchar(50),@leadTimeUnits varchar(50),@quoteID varchar(50)
--SELECT * FROM A_V_ORDER_ITEMS_ALL_DATA WHERE ID = @purchItemID
SELECT @pplID = PROD_PRICE_LIST,@quoteID = QUOTE_ID FROM A_V_ORDER_ITEMS_ALL_DATA WHERE ID = @purchItemID
--SELECT * FROM A_V_PROD_PRICE_LIST_APPROVED_DATA WHERE ID = @pplID
--SELECT * FROM A_V_QUOTES_APPROVED_DATA WHERE HISTORY_REF_ID = @quoteID
SELECT @leadTime = PRODUCTION_TIME,@leadTimeUnits = PRODUCTION_TIME_UNIT 
	FROM A_V_PROD_PRICE_LIST_APPROVED_DATA WHERE ID = @pplID
set @startDate = getDate()


print 'Unit = '
print @leadtimeUnits
SELECT @stopDate = dbo.A_FN_DATE_TIME_ADD_USING_UNITS (@leadTimeUnits,getDate(),@leadTime)


