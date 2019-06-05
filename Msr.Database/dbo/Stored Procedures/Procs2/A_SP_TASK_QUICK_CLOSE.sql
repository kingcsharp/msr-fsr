
CREATE   PROCEDURE [dbo].[A_SP_TASK_QUICK_CLOSE]
@RET_STATUS as varchar(500) OUTPUT,
@MSGS as varchar(50) OUTPUT,
@ID as varchar(50),
@strNTLogin as varchar(50)
AS
print 'Quick Closing a task'
declare @curPerReq as varchar(50)
declare @curGroupReq as varchar(50)
declare @parentSys varchar(50)
SELECT @parentSys = SYSTEM_TASK FROM A_TASKS WHERE ID = (SELECT PARENT_ID FROM A_TASKS WHERE ID = @ID)
SELECT @curPerReq = REQUESTEE_ID,@curGroupReq = GROUP_REQUESTEE_ID FROM A_TASKS WHERE ID = @ID
if @curPerReq is not null
	begin
	if @curPerReq != @strNTLogin  
		begin
		set @RET_STATUS = 'ERROR - You are not the assignee. You are ' + @strNTLogin + ' and the assignee is ' + @curPerReq
		goto fin
		end
	end
else
	begin
	print 'You are not the assignee...  so are you in the group assigned'
	if not exists (SELECT ID FROM A_PERSON_ROLES WHERE PERSON_ID = @strNTLogin AND ROLE_ID = @curGroupReq)
		begin
		set @RET_STATUS = 'ERROR - You are not in the assigned group and cannot perform this task.'
		goto fin
		end
	else
		begin
		print 'You are a member of the group so accept it first'
		declare @RET_STATUS2 as varchar(50),
				@MSGS2 as varchar(50)
		exec A_SP_TASK_ACCEPT @RET_STATUS2 OUTPUT,@MSGS2 OUTPUT,@ID,@strNTLogin
		print 'Finished Accepting now doing something else Ret Status = ' + isNull(@RET_STATUS2,'NULL')
		if @RET_STATUS2 is not null
			begin
				print 'Error Accepting this task'
				print 'ERROR: ' + @RET_STATUS2
				goto fin
			end
		end
	end
print 'OK I am allowed to close it.'

exec A_SP_TASK_CHECK_MONITORS_TO_GO_TO_DNR @ID



declare @errMsg varchar(100)
--print 'Check to see if the task has children and should not be closed'
--if exists(SELECT * FROM A_TASKS WHERE
--	ID = @ID AND CHILD_STATUS = 'NOT_FINISHED')
--	begin
--	set @RET_STATUS = 'ERROR - Children tasks not completed'
--	goto fin
--	end


print 'Check to see if the task has any monitors that can not be automatically closed'

declare @curs as cursor,@it varchar(50),@typ varchar(50),@res varchar(4000)
set @curs = cursor for SELECT ID,MONITOR_TYPE FROM A_V_MONITOR_TEMPLATES_WITH_RESULTS 
	WHERE TASK_ID = @ID AND PRINT_RESULT IS NULL
open @curs
fetch next from @curs into @it,@typ
while @@fetch_status = 0
	begin
	DELETE FROM A_MONITOR_RESULTS WHERE MONITOR_TEMPLATE_ID = @it		
	if @typ = 'YES_NO' SELECT @res = YES_NO_ANSWER FROM A_V_MONITOR_TEMPLATES_WITH_RESULTS WHERE ID = @it
	if @typ is not null
		INSERT INTO A_MONITOR_RESULTS (ID,MONITOR_TEMPLATE_ID,TEXT_VAL,PRINT_RESULT,DRCM,MODBY,COMMENT)
			VALUES (newID(),@it,@RES,@RES,getDate(),@strNTLogin,null)
		
	fetch next from @curs into @it,@typ
	end


if exists(SELECT * FROM A_V_MONITOR_TEMPLATES_WITH_RESULTS 
	WHERE TASK_ID = @ID AND PRINT_RESULT IS NULL)
	begin
	set @RET_STATUS = 'ERROR - MONITOR NOT COMPLETED'
	goto fin
	end


if @parentSys <> 'SYS_DNR'
	begin
	print 'Check to see if the task has failing monitors that stop it from closing'
	if exists(SELECT * FROM A_MONITOR_TEMPLATES 
		WHERE TASK_ID = @ID AND IS_PASSING=0 AND FAIL_ACTION not in ('CONTINUE','ENDPROCEDURE'))
		begin
		set @RET_STATUS = 'ERROR - A Failing Monitor Must Be Corrected'
		goto fin
		end
	end

--ADD Code to check monitors
print 'Monitors OK'
print 'Check to see if the task is for quoting an order'
exec A_SP_TASK_FINISH_CHECK_ORDER_QUOTE_DATA @errMsg OUTPUT,@ID
if @errMsg is not null
	begin
	print 'This ones quote is not completed must not close'
	print 'Error msg = ' + @errMsg
	set @RET_STATUS = @errMsg
	goto fin
	end

print 'Check to see if it is already closed'
if exists (SELECT * FROM A_TASKS WHERE ID = @ID AND STATUS IN ('FINISHED','CLOSED'))
	goto fin

print 'Set the Stop Date and Stop Counter if they have not already been set'
declare @actStopDate as dateTime
declare @actStopCount as numeric
declare @cID as varchar(50)
SELECT @cID = COUNTER,@actStopDate = ACTUAL_STOP_DATE,@actStopCount = ACTUAL_COUNTER_STOP FROM A_TASKS WHERE ID = @ID
if @actStopDate is NULL
	begin
		declare @myDate as datetime
		set @myDate = getDate()
		exec A_SP_TASK_DATES_UPDATE @ID,'ACTUAL_STOP',@myDate,@strNTLogin
		exec A_SP_TASK_UPDATE_ALL_DATES @ID,@strNTlogin
		set @actStopDate = getDate()
	end

print 'Now for the timer'
declare @counterVal as numeric
if @cID is not null
	begin
	print 'We have a counter'
	if @actStopCount is NULL
		begin
		print 'The count in the task was null'
		select top 1 @counterVal = VAL FROM A_COUNTERS_RECORDINGS WHERE DATE_RECORDED < @actStopDate ORDER BY DATE_RECORDED DESC
		end
	else
		begin
		print 'The count in the task was not null so we dont need to do anything'
		end
	end

print 'Now check if it is a send or recieve with no action object'
declare @sysID varchar(50),@objID varchar(50),@qty float
SELECT @objID = OBJECT_ID FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @ID
SELECT @qty = QTY FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @objID
SELECT @sysID = SYSTEM_TASK FROM A_TASKS WHERE ID = @ID
if @sysID in ('SYS_RECEIVE','SYS_SEND')
	begin
	if not exists(SELECT * FROM A_TASK_ACTION_OBJECT_LINK WHERE TASK_ID = @ID)
		begin
		INSERT INTO A_TASK_ACTION_OBJECT_LINK
			(ID,TASK_ID,OBJECT_ID,RELATIONSHIP,DRCM,MODBY,QTY)
			SELECT newID(),@ID,OBJECT_ID,'ACTION_OBJECT',getDate(),@strNTLogin,@qty FROM
				A_TASK_OBJECT_LINK WHERE TASK_ID = @ID
		end
	end


print 'Counter and Dates Updated.  Change the Status'

exec A_SP_TASK_FINISH @ID,@strNTLogin

fin:


		print @RET_STATUS













