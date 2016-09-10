






CREATE             PROCEDURE DBO.A_SP_FORECASTS_VIEW_FORECAST
@forecastID varchar(50),
@strListToexpand varchar(8000),
@strExpandAllList varchar(8000),
@strNTLogin nvarchar(50)
AS
CREATE TABLE #tempExpandList(ID varchar(50))
INSERT INTO #tempExpandList Exec A_SP_Z_SPLIT @strListToexpand,','
CREATE TABLE #tempExpandAllList(ID varchar(50))
INSERT INTO #tempExpandAllList Exec A_SP_Z_SPLIT @strExpandAllList,','
print 'viewing a forecast'
CREATE TABLE #tempForecast
( 
IDENT int IDENTITY PRIMARY KEY,
USE_PUT_TEXT tinyint,
VIEW_ITEM smallInt,
ID varchar(50),
PARENT_ID varchar(50),
NAME varchar(1000),
TREE_LEVEL int,TREE_HAS_CHILD smallInt,EXPANDED smallInt,
CONFIDENCE float,
PROGRESS varchar(50)
)
print 'The forecast ID is ' + @forecastID

declare @firstMonth as dateTime
declare @lastMonth as dateTime
SELECT 
@firstMonth = convert(dateTime,convert(varchar(50),START_MONTH) + '/1/' + convert(varchar(50),START_YEAR)),
@lastMonth = convert(dateTime,convert(varchar(50),STOP_MONTH) + '/1/' + convert(varchar(50),STOP_YEAR))
FROM A_FORECASTS_HISTORY WHERE OBJECT_ID = @forecastID
print 'First Month = ' + convert(varchar(50),@firstMonth)
print 'Last Month = ' + convert(varchar(50),@lastMonth)
declare @monthCounter dateTime,@sql varchar(4000)
set @monthCounter = @firstMonth
while @monthCounter <= @lastMonth
	begin
	print 'Adding column for month = ' + convert(varchar(50),month(@monthCounter)) + '/' + convert(varchar(50),year(@monthCounter))
	set @sql = 'ALTER TABLE #tempForecast 
		ADD MONTH_' + 
		convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
		' money NULL, 
		ACT_MONTH_' + 
		convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
		' money NULL 

		'


	print @sql
	exec(@sql)
	set @monthCounter = dateAdd(mm,1,@monthCounter)	
	end
ALTER TABLE #tempForecast ADD TOTAL money null,MO_AVG money null,ACTUAL_TOTAL money,ACT_MO_AVG money
exec A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_ITEM @forecastID,@firstMonth,@lastMonth,'Total Sales Revenue','TSR',1,'REVENUE'
exec A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_ITEM @forecastID,@firstMonth,@lastMonth,'Total Cost of Sales','TCS',3,'COS'
exec A_SP_FORECASTS_VIEW_FORECAST_ADD_CALCULATED_COLUMN @forecastID,@firstMonth,@lastMonth,'Gross Margin','GM',5,'-','1','3'
exec A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_ITEM @forecastID,@firstMonth,@lastMonth,'Operating Expenses','OE',6,'OP_EXP'
exec A_SP_FORECASTS_VIEW_FORECAST_ADD_CALCULATED_COLUMN @forecastID,@firstMonth,@lastMonth,'EBIT','EBIT',8,'-','5','6'
exec A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_ITEM @forecastID,@firstMonth,@lastMonth,'Total Interest','INT',9,'INTEREST_F'
exec A_SP_FORECASTS_VIEW_FORECAST_ADD_CALCULATED_COLUMN @forecastID,@firstMonth,@lastMonth,'EBT','EBT',11,'-','8','9'
exec A_SP_FORECASTS_VIEW_FORECAST_ADD_FORECAST_ITEM @forecastID,@firstMonth,@lastMonth,'Total Taxes','TAX',12,'TAX_F'
exec A_SP_FORECASTS_VIEW_FORECAST_ADD_CALCULATED_COLUMN @forecastID,@firstMonth,@lastMonth,'NET','NET',14,'-','11','12'

declare @cnt int
set @cnt = 0
set @sql = 'UPDATE #tempForecast SET TOTAL = '
set @monthCounter = @firstMonth
while @monthCounter <= @lastMonth
	begin
	set @cnt = @cnt + 1
	print 'Adding column for month = ' + convert(varchar(50),month(@monthCounter)) + '/' + convert(varchar(50),year(@monthCounter))
	set @sql = @sql + ' MONTH_' + 
		convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
		' +'
	set @monthCounter = dateAdd(mm,1,@monthCounter)	
	end
set @sql = left(@sql,len(@sql)-1)
print @sql
exec(@sql)
set @sql = 'UPDATE #tempForecast set MO_AVG = TOTAL / ' + convert(varchar(50),@cnt)
exec(@sql)

set @cnt = 0
set @sql = 'UPDATE #tempForecast SET ACTUAL_TOTAL = '
set @monthCounter = @firstMonth
while @monthCounter <= @lastMonth
	begin
	set @cnt = @cnt + 1
	print 'Adding column for month = ' + convert(varchar(50),month(@monthCounter)) + '/' + convert(varchar(50),year(@monthCounter))
	set @sql = @sql + ' ACT_MONTH_' + 
		convert(varchar(50),month(@monthCounter)) + '_' + convert(varchar(50),year(@monthCounter)) +
		' +'
	set @monthCounter = dateAdd(mm,1,@monthCounter)	
	end
set @sql = left(@sql,len(@sql)-1)
print @sql
exec(@sql)
set @sql = 'UPDATE #tempForecast set ACTUAL_MO_AVG = ACTUAL_TOTAL / ' + convert(varchar(50),@cnt)
exec(@sql)






SELECT * FROM #tempForecast







