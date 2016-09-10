CREATE PROCEDURE DBO.A_SP_FORECASTS_VIEW_FORECAST_ADD_TOTAL_ROW
@fHistID varchar(50),
@subForecastID varchar(50),
@firstMonth datetime,
@lastMonth datetime,
@totalKey varchar(50)
AS
declare @monthCounter as dateTime, @monthField varchar(50), @sql varchar(8000)
print 'Adding total from subforecast = ' + @subforecastID
set @monthCounter = @firstMonth
set @sql = 'UPDATE #tempForecast SET '
while @monthCounter <= @lastMonth
	begin
	set @monthField = ' MONTH_' + convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter))
	set @sql = @sql + @monthField + ' = isNull(' + @monthField + ',0) + 
	(SELECT ' + @monthField + ' FROM #tempForecast WHERE ID = ''' + @subForecastID + '_' + @totalKey + '''),'
	set @monthCounter = dateAdd(mm,1,@monthCounter)	
	end
set @sql = left(@sql,len(@sql)-1)
set @sql = @sql + ' WHERE ID = ''' + @fHistID + '_' + @totalKey + ''''
print @sql
exec(@sql)



