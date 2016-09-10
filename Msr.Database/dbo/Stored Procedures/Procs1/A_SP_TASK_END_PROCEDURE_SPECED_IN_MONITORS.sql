



CREATE    PROCEDURE dbo.A_SP_TASK_END_PROCEDURE_SPECED_IN_MONITORS
@taskID varchar(50),
@strNTLogin varchar(50)
AS
print 'Checking monitors to see if we need to stop the procedure.'
begin transaction

declare @curs2 as cursor,@childID as varchar(50),@myParent2 varchar(50)
if not exists(SELECT * FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @taskID AND IS_PASSING = 0 AND FAIL_ACTION = 'ENDPROCEDURE')
	begin
	print 'This task will not end the procedure'
	goto fin
	end

print 'There is a failing monitor that also says close the procedure.'
declare @requestorID varchar(50),@requesteeID varchar(50),@procID varchar(50),@stepID varchar(50),@PARENT_ID varchar(50)
SELECT @PARENT_ID = PARENT_ID FROM A_TASKS WHERE ID = @taskID
declare @curs as  cursor,@it as varchar(50),@r varchar(50)
set @curs = cursor for SELECT ID,ORIG_REQUESTOR_ID
				FROM A_TASKS 
				WHERE PARENT_ID = @PARENT_ID AND STATUS NOT IN ('FINISHED','CLOSED') ORDER BY ID
open @curs
fetch next from @curs into @it,@r
while @@fetch_status = 0
	begin
	set @curs2 = CURSOR FOR SELECT ID FROM A_TASKS WHERE PARENT_ID = @it
	open @curs2
	fetch next from @curs2 into @childID
	while @@fetch_status = 0
		begin
		print 'Deleting a Child task (should not happen much)' + @childID
		exec  A_SP_TASK_DELETE_TASK_FOREVER @childID,@strNTLogin
		fetch next from @curs2 into @childID
		end
	close @curs2
	deallocate @curs2

	SELECT @myParent2 = PARENT_ID FROM A_TASKS WHERE ID = @it
	UPDATE A_TASKS SET PARENT_ID = @myParent2 WHERE PARENT_ID = @it
	DELETE FROM A_MONITOR_RESULTS WHERE MONITOR_TEMPLATE_ID IN 
					(SELECT ID FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @it)
	DELETE FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @it
	DELETE FROM A_TASKS WHERE ID = @it
	fetch next from @curs into @it,@r
	end

declare @sql varchar(3000)
set @sql = 'exec A_SP_TASK_FIGURE_OUT_CHILD_STATUS ''' + @PARENT_ID + ''',''' + @strNTLogin + ''''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @sql,@strNTLogin

set @sql = 'exec A_SP_TASK_CHECK_STATUS ''' + @PARENT_ID + ''',''' + @strNTLogin + ''''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @sql,@strNTLogin




fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_TASK_END_PROCEDURE_SPECED_IN_MONITORS with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_TASK_END_PROCEDURE_SPECED_IN_MONITORS and we will terminate and not finish anything '
return 1

	



