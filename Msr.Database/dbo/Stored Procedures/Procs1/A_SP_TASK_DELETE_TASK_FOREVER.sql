









CREATE          PROCEDURE A_SP_TASK_DELETE_TASK_FOREVER
@ID varchar(50),
@strNTLogin varchar(50)
AS
declare @tester as varchar(50)
SELECT @tester = ID FROM A_TASKS WHERE ID = @ID AND (ORIG_REQUESTOR_ID = @strNTLogin or @strNTLogin IN (SELECT BOSS FROM A_PEOPLE_SUB_LOOKUP_TABLE WHERE SUBORDINATE = ORIG_REQUESTOR_ID))
if @tester is null
	begin
	print 'You can not delete it'
	goto fin
	end

declare @curs as CURSOR,@it varchar(50)
set @curs = CURSOR FOR SELECT ID FROM A_TASKS WHERE PARENT_ID = @ID
open @curs
fetch next from @curs into @it
while @@fetch_status = 0
	begin
	print 'Deleting Child task' + @it
	exec  A_SP_TASK_DELETE_TASK_FOREVER @it,@strNTLogin
	fetch next from @curs into @it
	end
close @curs
deallocate @curs

declare @myParent as varchar(50)
SELECT @myParent = PARENT_ID FROM A_TASKS WHERE ID = @ID
UPDATE A_TASKS SET PARENT_ID = @myPArent WHERE PARENT_ID = @ID
DELETE FROM A_MONITOR_RESULTS WHERE MONITOR_TEMPLATE_ID IN (SELECT ID FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @ID)
DELETE FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @ID
DELETE FROM A_TASKS WHERE ID = @ID


declare @sql varchar(3000)
set @sql = 'exec A_SP_TASK_FIGURE_OUT_CHILD_STATUS ''' + @myParent + ''',''' + @strNTLogin + ''''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @sql,@strNTLogin

set @sql = 'exec A_SP_TASK_CHECK_STATUS ''' + @myParent + ''',''' + @strNTLogin + ''''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @sql,@strNTLogin


fin:








