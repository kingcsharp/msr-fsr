






CREATE      PROCEDURE A_SP_TASKS_REASSIGN_TASK
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@ID nvarchar(50),
@REQUESTEE_ID varchar(50),
@GROUP_REQUESTEE_ID varchar(50),
@COMMENTS nvarchar(4000),
@strNTLogin varchar(50)
AS
print 'ReAssigning a Task'

print 'Making sure this is the person who Originally Requested it'
declare @TESTER as varchar(50)
SELECT @TESTER = ID FROM A_TASKS WHERE ORIG_REQUESTOR_ID = @strNTLogin and ID = @ID
if @TESTER is null
	begin
	print 'This is not yours to reAssign'
	set @newID = 'Not yours to reAssign'
	goto fin
	end
if (@REQUESTEE_ID is not null) and (@GROUP_REQUESTEE_ID is not null)
	begin
	print 'Assigned both a group and a person exiting'
	set @newID = 'Assigned to both a person and a group does not work'
	goto fin
	end

print 'I am going to reassign this'
print 'Get the current Active Task Assignee'
declare @curID as varchar(50)
SELECT @curID = ID FROM A_TASK_ASSIGNEE WHERE TASK_ID = @ID AND ACTIVE = '1'
if @curID is NULL
	begin
	print 'I can not find the current Active Task Assignee'
	set @newID = 'I can not find the current Active Task Assignee'
	goto fin
	end
print 'Set the old one to ReAssigned and inactive'
UPDATE A_TASK_ASSIGNEE SET STATUS = 'REASSIGNED', ACTIVE = 0,ASSIGNEE_CURRENTLY_ACCEPTED = 0 WHERE ID = @curID
print 'Now insert a new one into A_TASK_ASSIGNEE '
INSERT INTO A_TASK_ASSIGNEE (ID,TASK_ID,PERSON_ASSIGNED,ROLE_ASSIGNED,REQUEST_DATE,DRCM,MODBY,ACTIVE,STATUS,
REQUESTOR)
VALUES (newID(),@ID,@REQUESTEE_ID,@GROUP_REQUESTEE_ID,getDate(),getDate(),@strNTLogin,'1','REQUESTED',@strNTLogin)
print 'Adding the ReAssignment Comments to the comments box.'
UPDATE A_TASKS SET COMMENT = isNull(COMMENT,'') + char(13) + @COMMENTS WHERE ID = @ID

exec A_SP_TASK_SET_REQUESTEE_FROM_ASSIGNEE_TABLE @ID,@strNTLogin
exec A_SP_TASKS_UPDATE_DATA @ID, @strNTlogin
exec A_SP_TASK_AUTO_ACCEPT @ID,@strNTLogin

exec A_SP_TASK_SEND_EMAIL_UPDATE @ID,'reassigned',@strNTLogin


fin:







