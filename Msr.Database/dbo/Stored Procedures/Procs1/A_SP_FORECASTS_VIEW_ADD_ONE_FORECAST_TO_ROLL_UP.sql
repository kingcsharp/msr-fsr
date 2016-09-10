








CREATE          PROCEDURE DBO.A_SP_FORECASTS_VIEW_ADD_ONE_FORECAST_TO_ROLL_UP
@fHistID varchar(50),
@curItem int output,
@firstMonth dateTime,
@lastMonth dateTime,
@lev int
AS
	declare @fType varchar(50)
	declare @item1 varchar(50),@item2 varchar(50)
	SELECT @fType = F_TYPE FROM A_FORECASTS_HISTORY WHERE ID = @fHistID
	if @fType = 'START_FROM_SCRATCH'
		begin
		exec A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_NAME @fHistID,@curItem OUTPUT,'NAME',@lev
		set @lev = @lev + 1
		exec A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_ITEM @fHistID,@firstMonth,@lastMonth,'Sales Revenue','TSR',@curItem OUTPUT,'REVENUE',@lev
		exec A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_ITEM @fHistID,@firstMonth,@lastMonth,'Cost of Sales','TCS',@curItem OUTPUT,'COS',@lev
		exec A_SP_FORECASTS_VIEW_FORECAST_ADD_CALCULATED_COLUMN @fHistID,@firstMonth,@lastMonth,'Gross Margin','GM',@curItem OUTPUT,'-','TSR','TCS',@lev
		exec A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_ITEM @fHistID,@firstMonth,@lastMonth,'Operating Expenses','OE',@curItem OUTPUT,'OP_EXP',@lev
		exec A_SP_FORECASTS_VIEW_FORECAST_ADD_CALCULATED_COLUMN @fHistID,@firstMonth,@lastMonth,'EBIT','EBIT',@curItem OUTPUT,'-','GM','OE',@lev
		exec A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_ITEM @fHistID,@firstMonth,@lastMonth,'Interest','INT',@curItem OUTPUT,'INTEREST_F',@lev
		exec A_SP_FORECASTS_VIEW_FORECAST_ADD_CALCULATED_COLUMN @fHistID,@firstMonth,@lastMonth,'EBT','EBT',@curItem OUTPUT,'-','EBIT','INT',@lev
		exec A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_ITEM @fHistID,@firstMonth,@lastMonth,'Taxes','TAX',@curItem OUTPUT,'TAX_F',@lev
		exec A_SP_FORECASTS_VIEW_FORECAST_ADD_CALCULATED_COLUMN @fHistID,@firstMonth,@lastMonth,'Net Profit','NET',@curItem OUTPUT,'-','EBT','TAX',@lev
		end
	else
		begin
		exec A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_NAME @fHistID,@curItem OUTPUT,'NAME',@lev
		set @lev = @lev + 1
		exec dbo.A_SP_FORECASTS_VIEW_FORECAST_ADD_TOTAL_ROWS_PLACE_HOLDER @fHistID,@firstMonth,@lastMonth,@curItem OUTPUT,@lev
		set @lev = @lev + 1

		Declare @it nvarchar(50)
		Declare @curs Cursor
		set @curs = Cursor For SELECT CHILD FROM A_FORECAST_ROLL_UP WHERE PARENT = @fHistID
		open @curs
		Fetch Next from @curs Into @it
		declare @lbl varchar(50)
		declare @subID varchar(50)
		while (@@fetch_status = 0)
		Begin
			SELECT @subID = HISTORY_REF_ID FROM A_FORECASTS WHERE ID = @it
			exec A_SP_FORECASTS_VIEW_ADD_ONE_FORECAST_TO_ROLL_UP @subID,@curItem OUTPUT,@firstMonth,@lastMonth,@lev
			Fetch Next from @curs Into @it
		End
		close @curs
		Deallocate @curs
		exec dbo.A_SP_FORECASTS_VIEW_FORECAST_FIGURE_TOTALS @fHistID,@firstMonth,@lastMonth
		end









