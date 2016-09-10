

CREATE   PROCEDURE DBO.ZZ_OBSOLETE_A_SP_TASK_PROCEDURE_CREATE_CHILDREN_TASKS_STARTING_WITH_CERTAIN_STEP
@startStepID varchar(50),
@recurNumber int,
@PARENT_ID  varchar(50)
AS
print 'The parent Task is ' + @PARENT_ID
declare @procID varchar(50),@requestor varchar(50),@requesteeRole varchar(50),
	@stepName nvarchar(2000),@systemID varchar(50),@newTaskID nvarchar(50),
	@messages nvarchar(2000),@taskComment nvarchar(2000),
	@qItemID varchar(50),@SUBMIT_STATUS varchar(50),@newID varchar(50), @phID varchar(50),
	@supplierID varchar(50),@showStepsInAP smallInt,@requesteeID varchar(50)
SELECT @procID = PROCEDURE_ID,@requestor = ORIG_REQUESTOR_ID FROM A_TASKS WHERE ID = @PARENT_ID
SELECT @phID = HISTORY_REF_ID FROM A_PROCEDURES WHERE ID = @procID
SELECT @showStepsInAP = STEPS_IN_AP FROM A_PROCEDURES_HISTORY WHERE ID = @phID
if @showStepsInAP is NULL or @showStepsInAP <> 1 
	begin 
	print 'We are not supposed to do the steps of this procedure'
	goto fin
	end
declare @SPECIFIC_LOCATION varchar(50)
Declare @it nvarchar(50)
Declare @curs Cursor,@myPrintOrder int
SELECT @myPrintOrder = PRINT_ORDER FROM A_PROCEDURE_STEPS WHERE ID = @startStepID
set @curs = Cursor For SELECT ID FROM A_PROCEDURE_STEPS WHERE PROCEDURE_ID = @phID AND PRINT_ORDER >= @myPrintOrder
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	if exists(SELECT * FROM A_TASKS 
		WHERE PARENT_ID = @PARENT_ID AND PROCEDURE_ID = @procID AND 
				PROCEDURE_STEP_ID = @it and RECURSION_NUMBER = @recurNumber)
		begin
		print 'Already have this onso done here'
		goto DONE_WITH_STEP
		end
	print 'We need to get all the data for this procedure step ' + @it
	exec sp_GetUniqueID3 @newID OUTPUT
	select @requesteeRole = OWNER_ROLE_ID,@stepName = STEP_TEXT,@systemID = SYSTEM_TASK
		FROM A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR WHERE ID = @it
	select @supplierID = HISTORY_REF_ID FROM A_COMPANIES WHERE ID = dbo.FN_ROLE_GET_COMPANY(@requesteeRole)
	
	print 'requesteeRole ' + isnull(@requesteeRole,'NULL')
	if @requesteeRole is null
		begin
		select @requesteeID = REQUESTEE_ID FROM A_TASKS WHERE ID = @PARENT_ID
		print 'Requestee ID = ' + isNull(@requesteeID,'NULL')
		end
	print 'Step Name ' + @stepName
	print 'systemID ' + isNull(@systemID,'NULL')
	print 'Inserting this task'
	SELECT @SPECIFIC_LOCATION = SPECIFIC_LOCATION FROM A_PROCEDURE_STEPS WHERE ID = @it
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
			PROCEDURE_STEP_ID = @it,
			STATUS = 'PENDING_PARENT_ACCEPTANCE'
			WHERE ID = @newTaskID

		if @recurNumber > 1
			begin
			UPDATE A_TASKS SET 
			RECURSION_NUMBER = @recurNumber,
			STATUS = 'Qued'
			WHERE ID = @newTaskID 			exec A_SP_TASK_FIGURE_OUT_PROCEDURE_START_DATE_TIME @newTaskID
			declare @myQdate datetime,@sql varchar(50)
			select @myQDate = ORIG_PLANNED_START_DATE FROM A_TASKS WHERE ID = @newTaskID
			if @startStepID = @it
				begin
				set @sql = 'exec A_SP_TASK_SUBMIT null,null,''' + @newTaskID + ''',''' + @requestor + ''''
				if @myQDate > getDate()
					exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP_WITH_DATE @sql,@myQDate,@requestor
				else
					exec(@sql)
				end

			end
		print 'Task Updated'

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
		DONE_WITH_STEP:
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs


fin:

print 'Finished with A_SP_TASK_PROCEDURE_CREATE_CHILDREN_TASKS_STARTING_WITH_CERTAIN_STEP'











