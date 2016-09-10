



CREATE      PROCEDURE A_SP_TASKS_FORWARD_TASK
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@ID nvarchar(50),
@REQUESTEE_ID varchar(50),
@GROUP_REQUESTEE_ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Forwarding a Task'

print 'Making sure this is the person assigned and accepted'
declare @TESTER as varchar(50)
SELECT @TESTER = ID FROM A_TASKS WHERE REQUESTEE_ID = @strNTLogin and ID = @ID
if @TESTER is null
	begin
	print 'This is not yours to forward'
	set @newID = 'Not yours to forward'
	goto fin
	end
if (@REQUESTEE_ID is not null) and (@GROUP_REQUESTEE_ID is not null)
	begin
	print 'Assigned both a group and a person exiting'
	set @newID = 'Assigned to both a person and a group does not work'
	goto fin
	end

print 'I am going to Forward this'
print 'Get the current Active Task Assignee'
declare @curID as varchar(50)
SELECT @curID = ID FROM A_TASK_ASSIGNEE WHERE TASK_ID = @ID AND ACTIVE = '1'
if @curID is NULL
	begin
	print 'I can not find the current Active Task Assignee'
	set @newID = 'I can not find the current Active Task Assignee'
	goto fin
	end
print 'Set the old Task Assignee to forwarded and inactive'
UPDATE A_TASK_ASSIGNEE SET STATUS = 'FORWARDED', ACTIVE = 0,ASSIGNEE_CURRENTLY_ACCEPTED = 0 WHERE ID = @curID
print 'Set the Task itself to have no assignee at all because we will get one later'
UPDATE A_TASKS SET REQUESTEE_ID = NULL, GROUP_REQUESTEE_ID = NULL WHERE ID = @ID
print 'Now insert a new one into A_TASK_ASSIGNEE '
INSERT INTO A_TASK_ASSIGNEE (ID,TASK_ID,PERSON_ASSIGNED,ROLE_ASSIGNED,REQUEST_DATE,DRCM,MODBY,ACTIVE,STATUS,
REQUESTOR)
VALUES (newID(),@ID,@REQUESTEE_ID,@GROUP_REQUESTEE_ID,getDate(),getDate(),@strNTLogin,'1','REQUESTED',@strNTLogin)

exec A_SP_TASK_SET_REQUESTEE_FROM_ASSIGNEE_TABLE @ID,@strNTLogin
exec A_SP_TASKS_UPDATE_DATA @ID, @strNTlogin
exec A_SP_TASK_AUTO_ACCEPT @ID,@strNTLogin
exec A_SP_TASK_SEND_EMAIL_UPDATE @ID,'forwarded',@strNTLogin
fin:




