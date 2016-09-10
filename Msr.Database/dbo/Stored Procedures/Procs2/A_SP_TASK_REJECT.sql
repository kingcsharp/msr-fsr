




CREATE           PROCEDURE A_SP_TASK_REJECT
	@newObjID as varchar(50) OUTPUT,
	@messages as nvarchar(500) OUTPUT,
	@taskID varchar(50),
	@reason nvarchar(2000),
	@strNTLogin varchar(50)
AS

declare @curPerReq as varchar(50)
declare @curGroupReq as varchar(50)

SELECT @curPerReq = REQUESTEE_ID,@curGroupReq = GROUP_REQUESTEE_ID FROM A_TASKS WHERE ID = @taskID
if @curPerReq is not null
	begin
	if @curPerReq != @strNTLogin  
		begin
		set @newObjID = 'ERROR - You are not the assignee'
		goto fin
		end
	end
else
	begin
	print 'You are not the assignee...  so are you in the group assigned?'
	if @strNTLogin not in (SELECT PERSON FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS WHERE ROLE_ID = @curGroupReq)
		begin
		set @newObjID = 'ERROR - You are nto in this asssigned Group'
		goto fin
		end
	else
		begin
		print 'You are a member of the group so accept it first'
		declare @RET_STATUS2 as varchar(50),
				@MSGS2 as varchar(50)
		exec A_SP_TASK_ACCEPT @RET_STATUS2 OUTPUT,@MSGS2 OUTPUT,@taskID,@strNTLogin
		print 'Finished Accepting now doing something else Ret Status = ' + isNull(@RET_STATUS2,'NULL')
		if @RET_STATUS2 is not null
			begin
				print 'Error Accepting this task'
				print 'ERROR: ' + @RET_STATUS2
				goto fin
			end
		end
	end
print 'lets get the data about the current Task Assignee'
declare @requestee as varchar(50)
declare @roleRequestee as varchar(50)
declare @aID as varchar(50)
declare @requestor as varchar(50)
SELECT @requestor = REQUESTOR,@requestee = PERSON_ASSIGNED,@roleRequestee = ROLE_ASSIGNED,@aID = ID FROM A_TASK_ASSIGNEE WHERE TASK_ID = @taskID AND ACTIVE = 1
print'Reject the current assignment'
exec A_SP_TASK_SEND_EMAIL_UPDATE @taskID,'rejected',@strNTLogin
UPDATE A_TASK_ASSIGNEE SET STATUS = 'REJECTED', REJECTED_BY = @strNTLogin, 
REASON_REJECTED = @reason, REJECTION_DATE = getDate(),ACTIVE = 0,ASSIGNEE_CURRENTLY_ACCEPTED = 0,DRCM = getDate()
WHERE ID = @aID
print 'Assign it to the requestor'
INSERT INTO A_TASK_ASSIGNEE (ID,REQUESTOR,TASK_ID,PERSON_ASSIGNED,ASSIGNEE_CURRENTLY_ACCEPTED,ASSIGN_DATE,ASSIGNER,ACCEPTED_DATE,REQUEST_DATE,DRCM,MODBY,ACTIVE,STATUS)
Values (newID(),@requestor,@taskID,@requestor,1,getDate(),@strNTLogin,getDate(),getDate(),getDate(),@strNTLogin,1,'REQUESTED')
UPDATE A_TASKS SET REQUESTEE_ID = NULL, GROUP_REQUESTEE_ID = NULL WHERE ID = @taskID
exec A_SP_TASK_SET_REQUESTEE_FROM_ASSIGNEE_TABLE @taskID,@strNTLogin
exec A_SP_TASKS_UPDATE_DATA @taskID,@strNTLogin



fin:




