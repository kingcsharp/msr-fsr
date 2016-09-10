











CREATE             PROCEDURE DBO.A_SP_FORECASTS_VIEW_ROLLUP
@forecastID varchar(50),
@forecastHistID varchar(50),
@forecastObjID varchar(50),
@strListToexpand varchar(8000),
@strExpandAllList varchar(8000),
@strNTLogin nvarchar(50)
AS
if @forecastID is not null and @forecastHistID is null
	SELECT @forecastHistID = HISTORY_REF_ID FROM A_FORECASTS WHERE ID = @forecastID
if @forecastObjID is not null and @forecastHistID is null
	SELECT @forecastHistID = ID FROM A_FORECASTS_HISTORY WHERE OBJECT_ID = @forecastObjID

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
PROGRESS varchar(50),
FORECAST_ID varchar(50),
FORECAST_NAME varchar(1000),
hidden tinyint,
SHORT_COMMENT varchar(10)
)
print 'The forecast ID is ' + @forecastID
print 'The forecast History ID is ' + @forecastHistID

declare @firstMonth as dateTime
declare @lastMonth as dateTime,@ID varchar(50)
SELECT 
@firstMonth = convert(dateTime,convert(varchar(50),START_MONTH) + '/1/' + convert(varchar(50),START_YEAR)),
@lastMonth = convert(dateTime,convert(varchar(50),STOP_MONTH) + '/1/' + convert(varchar(50),STOP_YEAR))
FROM A_FORECASTS_HISTORY WHERE ID = @forecastHistID
print 'First Month = ' + convert(varchar(50),@firstMonth)
print 'Last Month = ' + convert(varchar(50),@lastMonth)
declare @monthCounter dateTime,@sql varchar(4000)
set @monthCounter = @firstMonth
while @monthCounter <= @lastMonth
	begin
	print 'Adding column for month = ' + convert(varchar(50),month(@monthCounter)) + '/' + convert(varchar(50),year(@monthCounter))
	set @sql = 'ALTER TABLE #tempForecast ADD MONTH_' + 
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
ALTER TABLE #tempForecast ADD TOTAL money null,MO_AVG money null,ACTUAL_TOTAL money,ACTUAL_MO_AVG money

exec A_SP_FORECASTS_VIEW_ADD_ONE_FORECAST_TO_ROLL_UP @forecastHistID,1,@firstMonth,@lastMonth,0

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
declare @monthCount int
if (getDate() > @lastMonth)
	set @monthCount = dateDiff(mm,@firstMonth,@lastMonth) + 1
else
	set @monthCount = dateDiff(mm,@firstMonth,getDate()) + 1


set @sql = 'UPDATE #tempForecast set ACTUAL_MO_AVG = ACTUAL_TOTAL / ' + convert(varchar(50),@monthCount)
exec(@sql)





SELECT * FROM #tempForecast WHERE hidden is NULL or hidden <> 1










