

CREATE                PROCEDURE [dbo].[A_SP_TASK_ACCEPT] 
@RET_STATUS as varchar(50) OUTPUT,
@MSGS as varchar(50) OUTPUT,
@ID as varchar(50),
@strNTLogin as varchar(50)
AS
print 'Accepting a task'
print 'Verifying that I am the person who can accept it'
declare @requestee as varchar(50)
declare @roleRequestee as varchar(50)
SELECT @requestee = REQUESTEE_ID,@roleRequestee = GROUP_REQUESTEE_ID FROM A_TASKS WHERE ID = @ID
declare @tester as varchar(50),@taskStat varchar(50)

--SELECT @taskStat = STATUS FROM A_TASKS WHERE ID = @ID
--if @taskStat = 'ACCEPTED' AND @requestee is null
--	goto grupStuff

--if @taskStat not in ('REQUESTED','CREATING')
--	begin
--	print 'ERROR - The Status of this task does not allow acceptance'
--	set @RET_STATUS = 'ERROR - The Status of this task does not allow acceptance'
--	goto fin
--	end
--grupstuff:
--if @roleRequestee is not null
--	begin
--	print 'This is a request to  role ' + @roleRequestee
--	SELECT @tester = ID FROM A_PERSON_ROLES WHERE ROLE_ID = @roleRequestee AND PERSON_ID = @strNTLogin
--	if @tester is null
--		begin
--		print 'You are not in this role'
--		end
--	end

print 'GOT HERE'
print 'Requestee = '
print @requestee
--if isNULL(@requestee,'') = @strNTLogin or (@tester is not null)
if @strNTLogin is not null
	begin
	print 'You are able to accept this task so setting the status to Accepted'
	--UPDATE A_TASKS SET STATUS = 'ACCEPTED' WHERE ID = @ID
	exec A_SP_TASK_ACCEPT_BATCHED_TASKS @ID,@strNTLogin
	print 'Updated now continueing'
	if @requestee is null
		begin
		print 'This is a requestee accepting for a group'
		declare @curTAID as varchar(50)
		SELECT @curTAID = ID FROM A_TASK_ASSIGNEE WHERE TASK_ID = @ID and ACTIVE = 1
		print 'The ID is ' + @curTAID
		INSERT INTO A_TASK_ASSIGNEE 
		(ID,TASK_ID,PERSON_ASSIGNED,ROLE_ASSIGNED,ASSIGNEE_CURRENTLY_ACCEPTED,
		ASSIGN_DATE,ASSIGNER,ACCEPTED_DATE,REQUESTOR,REQUEST_DATE,
		DRCM,MODBY,ROLE_TASK_ACCEPTED_UNDER,ACTIVE)
		SELECT 
		newID(),TASK_ID,@strNTLogin,NULL,'1',
		getDate(),@strNTLogin,getDate(),REQUESTOR,getDate(),
		getDate(),@strNTLogin,ROLE_ASSIGNED,'1' from A_TASK_ASSIGNEE
		WHERE ID = @curTAID
		print 'Created the new one'
		UPDATE A_TASK_ASSIGNEE SET DRCM = getDate(),MODBY = @strNTLogin,ACTIVE = NULL,
		STATUS = 'ROLE_ACCEPTED'
		WHERE ID = @curTAID
		print 'Finished accepting for a group'
		end
	else
		begin
		print 'This is just a person accepting for himself'
			UPDATE A_TASK_ASSIGNEE SET STATUS='ACCEPTED',ASSIGNEE_CURRENTLY_ACCEPTED = 1,ASSIGN_DATE = getDate(),ASSIGNER=@strNTlogin,
		ACCEPTED_DATE = getDate() WHERE TASK_ID = @ID AND ACTIVE = 1 AND PERSON_ASSIGNED = @requestee
		end
		
	end
else
	begin
	print 'NO Dice = You cannot accept it.'
	--set @RET_STATUS = 'ERROR - You are nto allowed to Accept this task'
	goto fin
	end

exec A_SP_TASK_SET_REQUESTEE_FROM_ASSIGNEE_TABLE @ID,@strNTLogin
print 'Sending an email about the task'
exec A_SP_TASK_SEND_EMAIL_UPDATE @ID,'accepted',@strNTLogin
exec A_SP_TASKS_UPDATE_DATA @ID,@strNTLogin
exec A_SP_TASK_JUST_ACCEPTED_DO_EVERYTHING_NECESSARY @ID,@strNTLogin

fin: 
















