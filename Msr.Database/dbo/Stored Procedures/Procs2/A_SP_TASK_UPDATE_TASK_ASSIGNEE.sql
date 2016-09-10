


CREATE   PROCEDURE A_SP_TASK_UPDATE_TASK_ASSIGNEE 
@ID as varchar(50),
@strNTLogin as varchar(50)
AS
print ' Updating the Assignee information'
DELETE FROM A_TASK_ASSIGNEE WHERE TASK_ID = @ID
declare @accPer as varchar(50)
declare @accRole as varchar(50)
declare @req as varchar(50)
SELECT @accPer = REQUESTEE_ID,@accRole = GROUP_REQUESTEE_ID,@req=REQUESTOR FROM A_TASKS WHERE ID = @ID
if @accPer is not null
	begin
		print 'A Person is requested so add it'
		INSERT INTO A_TASK_ASSIGNEE (
		ID,TASK_ID,PERSON_ASSIGNED,REQUESTOR,DRCM,MODBY,ACTIVE,STATUS)
		VALUES
		(newID(),@ID,@accPer,@req,getDate(),@strNTLogin,1,'REQUESTED')
	end
else
	begin
		if @accRole is not null
			begin
				print 'A Role is requested so add it'
				INSERT INTO A_TASK_ASSIGNEE (
				ID,TASK_ID,ROLE_ASSIGNED,REQUESTOR,DRCM,MODBY,ACTIVE,STATUS)
				VALUES
				(newID(),@ID,@accRole,@req,getDate(),@strNTLogin,1,'REQUESTED')
			end
	end



