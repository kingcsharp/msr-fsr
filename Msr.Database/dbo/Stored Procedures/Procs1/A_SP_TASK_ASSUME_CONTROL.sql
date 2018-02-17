Create PROCEDURE dbo.A_SP_TASK_ASSUME_CONTROL 
@RET_STATUS as varchar(500) OUTPUT,
@MSGS as varchar(500) OUTPUT,
@ID as varchar(50),
@strNTLogin as varchar(50)
AS
print 'Assuming a task'
print 'Verifying that I am a person who can accept it'
declare @requestee as varchar(50)
declare @roleRequestee as varchar(50)
SELECT @requestee = REQUESTEE_ID,@roleRequestee = GROUP_REQUESTEE_ID FROM A_TASKS WHERE ID = @ID
declare @tester as varchar(50),@taskStat varchar(50)
SELECT @taskStat = STATUS FROM A_TASKS WHERE ID = @ID
--if @taskStat not in ('ACCEPTED')
--	begin
--	set @RET_STATUS = 'ERROR - Please start the task to take over this task.'
--	goto fin
--	end

if @roleRequestee is not null
	begin
	print 'This is a request to  role ' + @roleRequestee
	if not exists (SELECT ID FROM A_PERSON_ROLES WHERE PERSON_ID = @strNTLogin AND ROLE_ID = @roleRequestee)
		begin
		set @RET_STATUS = 'ERROR - You are not a member of the role that this task was assigned to.'
		goto fin
		end
	end
else
	begin
	set @RET_STATUS = 'ERROR - This task was never assigned to a group, so I do not know how you can assume it.'
	goto fin
	end


declare @fwdID varchar(50),@fwdMSG varchar(50)
exec A_SP_TASKS_FORWARD_TASK 
@fwdID OUTPUT,
@fwdMSG OUTPUT,
@ID,@strNTLogin,null,@requestee

declare @accStat varchar(50),@accMSG varchar(50)
exec A_SP_TASK_ACCEPT 
@accStat OUTPUT,
@accMSG OUTPUT,
@ID,@strNTLogin


fin: 















