






CREATE       PROCEDURE A_SP_TASK_SET_REQUESTEE_FROM_ASSIGNEE_TABLE
@ID varchar(50),
@strNTlogin varchar(50)
AS
declare @PER as varchar(50)
declare @ROLE as varchar(50)
declare @REQ as varchar(50)
declare @REQ_DATE as varchar(50)
declare @STATUS as varchar(50)

SELECT @REQ_DATE = REQUEST_DATE,@STATUS = STATUS,@REQ = REQUESTOR, @PER = PERSON_ASSIGNED, @ROLE = ROLE_ASSIGNED FROM A_TASK_ASSIGNEE WHERE TASK_ID = @ID AND ACTIVE = 1
IF @PER is not null
	begin
	print 'The person is not null in the active task assignee row so setting the person to ' + @PER
	UPDATE A_TASKS SET REQUESTEE_ID = @PER, REQUESTOR = @REQ,DRCM = getDate(),MODBY = @strNTLogin WHERE ID = @ID
	end
IF @ROLE is not null
	begin
	print 'The Role is not null in the active task assignee row so setting the Role to ' + @ROLE
	UPDATE A_TASKS SET REQUESTEE_ID = NULL,GROUP_REQUESTEE_ID = @ROLE, REQUESTOR = @REQ,DRCM = getDate(),MODBY = @strNTLogin WHERE ID = @ID
	end

declare @curStat as varchar(50)
SELECT @curStat = STATUS FROM A_TASKS WHERE ID = @ID
if @STATUS = 'REQUESTED'
	begin
	if isNull(@curStat,'') != 'REQUESTED' 
		begin
		UPDATE A_TASKS SET 
		--STATUS = 'REQUESTED',
		DRCM = getDate(),MODBY = @strNTLogin WHERE ID = @ID
		end
	if @REQ_DATE is not null
		begin
		UPDATE A_TASKS SET LAST_REQUEST_DATE = @REQ_DATE WHERE ID = @ID
		end
	end
if @STATUS = 'ACCEPTED'
	begin
	if isNull(@curStat,'') = 'REQUESTED' --If it is not accepted or finished we need to update it
		begin
		UPDATE A_TASKS SET STATUS = 'ACCEPTED',DRCM = getDate(),MODBY = @strNTLogin WHERE ID = @ID
		end
	end

if @REQ_DATE is not null
	begin
	UPDATE A_TASKS SET LAST_REQUEST_DATE = @REQ_DATE WHERE ID = @ID
	end
else
	begin
	SELECT top 1 @REQ_DATE = REQUEST_DATE FROM A_TASK_ASSIGNEE WHERE TASK_ID = @ID AND REQUEST_DATE is not null ORDER BY REQUEST_DATE DESC 
	UPDATE A_TASKS SET LAST_REQUEST_DATE = @REQ_DATE WHERE ID = @ID
	end







