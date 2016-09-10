CREATE  PROCEDURE [dbo].[A_SP_TASK_ADD_PROCEDURE_AS_CHILD_TASK]
@newObjID varchar(50) OUTPUT,
@messages varchar(500) OUTPUT,
@PARENT_TASK_ID varchar(50),
@PROC_HIST_ID nvarchar(50),
@strNTLogin varchar(50)
AS
print 'Adding a procedure as a child task to an existing task ' + @PARENT_TASK_ID

declare 
	@PROC_ID varchar(50),
	@systemID varchar(50),
	@procedureName varchar(4000),
	@taskObject varchar(50),
	@newTaskID nvarchar(50),
	@taskComment nvarchar(2000), 
	@SUBMIT_STATUS varchar(50),
	@requesteeID varchar(50),
	@stepInAP varchar(10)

SELECT	@stepInAP = STEPS_IN_AP,
	@PROC_ID = ID,
	@procedureName = NAME,
	@systemID = SYSTEM_ID,
	@taskComment = 'Child Task = ' + @PARENT_TASK_ID
FROM A_V_PROCEDURES_APPROVED_DATA
WHERE HISTORY_REF_ID = @PROC_HIST_ID

SELECT @requesteeID = REQUESTEE_ID	FROM A_TASKS WHERE ID = @PARENT_TASK_ID

SELECT @taskObject = OBJECT_ID FROM A_TASK_OBJECT_LINK	WHERE TASK_ID = @PARENT_TASK_ID

makeTasks:
print '@requesteeID = ' + isNull(@requesteeID,'NULL')

if @requesteeID is null
	begin
		set @requesteeID = @strNTLogin
	end

print 'Making a new task now'
exec A_SP_TASKS_UPDATE_TASK @newTaskID OUTPUT,@messages OUTPUT,
	null, --ID
	@PARENT_TASK_ID, --Parent,
	@strNTLogin,--Requestor
	@requesteeID, --REQUESTEE_ID,
	null, --GROUP_REQUESTEE_ID
	@procedureName, --DESCRIPTION 
	@systemID, --SYSTEM_TASK,
	@taskComment, --COMMENT,
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
	@PROC_ID, --@REF_PROC_LIST varchar(8000),
	null, --@REF_FILE_LIST varchar(8000),
	null, --@DISCUSSIONS varchar(8000),
	null, --@SURVEYS varchar(8000),
	null, --@MEETINGS varchar(8000),
	@taskObject, --Object varchar(8000),
	null, --@COMPANIES varchar(8000),
	null, --@SYSTEM_PEOPLE_TO_EMAIL varchar(8000),
	'1', --@PRIORITY smallInt,
	null, --@ADDITIONAL_ASSIGNEES varchar(8000),
	@strNTLogin --strNTLogin
	print 'Created A Task with the ID of ' + @newTaskID
	set @newObjID = @newTaskID

	UPDATE A_TASKS SET PROCEDURE_ID = @PROC_ID WHERE ID = @newTaskID
print 'Create all the monitors for the task'
	exec A_SP_TASK_CREATE_MONITORS_FROM_PROCEDURE_INFO @newTaskID,@strNTLogin


print 'Now we need to make sure all the child tasks are under way as well.'
declare @sql varchar(8000)
set @sql = 'exec A_SP_TASK_CREATE_CHILD_TASKS_FOR_PROCEDURE_STEPS ''' + @newTaskID + ''',''' + @strNTLogin + ''''
print @sql
exec (@sql)

print ' Now accept the task'
set @sql = 'exec A_SP_TASK_ACCEPT null,null,''' + @newTaskID + ''',''' + @requesteeID + ''''
print @sql
exec(@sql)
fin:






