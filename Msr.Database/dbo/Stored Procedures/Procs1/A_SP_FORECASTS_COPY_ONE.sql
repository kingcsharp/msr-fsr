





CREATE      procedure A_SP_FORECASTS_COPY_ONE
	@newObjID nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@strNTLogin nvarchar(50),
	@copyPrefix nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one

INSERT INTO A_FORECASTS_HISTORY 
(
[ID], [NAME], [START_MONTH], [START_YEAR], [DRCM], 
[MODBY], [CO], [STOP_MONTH], [STOP_YEAR], [START_DATE], [STOP_DATE],F_TYPE)
SELECT 
@newID, [NAME], [START_MONTH], [START_YEAR], getDate(), 
@strNTLogin, [CO], [STOP_MONTH], [STOP_YEAR], [START_DATE], [STOP_DATE],F_TYPE
FROM A_FORECASTS_HISTORY WHERE ID = @strID

--find out what object id the new one got
SELECT @newObjID = OBJECT_ID FROM A_FORECASTS_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID

print 'Copy all the ForeCast Items as well'
Declare @oneStep nvarchar(50)
Declare @stepCursor Cursor
set @stepCursor = Cursor
For SELECT ID FROM A_FORECAST_ITEMS WHERE FORECAST_ID = @strID
open @stepCursor
Fetch Next from @stepCursor
Into @oneStep
while (@@fetch_status = 0)
	Begin
	print @oneStep
	exec A_SP_FORECAST_ITEM_COPY @newID,@oneStep,@strNTLogin
	Fetch Next from @stepCursor
	Into @oneStep
	End
close @stepCursor
Deallocate @stepCursor				

print 'Copy all the subForecasts'
INSERT INTO A_FORECAST_ROLL_UP (ID,PARENT,CHILD,DRCM,MODBY)
	SELECT newID(),@newID,CHILD,getDate(),@strNTLogin
		FROM A_FORECAST_ROLL_UP WHERE PARENT = @strID



