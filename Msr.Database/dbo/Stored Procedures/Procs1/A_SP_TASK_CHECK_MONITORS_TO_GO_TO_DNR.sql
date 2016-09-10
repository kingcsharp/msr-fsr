
CREATE  PROCEDURE dbo.A_SP_TASK_CHECK_MONITORS_TO_GO_TO_DNR 
@taskID varchar(50)
AS
declare @PARENT_ID varchar(50),@pSys varchar(50)
SELECT @PARENT_ID = PARENT_ID FROM A_TASKS WHERE ID = @taskID
if exists(SELECT * FROM A_TASKS WHERE ID = @PARENT_ID AND SYSTEM_TASK = 'SYS_DNR')
	goto fin
	


if exists(SELECT * FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @taskID AND IS_PASSING = 0 AND FAIL_ACTION = 'DNR')
	begin
	print 'There is a failing monitor. We need to make a Diagnose and Repair'
	
	declare @newTaskID varchar(50),@messages varchar(2000),
		@requestorID varchar(50),@requesteeID varchar(50),@ostD datetime,
		@procID varchar(50),@stepID varchar(50)
		
	SELECT 
		@PARENT_ID = PARENT_ID,
		@requesteeID = REQUESTEE_ID,
		@ostD = getDate()
		FROM A_TASKS WHERE ID = @taskID
	


	exec A_SP_TASKS_UPDATE_TASK @newTaskID OUTPUT,@messages OUTPUT,
		null, --ID
		@taskID, --Parent,
		@requesteeID,--Requestor
		@requesteeID, --REQUESTEE_ID,
		null, --GROUP_REQUESTEE_ID
		'Diagnose and Repair Failing Monitors', --DESCRIPTION 
		'SYS_DNR', --SYSTEM_TASK,
		NULL, --COMMENT,
		'3', --Security Level
		null, --counter
		@ostD, --orig planned Start Date
		null, --orig planned stop date,
		null, --@ORIG_PLANNED_COUNTER_START numeric,
		null, --@ORIG_PLANNED_COUNTER_STOP numeric,
		@ostD, --@CUR_PLANNED_START_DATE dateTime,
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
		@requesteeID --strNTLogin
	INSERT INTO A_TASK_OBJECT_LINK (ID,TASK_ID,OBJECT_ID)
		SELECT newID(),@newTaskID,OBJECT_ID FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @taskID

	exec A_SP_TASK_SUBMIT null,null,@newTaskID,@requesteeID
	exec A_SP_TASK_ACCEPT null,null,@newTaskID,@requesteeID


	declare @curs CURSOR,@it varchar(50),@newMonitorID varchar(50),
		@desc varchar(2000),@monTaskID varchar(50)


		print 'Making a new Task with a test in it'
		SELECT 	@desc = DESCRIPTION,
				@procID = PROCEDURE_ID,
				@stepID = PROCEDURE_STEP_ID
				FROM A_TASKS WHERE ID = @taskID
-- 		exec A_SP_TASKS_UPDATE_TASK @monTaskID OUTPUT,@messages OUTPUT,
-- 			null, --ID
-- 			@newTaskID, --Parent,
-- 			@requesteeID,--Requestor
-- 			@requesteeID, --REQUESTEE_ID,
-- 			null, --GROUP_REQUESTEE_ID
-- 			@desc, --DESCRIPTION 
-- 			null, --SYSTEM_TASK,
-- 			NULL, --COMMENT,
-- 			'3', --Security Level
-- 			null, --counter
-- 			@ostD, --orig planned Start Date
-- 			null, --orig planned stop date,
-- 			null, --@ORIG_PLANNED_COUNTER_START numeric,
-- 			null, --@ORIG_PLANNED_COUNTER_STOP numeric,
-- 			@ostD, --@CUR_PLANNED_START_DATE dateTime,
-- 			null, --@CUR_PLANNED_STOP_DATE dateTime,
-- 			null, --@CUR_PLANNED_COUNTER_START numeric,
-- 			null, --@CUR_PLANNED_COUNTER_STOP numeric,
-- 			null, --@ACTUAL_START_DATE dateTime,
-- 			null, --@ACTUAL_STOP_DATE dateTime,
-- 			null, --@ACTUAL_COUNTER_START numeric,
-- 			null, --@ACTUAL_COUNTER_STOP numeric,
-- 			null, --@REF_PROC_LIST varchar(8000),
-- 			null, --@REF_FILE_LIST varchar(8000),
-- 			null, --@DISCUSSIONS varchar(8000),
-- 			null, --@SURVEYS varchar(8000),
-- 			null, --@MEETINGS varchar(8000),
-- 			null, --Object varchar(8000),
-- 			null, --@COMPANIES varchar(8000),
-- 			null, --@SYSTEM_PEOPLE_TO_EMAIL varchar(8000),
-- 			'1', --@PRIORITY smallInt,
-- 			null, --@ADDITIONAL_ASSIGNEES varchar(8000),
-- 			@requesteeID --strNTLogin
-- 		UPDATE A_TASKS SET PROCEDURE_ID = @procID, PROCEDURE_STEP_ID = @stepID
-- 			WHERE ID = @monTaskID
-- 
-- 	INSERT INTO A_TASK_OBJECT_LINK (ID,TASK_ID,OBJECT_ID)
-- 		SELECT newID(),@monTaskID,OBJECT_ID FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @taskID
-- 	
-- 		UPDATE A_MONITOR_TEMPLATES SET TASK_ID = @monTaskID,FAIL_ACTION='CONTINUE'
-- 				WHERE TASK_ID= @taskID
-- 
-- 
-- 	exec A_SP_TASK_SUBMIT null,null,@monTaskID,@requesteeID
-- 	exec A_SP_TASK_ACCEPT null,null,@monTaskID,@requesteeID
-- 	exec A_SP_TASK_FINISH @monTaskID,@requesteeID
-- 	exec A_SP_TASK_CLOSE null,null,@monTaskID,@requesteeID

	end
fin:

