









CREATE         PROCEDURE [dbo].[A_SP_PROCEDURE_ASSIGN_TO_PEOPLE]
@procID varchar(50),
@personID varchar(50),
@requesteeRole varchar(50),
@origSTDate varchar(50),
@parentTaskID varchar(50),
@strNTLogin varchar(50)
AS
begin Transaction

print 'Assigning a procedure'
declare @newTaskID varchar(50),@messages varchar(2000),@taskName varchar(1000),
	@systemID varchar(50),@origStartDate dateTime
print 'here'
declare @assName varchar(1000)

print @origSTDate
set @origStartDate = convert(dateTime,@origSTDate)
SELECT @taskName = NAME,@systemID = SYSTEM_ID
	FROM A_V_PROCEDURES_DATA_QUICK
	WHERE ID = @procID
	
exec A_SP_TASKS_UPDATE_TASK @newTaskID OUTPUT,@messages OUTPUT,
		null, --ID
		@parentTaskID, --Parent,
		@strNTLogin,--Requestor
		@personID, --REQUESTEE_ID,
		@requesteeRole, --GROUP_REQUESTEE_ID
		@taskName, --DESCRIPTION 
		@taskName, --Title
		@systemID, --SYSTEM_TASK,
		null, --COMMENT,
		'3', --Security Level
		null, --counter
		@origStartDate, --orig planned Start Date
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
		@strNTLogin --strNTLogin
	
SELECT @assName =
	'[' + p.ID + ']<<b>><<bb>> ' +
	p.FULL_NAME
	+ '<</bb>><</b>>(' + isNull(c.PARENT_NAME + '-','') + c.NAME + ')'
FROM A_V_PEOPLE_APPROVED_DATA p, A_V_COMPANIES_APPROVED_DATA c 
WHERE p.ID = @personID and p.COMPANY = c.ID
UPDATE A_TASKS SET PROCEDURE_ID = @procID WHERE ID = @newTaskID


if @parentTaskID is null
	exec A_SP_TASK_SUBMIT null,null,@newTaskID,@strNTLogin
else
	UPDATE A_TASKS SET STATUS = 'PENDING_PARENT_ACCEPTANCE' WHERE ID = @newTaskID

exec A_SP_TASK_CREATE_CHILD_TASKS_FOR_PROCEDURE_STEPS @newTaskID,@strNTLogin

if (@personID is not null)
begin
declare @cur cursor,@tID varchar(50),@comments varchar(4000)
set @comments = 'Assignee = ' + @assName + ''
set @cur = CURSOR FOR SELECT ID FROM A_TASKS WHERE (ID = @newTaskID OR PARENT_ID = @newTaskID)
open @cur
fetch next from @cur into @tID
while @@fetch_status = 0
	begin
	exec A_SP_TASK_COMMENT_UPDATE_ONE_COMMENT null,null,null,@tID,@COMMENTS,'ALL',@strNTlogin
	INSERT INTO A_TASK_PROCEDURE_ASSIGNEE (TASK_ID,ASSIGNEE_ID,DRCM,MODBY)
		VALUES (@tID,@personID,getdate(),@strNTLogin)
	fetch next from @cur into @tID
	end
close @cur
deallocate @cur
end

fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_PROCEDURE_ASSIGN_TO_PEOPLE with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_PROCEDURE_ASSIGN_TO_PEOPLE and we will terminate and not finish anything '
return 1
raiserror('Problem creating an actual part',16,1)






















