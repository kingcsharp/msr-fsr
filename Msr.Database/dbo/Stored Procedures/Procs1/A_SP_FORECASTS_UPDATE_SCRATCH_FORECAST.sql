





CREATE       PROCEDURE A_SP_FORECASTS_UPDATE_SCRATCH_FORECAST
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@objID nvarchar(50),
@ID nvarchar(50),
@NAME nvarchar(2000),
@START_MONTH nvarchar(50),
@START_YEAR nvarchar(50),
@STOP_MONTH nvarchar(50),
@STOP_YEAR nvarchar(50),
@CO nvarchar(50),
@FTYPE varchar(50),
@SUBFORECASTS varchar(50),
@SALES_OFFSET nvarchar(50),
@PURCHASE_OFFSET nvarchar(50),
@SUPPLIERS varchar(8000),
@CUSTOMERS varchar(8000),
@FORECAST_TYPES varchar(8000),
@strNTLogin varchar(50)
AS
print 'Updating a forecast'
if @objID is null
	begin
		print 'ID is Null we need to create this Forecast'
		exec sp_GetUniqueID3 @newID OUTPUT
		set @ID = @newID
		INSERT INTO A_FORECASTS_HISTORY(ID,NAME,F_TYPE,MODBY,DRCM) VALUES(@newID,@NAME,@FTYPE,@strNTLogin,getDATE())
		SELECT @newID = OBJECT_ID FROM A_FORECASTS_HISTORY WHERE ID = @newID
	end
else
	begin
		print 'The ID is not null so we are just updating Forecast objectID='+@objID
		set @newID = @objID
	end

print'Updating the Forecast history Data'
UPDATE A_FORECASTS_HISTORY SET
NAME = @NAME,
START_MONTH = @START_MONTH,
START_YEAR = @START_YEAR,
STOP_MONTH = @STOP_MONTH,
STOP_YEAR = @STOP_YEAR,
SALES_OFFSET = @SALES_OFFSET,
PURCHASE_OFFSET = @PURCHASE_OFFSET,
CO = @CO,
DRCM = getDate(),
MODBY = @strNTLogin
WHERE OBJECT_ID = @newID


print 'Now that the main Forecast Data is updated we need to update the subforecast filter stuff'
print 'First delete all the old Forecasts we used to link to'
DELETE FROM A_FORECAST_ROLL_UP WHERE PARENT = @ID
print 'making a cursor to go through the suppliers string'
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @SUBFORECASTS,', '
INSERT INTO A_FORECAST_ROLL_UP (ID,PARENT,CHILD,DRCM,MODBY) 
	SELECT newID(),@ID,IT,getDate(),@strNTLogin FROM #TempItems

print 'Delete all the old Customers we related to'
DELETE FROM A_FORECAST_CUSTOMER_LIST WHERE FORECAST_ID = @ID
print 'making a cursor to go through the customers string'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @CUSTOMERS,', '
INSERT INTO A_FORECAST_CUSTOMER_LIST (ID,FORECAST_ID,CO,DRCM,MODBY) 
	SELECT newID(),@ID,IT,getDate(),@strNTLogin FROM #TempItems

print 'Delete all the old @SUPPLIERS we related to'
DELETE FROM A_FORECAST_SUPPLIER_LIST WHERE FORECAST_ID = @ID
print 'making a cursor to go through the @SUPPLIERS string'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @SUPPLIERS,', '
INSERT INTO A_FORECAST_SUPPLIER_LIST (ID,FORECAST_ID,CO,DRCM,MODBY) 
	SELECT newID(),@ID,IT,getDate(),@strNTLogin FROM #TempItems

print 'Delete all the old @FORECAST_TYPES we related to'
DELETE FROM A_FORECAST_ROLLUP_TYPES WHERE FORECAST_ID = @ID
print 'making a cursor to go through the @FORECAST_TYPES string'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @FORECAST_TYPES,', '
INSERT INTO A_FORECAST_ROLLUP_TYPES (ID,FORECAST_ID,F_TYPE_ID,DRCM,MODBY) 
	SELECT newID(),@ID,IT,getDate(),@strNTLogin FROM #TempITems


 

