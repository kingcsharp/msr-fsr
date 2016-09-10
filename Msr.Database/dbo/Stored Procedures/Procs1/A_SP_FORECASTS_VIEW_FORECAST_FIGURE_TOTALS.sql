CREATE PROCEDURE dbo.A_SP_FORECASTS_VIEW_FORECAST_FIGURE_TOTALS 
@fHistID varchar(50),
@firstMonth datetime,
@lastMonth datetime
AS
print '$$$$$$$$$$Figuring Totals for Forecast = ' + @fHistID
declare @curs as CURSOR,@it varchar(50),@monthCounter dateTime,@sql varchar(8000),@monthField varchar(50)
set @curs = cursor for SELECT f.HISTORY_REF_ID FROM A_FORECAST_ROLL_UP r,A_FORECASTS f WHERE f.ID = r.CHILD AND r.PARENT = @fHistID
open @curs
fetch next from @curs into @it
while @@fetch_status = 0 
	begin
	exec A_SP_FORECASTS_VIEW_FORECAST_ADD_TOTAL_ROW	@fHistID,@it,@firstMonth,@lastMonth,'TSR'
	exec A_SP_FORECASTS_VIEW_FORECAST_ADD_TOTAL_ROW	@fHistID,@it,@firstMonth,@lastMonth,'TCS'
	exec A_SP_FORECASTS_VIEW_FORECAST_ADD_TOTAL_ROW	@fHistID,@it,@firstMonth,@lastMonth,'GM'
	exec A_SP_FORECASTS_VIEW_FORECAST_ADD_TOTAL_ROW	@fHistID,@it,@firstMonth,@lastMonth,'OE'
	exec A_SP_FORECASTS_VIEW_FORECAST_ADD_TOTAL_ROW	@fHistID,@it,@firstMonth,@lastMonth,'EBIT'
	exec A_SP_FORECASTS_VIEW_FORECAST_ADD_TOTAL_ROW	@fHistID,@it,@firstMonth,@lastMonth,'INT'
	exec A_SP_FORECASTS_VIEW_FORECAST_ADD_TOTAL_ROW	@fHistID,@it,@firstMonth,@lastMonth,'EBT'
	exec A_SP_FORECASTS_VIEW_FORECAST_ADD_TOTAL_ROW	@fHistID,@it,@firstMonth,@lastMonth,'TAX'
	exec A_SP_FORECASTS_VIEW_FORECAST_ADD_TOTAL_ROW	@fHistID,@it,@firstMonth,@lastMonth,'NET'
	fetch next from @curs into @it
	end





