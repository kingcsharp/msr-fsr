




CREATE     PROCEDURE dbo.A_SP_TASKS_TAKE_BACK_TASK
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@ID nvarchar(50),
@strNTLogin varchar(50)
AS

print 'Taking Back a Task'

print 'Making sure this is the person who Originally Requested it'
declare @TESTER as varchar(50)
SELECT @TESTER = ID FROM A_TASKS WHERE ORIG_REQUESTOR_ID = @strNTLogin and ID = @ID
if @TESTER is null
	begin
	print 'This is not yours to Take Back'
	set @newID = 'Not yours to Take Back'
	goto fin
	end

print 'I am going to take it back this'
print 'Get the current Active Task Assignee'
declare @curID as varchar(50)
SELECT @curID = ID FROM A_TASK_ASSIGNEE WHERE TASK_ID = @ID AND ACTIVE = '1'
if @curID is NULL
	begin
	print 'I can not find the current Active Task Assignee'
	set @newID = 'I can not find the current Active Task Assignee'
	goto fin
	end

exec A_SP_TASK_SEND_EMAIL_UPDATE @ID,'takenBack',@strNTLogin
print 'Set the old one to ReAssigned and inactive'
UPDATE A_TASK_ASSIGNEE SET STATUS = 'TAKEN_BACK', ACTIVE = 0,ASSIGNEE_CURRENTLY_ACCEPTED = 0 WHERE ID = @curID
print 'Now insert a new one into A_TASK_ASSIGNEE '
INSERT INTO A_TASK_ASSIGNEE (ID,TASK_ID,PERSON_ASSIGNED,ROLE_ASSIGNED,REQUEST_DATE,DRCM,MODBY,ACTIVE,STATUS,
REQUESTOR)
VALUES (newID(),@ID,@strNTLogin,NULL,getDate(),getDate(),@strNTLogin,'1','REQUESTED',@strNTLogin)

exec A_SP_TASK_SET_REQUESTEE_FROM_ASSIGNEE_TABLE @ID,@strNTLogin
exec A_SP_TASKS_UPDATE_DATA @ID, @strNTlogin
exec A_SP_TASK_AUTO_ACCEPT @ID,@strNTLogin
fin:




