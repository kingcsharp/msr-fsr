
CREATE       PROCEDURE DBO.A_SP_TASK_PROCEDURE_CREATE_CHILDREN_TASKS_STARTING_WITH_CERTAIN_STEP
@startStepID varchar(50),
@recurNumber int,
@PARENT_ID  varchar(50),
@repeatFromStep varchar(50)
AS
print 'The parent Task is ' + @PARENT_ID
print 'recur step is ' + @repeatFRomStep
print 'RecurNumber = '
print @recurNumber

declare @procID varchar(50),@requestor varchar(50),@requesteeRole varchar(50),
	@stepName nvarchar(2000),@systemID varchar(50),@newTaskID nvarchar(50),
	@messages nvarchar(2000),@taskComment nvarchar(2000),
	@SUBMIT_STATUS varchar(50),@newID varchar(50), @phID varchar(50),
	@supplierID varchar(50),@showStepsInAP smallInt,@requesteeID varchar(50),
	@SPECIFIC_LOCATION varchar(50)

SELECT @procID = PROCEDURE_ID,@requestor = ORIG_REQUESTOR_ID FROM A_TASKS WHERE ID = @PARENT_ID
SELECT @phID = HISTORY_REF_ID FROM A_PROCEDURES WHERE ID = @procID
if exists(SELECT * FROM A_TASKS 
		WHERE PARENT_ID = @PARENT_ID AND PROCEDURE_ID = @procID AND 
		PROCEDURE_STEP_ID = @startStepID and RECURSION_NUMBER = @recurNumber 
		and REPEAT_FROM_STEP = @repeatFromStep)
	begin
	print 'Already have this one so done here'
	goto DONE_WITH_STEP
	end

if exists(SELECT * FROM A_TASKS 
		WHERE PARENT_ID = @PARENT_ID AND PROCEDURE_ID = @procID AND 
		PROCEDURE_STEP_ID = @repeatFromStep and RECURSION_NUMBER = @recurNumber 
		and REPEAT_FROM_STEP = @repeatFromStep)
	begin
	print 'We just finished making this thing to the point where we started from'
	goto DONE_WITH_STEP
	end


		
print 'We need to get all the data to make a task out of this step ' + @startStepID
exec sp_GetUniqueID3 @newID OUTPUT
select @requesteeRole = OWNER_ROLE_ID,@stepName = STEP_TEXT,@systemID = SYSTEM_TASK
	FROM A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR WHERE ID = @startStepID
select @supplierID = HISTORY_REF_ID FROM A_COMPANIES WHERE ID = dbo.FN_ROLE_GET_COMPANY(@requesteeRole)
print 'requesteeRole ' + isnull(@requesteeRole,'NULL')
if @requesteeRole is null
	begin
	select @requesteeID = REQUESTEE_ID FROM A_TASKS WHERE ID = @PARENT_ID
	if @requesteeID is null
		begin
		select @requesteeRole = GROUP_REQUESTEE_ID FROM A_TASKS WHERE ID = @PARENT_ID
		if @requesteeRole is null
			set @requesteeID = null
		end
	end
print 'Step Name ' + @stepName
print 'systemID ' + isNull(@systemID,'NULL')
print 'Inserting this task'
SELECT @SPECIFIC_LOCATION = SPECIFIC_LOCATION FROM A_PROCEDURE_STEPS WHERE ID = @startStepID
exec A_SP_TASKS_UPDATE_TASK @newTaskID OUTPUT,@messages OUTPUT,
	null, --ID
	@PARENT_ID, --Parent,
	@requestor,--Requestor
	@requesteeID, --REQUESTEE_ID,
	@requesteeRole, --GROUP_REQUESTEE_ID
	@stepName, --DESCRIPTION 
	@systemID, --SYSTEM_TASK,
	null, --COMMENT,
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
	@procID, --@REF_PROC_LIST varchar(8000),
	null, --@REF_FILE_LIST varchar(8000),
	null, --@DISCUSSIONS varchar(8000),
	null, --@SURVEYS varchar(8000),
	null, --@MEETINGS varchar(8000),
	null, --Object varchar(8000),
	null, --@COMPANIES varchar(8000),
	null, --@SYSTEM_PEOPLE_TO_EMAIL varchar(8000),
	'1', --@PRIORITY smallInt,
	null, --@ADDITIONAL_ASSIGNEES varchar(8000),
	@requesteeID --strNTLogin
	print 'Created A Task with the ID of ' + @newTaskID
	
	
UPDATE A_TASKS SET 
	[PROCEDURE_ID] = @procID, 
	PROCEDURE_STEP_ID = @startStepID,
	STATUS = 'PENDING_PARENT_ACCEPTANCE',
	REPEAT_FROM_STEP = @repeatFromStep	
	WHERE ID = @newTaskID

if @recurNumber > 1
	begin
	UPDATE A_TASKS SET 
	RECURSION_NUMBER = @recurNumber,
	STATUS = 'Qued'
	WHERE ID = @newTaskID
	exec A_SP_TASK_FIGURE_OUT_PROCEDURE_START_DATE_TIME @newTaskID
	-- If this is the first task at this recursion number then we need to go ahead 
	-- and submit it.
	declare @prevTask varchar(50)
	SELECT @prevTask = PREV_TASK_ID FROM A_V_TASKS_WITH_PROCEDURE_STEP_DATA WHERE STEP_ID = @newTaskID
	if @prevTask is null
		begin
		declare @myQdate datetime,@sql varchar(50)
		select @myQDate = dateAdd(dd,-7,ORIG_PLANNED_START_DATE) FROM A_TASKS WHERE ID = @newTaskID
		set @sql = 'exec A_SP_TASK_SUBMIT null,null,''' + @newTaskID + ''',''' + @requestor + ''''
		if @myQDate > getDate()
			exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP_WITH_DATE @sql,@myQDate,@requestor
		else
			exec(@sql)
		end

	if not exists(SELECT * FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @newTaskID)
		INSERT INTO A_TASK_ORDER_INFORMATION (TASK_ID,ACTUAL_TO_LOC,MODBY,DRCM)
			VALUES (@newTaskID,@SPECIFIC_LOCATION,@requesteeID,getDate())
	else
		UPDATE A_TASK_ORDER_INFORMATION
			SET ACTUAL_TO_LOC = @SPECIFIC_LOCATION
			WHERE TASK_ID = @newTaskID

	INSERT INTO A_TASK_OBJECT_LINK 
		(ID,TASK_ID,OBJECT_ID,DRCM,MODBY)
	SELECT newID(),@newTaskID,OBJECT_ID,getDate(),@requesteeID
		FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @PARENT_ID
	exec A_SP_TASK_CREATE_MONITORS_FROM_PROCEDURE_INFO @newTaskID,@requesteeID
	exec A_SP_TASK_AUTOMATIC_ORDER_UPDATER @newTaskID,@requesteeID
	exec A_SP_TASK_FIGURE_OUT_PROCEDURE_START_DATE_TIME @newTaskID

	end

print 'Now we need to make all the steps that were waiting on this step to complete'
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT ID FROM A_PROCEDURE_STEPS WHERE 
							ID IN (SELECT MY_STEP FROM A_PROCEDURE_STEP_PRECEDING_STEPS WHERE
							PREV_STEP = @startStepID)
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
exec A_SP_TASK_PROCEDURE_CREATE_CHILDREN_TASKS_STARTING_WITH_CERTAIN_STEP
	@it,@recurNumber,@PARENT_ID,@repeatFromStep
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs




DONE_WITH_STEP:


fin:

print 'Finished with A_SP_TASK_PROCEDURE_CREATE_CHILDREN_TASKS_STARTING_WITH_CERTAIN_STEP'















