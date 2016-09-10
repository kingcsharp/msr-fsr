
CREATE                         PROCEDURE dbo.A_SP_FILL_START_PROCEDURE_IF_POSSIBLE 
@fillID varchar(50),
@strNTLogin varchar(50)
AS

 print 'We just made a fill, so we need to see if the purchase already has been started or if 
	this is the last fille of the purchase to be filled'
declare @mySQL varchar(7500)
declare @purchItemID varchar(50), @purchHistID varchar(50)
SELECT @purchItemID = PURCH_ITEM_ID FROM A_FILLS WHERE ID = @fillID
SELECT @purchHistID = PURCHASE_HIST_ID FROM A_ORDER_ITEMS WHERE ID = @purchItemID

print 'Make sure there are no other fills that need to be done before we start this product'
if exists(SELECT * FROM A_V_FILLS_SEARCH WHERE PURCHASE_HIST_ID = @purchHistID AND FILL_OBJ_ID is null)
	begin
	print 'There are more fills to be done, so we can not start yet'
	print 'SELECT * FROM A_V_FILLS_SEARCH WHERE PURCHASE_HIST_ID = ''' + @purchHistID + ''' AND FILL_OBJ_ID is null'
	goto fin
	end

print 'This is a new one and there are no more fills that need to be filled. we are going to have to create all the tasks for the purchase'

declare @phID varchar(50),@purchaser varchar(50),@taskName nvarchar(2000),
	@newTaskID nvarchar(50),@messages nvarchar(2000)

SELECT @phID = PURCHASE_HIST_ID,
	@purchaser = PURCHASER_ID,
	@taskName = 'Manage Purchase # ' + PURCHASE_ID
	FROM A_V_FILLS_SEARCH
	WHERE ID = @fillID

if exists(SELECT * FROM A_TASK_ORDER_INFORMATION WHERE PURCHASE_HIST_ID = @phID AND FILL_ID IS NULL)
	begin
	declare @myTask varchar(50)
	SELECT @myTask = TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE PURCHASE_HIST_ID = @phID
	print 'There is already a task for this purchase it is task # ' + @myTask
	goto fin
	end	



if @purchaser is null
	begin
	print 'Purchaser is null'
	return 1
	end

print 'First create a manage purchase task'

exec A_SP_TASKS_UPDATE_TASK @newTaskID OUTPUT,@messages OUTPUT,
	null, --ID
	null, --Parent,
	@purchaser,--Requestor
	@purchaser, --REQUESTEE_ID,
	null, --GROUP_REQUESTEE_ID
	@taskName, --DESCRIPTION 
	'SYS_PURCHASE', --SYSTEM_TASK,
	NULL, --COMMENT,
	'3', --Security Level
	null, --counter
	null, --orig planned Start Date
	null, --orig planned stop date,
	null, --@ORIG_PLANNED_COUNTER_START numeric,
	null, --@ORIG_PLANNED_COUNTER_STOP numeric,
	null, --@CUR_PLANNED_START_DATE dateTime,
	null, --@CUR_PLANNED_STOP_DATE dateTime,
	null, --@CUR_PLANNED_COUNTER_START numeric,
	null, --@CUR_PLANNED_COUNTER_STOP numeric,
	null, --@ACTUAL_START_DATE dateTime,
	null, --@ACTUAL_STOP_DATE dateTime,
	null, --@ACTUAL_COUNTER_START numeric,
	null, --@ACTUAL_COUNTER_STOP numeric,
	null, --@REF_PROC_LIST varchar(8000),
	null, --@REF_FILE_LIST varchar(8000),
	null, --@DISCUSSIONS varchar(8000),
	null, --@SURVEYS varchar(8000),
	null, --@MEETINGS varchar(8000),
	null, --Object varchar(8000),
	null, --@COMPANIES varchar(8000),
	null, --@SYSTEM_PEOPLE_TO_EMAIL varchar(8000),
	'1', --@PRIORITY smallInt,
	null, --@ADDITIONAL_ASSIGNEES varchar(8000),
	@purchaser --strNTLogin
	print 'Created A Task with the ID of ' + @newTaskID
	print 'Now to add the data to the TASK_ORDER_INFORMATION table'
	if not exists(SELECT * FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @newTaskID)
		INSERT INTO A_TASK_ORDER_INFORMATION 
		(TASK_ID)
		VALUES
		(@newTaskID)

	UPDATE A_TASK_ORDER_INFORMATION 
		SET PURCHASE_HIST_ID = @phID,DRCM = getDate(),MODBY= @strNTLogin
		WHERE TASK_ID = @newTaskID

	declare @SUBMIT_STATUS varchar(50)
	print 'Submitting Task'
	exec A_SP_TASK_SUBMIT @SUBMIT_STATUS OUTPUT,@messages OUTPUT,@newTaskID,@purchaser
	print 'Done Submitting Task'

print 'Created the manage purchase task with the id of ' + @newTaskID
print 'Creating tasks for all the root purchase Items'
Declare @pItemID nvarchar(50), @curs Cursor
set @curs = Cursor For 
	SELECT DISTINCT PURCH_ITEM_ID FROM A_V_FILLS_SEARCH 
		WHERE PURCHASE_HIST_ID = @phID AND PURCHASE_ITEM_PARENT_ID is NULL

open @curs
Fetch Next from @curs Into @pItemID
while (@@fetch_status = 0)
	Begin
 	print 'Creating a task for purchase item = ' + @pItemID
	set @mySQL = 'exec A_SP_PURCHASE_ITEM_CREATE_TASK ' +
	isnull('''' + @pItemID + '''','NULL') + ',' +
	isnull('''' + @newTaskID + '''','NULL') + ',' +
	isnull('''' + @strNTLogin + '''','NULL') + ''
	exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @mySQL,@strNTLogin
 	Fetch Next from @curs Into @pItemID
	End
close @curs
Deallocate @curs
print 'Finished Creating tasks for all the root purchase Items'

	set @mySQL = 'exec A_SP_PURCHASE_UPDATE_STATUS_OF_ALL_TASKS ' +
	isnull('''' + @phID + '''','NULL') + ',' +
	isnull('''' + @strNTLogin + '''','NULL') + ''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @mySQL,@strNTLogin

fin:
print 'Exiting A_SP_FILL_START_PROCEDURE_IF_POSSIBLE'



























