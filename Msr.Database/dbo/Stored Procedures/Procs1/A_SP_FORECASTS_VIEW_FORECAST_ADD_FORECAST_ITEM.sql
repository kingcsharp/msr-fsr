













CREATE                PROCEDURE dbo.A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_ITEM
@forecastID varchar(50),
@firstMonth dateTime,
@lastMonth dateTime,
@grouperName varchar(2000),
@grouperID varchar(2000),
@grouperNumber int OUTPUT,
@itemType varchar(50),
@lev int

AS
declare @subItemNumber int,@fid varchar(50),@fName varchar(2000)
SELECT @fName = NAME,@fid = ID FROM A_FORECASTS_HISTORY WHERE ID = @forecastID
set @grouperID = @fid + '_' + @grouperID
set @grouperName = @grouperName
set @subItemNumber = @grouperNumber + 1
INSERT INTO #tempForecast(USE_PUT_TEXT,VIEW_ITEM,NAME,ID,TREE_LEVEL,FORECAST_ID)
	VALUES(1,@grouperNumber,@grouperName,@grouperID,@lev,@fid)
Declare @ID nvarchar(50)
Declare @curs Cursor
if @itemType = 'COS'
	set @curs = Cursor For SELECT ID FROM A_V_FORECAST_ITEMS_WITH_ACCOUNT_PARENTS 
							WHERE PARENT_FORECAST_ITEM is NULL AND F_TYPE IN ('VAR_COS','FIX_COS') AND FORECAST_ID = @forecastID
else
	set @curs = Cursor For SELECT ID FROM A_V_FORECAST_ITEMS_WITH_ACCOUNT_PARENTS 
							WHERE PARENT_FORECAST_ITEM is NULL AND F_TYPE = @itemType AND FORECAST_ID = @forecastID

AND F_TYPE=@itemType
open @curs
Fetch Next from @curs Into @ID
set @lev = @lev + 1
while (@@fetch_status = 0)
	Begin
	print 'Getting ready to add an item'
	exec A_SP_FORECASTS_VIEW_FORECAST_ADD_ITEM_TO_FORECAST_TABLE @ID,@lev,0,@firstMonth,@lastMonth,@itemType,@subItemNumber,@fid
	print 'Added an Item'
	Fetch Next from @curs Into @ID
	End
close @curs
Deallocate @curs
declare @monthCounter dateTime,@sql varchar(8000),@insideSQL varchar(8000)
set @monthCounter = @firstMonth
print 'Creating this SQL'
set @sql = 'UPDATE #tempForecast SET '
while @monthCounter <= @lastMonth
	begin
	set @insideSQL = isNull(@insideSQL,'') + ' MONTH_' + 
		convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
		' = ' + ' isnull((SELECT SUM(MONTH_' + convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) + 
		') FROM #tempForecast WHERE PARENT_ID IS NULL AND VIEW_ITEM = ''' + convert(varchar(10),(@subItemNumber)) + '''),0), '

	set @insideSQL = isNull(@insideSQL,'') + ' ACT_MONTH_' + 
		convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
		' = ' + ' isnull((SELECT SUM(ACT_MONTH_' + convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) + 
		') FROM #tempForecast WHERE PARENT_ID IS NULL AND VIEW_ITEM = ''' + convert(varchar(10),(@subItemNumber)) + '''),0), '

	set @monthCounter = dateAdd(mm,1,@monthCounter)
	end
if @insideSQL is not null
	begin
	set @sql = @sql + left(@insideSQL,len(@insideSQL)-1)
	set @sql = @sql + ' WHERE ID = ''' + @grouperID + ''' AND FORECAST_ID = ''' + @fid + ''' '
	print @sql
	exec(@sql)
	end
else
	print 'There was no inside sql start firstMonth = ' + convert(varchar(50),@firstMonth) + ' lastMonth = ' + convert(varchar(50),@lastMonth)

set @grouperNumber = @grouperNumber + 2












