








CREATE           PROCEDURE dbo.A_SP_FORECASTS_VIEW_FORECAST_ADD_CALCULATED_COLUMN
@forecastID varchar(50),
@firstMonth dateTime,
@lastMonth dateTime,
@grouperName varchar(2000),
@grouperID varchar(2000),
@grouperNumber int OUTPUT,
@operator varchar(10),
@row1 varchar(10),
@row2 varchar(10),
@lev int
AS
declare @fid varchar(50)
SELECT @fid = @forecastID
set @grouperID = @fid + '_' + @grouperID
INSERT INTO #tempForecast(USE_PUT_TEXT,VIEW_ITEM,NAME,ID,TREE_LEVEL,FORECAST_ID)
	VALUES(1,@grouperNumber,@grouperName,@grouperID,@lev,@fid)

declare @monthCounter dateTime,@sql varchar(8000)
set @monthCounter = @firstMonth
set @sql = 'UPDATE #tempForecast SET '
while @monthCounter <= @lastMonth
	begin
	set @sql = @sql + ' MONTH_' + 
		convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
		' = ' + ' (SELECT (MONTH_' + convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) + 
		') FROM #tempForecast WHERE ID = ''' + @fid + '_' + @row1 + ''') ' + 
		@operator +
		' (SELECT (MONTH_' + convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) + 
		') FROM #tempForecast WHERE ID = ''' + @fid + '_' + @row2 + '''),'

	set @sql = @sql + ' ACT_MONTH_' + 
		convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
		' = ' + ' (SELECT (ACT_MONTH_' + convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) + 
		') FROM #tempForecast WHERE ID = ''' + @fid + '_' + @row1 + ''') ' + 
		@operator +
		' (SELECT (ACT_MONTH_' + convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) + 
		') FROM #tempForecast WHERE ID = ''' + @fid + '_' + @row2 + '''),'

	set @monthCounter = dateAdd(mm,1,@monthCounter)	

	end
set @sql = left(@sql,len(@sql)-1)
set @sql = @sql + ' WHERE ID = ''' + @grouperID + ''' AND FORECAST_ID = ''' + @fid + ''''
print @sql
exec(@sql)

set @grouperNumber = @grouperNumber + 1







