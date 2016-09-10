






CREATE       PROCEDURE dbo.A_SP_FORECASTS_VIEW_FORECAST_ADD_ITEM_TO_FORECAST_TABLE
@ID varchar(50),
@LEV integer,
@exAll smallint,
@firstMonth dateTime,
@lastMonth dateTime,
@fType varchar(50),
@fItemNum int,
@forecastID varchar(50)
AS
print 'First Checking to see if we are in the expand All List'
If @ID in (SELECT ID FROM #tempExpandAllList)
	begin
	print 'ID = ' + @ID + 'Is in the expand all list'
	set @exAll = 1
	end
print 'Insert My ID and Level into the tree table'
declare @tester as varchar(50)
SELECT TOP 1 @tester = ID FROM A_V_FORECAST_ITEMS_WITH_ACCOUNT_PARENTS WHERE PARENT_FORECAST_ITEM = (@ID)
print 'Checking for Child using ID = ' + @ID
if @tester is not null
	begin
	set @tester = '1'
	end
else
	set @tester = '0'
print 'Inserting the item into the tree ' + @ID
INSERT INTO #tempForecast(ID,TREE_LEVEL,TREE_HAS_CHILD,EXPANDED,VIEW_ITEM,FORECAST_ID) VALUES(@ID,@LEV,convert(smallInt,@tester),0,@fItemNum,@forecastID)
print 'Now add all the monthly data'
print 'The firstMonth = ' + isNull(convert(varchar(50),@firstMonth),'NULL') + ' lastMonth = ' + isNull(convert(varchar(50),@lastMonth),'NULL')

exec A_SP_FORECASTS_VIEW_FORECASTS_ADD_MONTHLY_DATA_FOR_ITEM @ID,@firstMonth,@lastMonth
IF ((@ID in (SELECT ID FROM #tempExpandList)) or (@exAll = 1))
	begin
	UPDATE #tempForecast SET EXPANDED = 1 WHERE ID = @ID
	print 'This one needs to be expanded so make a cursor for children and do it'
	Declare @it nvarchar(50)
	Declare @curs Cursor
	set @curs = cursor for SELECT ID FROM A_V_FORECAST_ITEMS_WITH_ACCOUNT_PARENTS WHERE PARENT_FORECAST_ITEM = @ID ORDER BY ACCOUNT_NAME
	open @curs
	Fetch Next from @curs Into @it
	set @LEV = @LEV + 1
	while (@@fetch_status = 0)
		Begin
		print 'Adding a child ' + @it
		exec  A_SP_FORECASTS_VIEW_FORECAST_ADD_ITEM_TO_FORECAST_TABLE @it,@LEV,@exAll,@firstMonth,@lastMonth,@fType,2,@forecastID
		Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs
	end











