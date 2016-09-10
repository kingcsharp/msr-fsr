



CREATE   PROCEDURE dbo.A_SP_FILL_TASK_FILL_FROM_LOCATION
@retID varchaR(50) OUTPUT,
@retMSG varchar(2000) OUTPUT,
@strLocation varchar(50),
@strTaskID varchar(50),
@strNTLogin varchar(50)
AS


declare @retStat varchar(50),@msgs varchar(50)
exec A_SP_TASK_ACCEPT @retStat OUTPUT,@msgs OUTPUT,@strTaskID,@strNTLogin
if @retStat is not null
	begin
		if not exists(SELECT * FROM A_TASKS WHERE ID = @strTaskID AND STATUS = 'ACCEPTED' AND REQUESTEE_ID = @strNTLogin)
			begin
			print 'Task returned a message'
			set @retID = 'YOU CAN NOT ACCEPT THIS TASK'
			goto fin
			end
	end

print 'Task Accepted Moving on'

print 'Searching for a fill item'
if @strLocation is null
	begin
		print 'We need a fill location'
		set @retID = 'YOU FORGOT TO SELECT A LOCATION'
		goto fin
	end

declare @fillItemID varchar(50),@appObjID varchar(50),@qty float,@fillID varchar(50)

SELECT @appObjID = OBJ_PROD_APPLIES_TO, @qty = TOT_QTY,@fillID = ID
	FROM A_V_FILLS_SEARCH 
	WHERE TASK_ID = @strTaskID

print 'The appObjID is ' + @appObjID
if @appObjID is null
	begin
		print 'Cant Find App Obj ID'
		set @retID = 'There is no Applicable Object for this Product'
		goto fin
	end

declare @actPartID varchar(50)
SELECT @actPartID = ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA 
	WHERE
	PART_ID = @appObjID AND 
		(
		LOCATION IN (SELECT CHILD_LOC FROM A_LOCATIONS_CHILD_LOOKUP WHERE LOCATION = @strLocation) 
		OR 
		LOCATION = @strLocation
		)
	AND AP_STATUS = 'ap_available'
	AND QTY > @qty

if @actPartID is null
	begin
	print 'Could not find an actual part to fill this item'
	set @retID = 'There are no Parts at this location'
	goto fin
	end
declare @msg varchar(200),@newFillID varchar(50)
	exec A_SP_FILLS_UPDATE_FILL 
	@newFillID OUTPUT,
	@msg OUTPUT,
	@fillID,
	@actPartID,
	@strNTLogin



set @retID = 'Success'

fin:



