


CREATE            PROCEDURE A_SP_TASKS_UPDATE_DATA 
@ID as varchar(50),
@MODBY as varchar(50)
AS

--print ' Updating all the task information'
--print 'We need to set the original Requestor of this task'
declare @origReq as varchar(50)
SELECT top 1 @origReq = REQUESTOR  FROM A_TASK_ASSIGNEE WHERE TASK_ID = @ID ORDER BY REQUEST_DATE
--print 'The original Requestor for this task was' + @origReq
UPDATE A_TASKS SET ORIG_REQUESTOR_ID = @origReq WHERE ID = @ID

--print 'Get the current person who has accepted the task'
declare @accPer as varchar(50)
declare @accRole as varchar(50)
SELECT @accPer = PERSON_ASSIGNED,@accRole = ROLE_TASK_ACCEPTED_UNDER FROM A_TASK_ASSIGNEE WHERE TASK_ID = @ID AND ASSIGNEE_CURRENTLY_ACCEPTED = 1
if @accPer is null
	begin
--		print 'No Person is the current accepted assignee of this task'
--		print 'So get the latest requested Role for this task'
		SELECT @accRole = ROLE_ASSIGNED FROM A_TASK_ASSIGNEE WHERE TASK_ID = @ID AND ACTIVE = 1
		if @accRole is null
			begin
--			print 'There is no Role requested, so we need to get the persons id'
			SELECT @accPer = PERSON_ASSIGNED FROM A_TASK_ASSIGNEE WHERE TASK_ID = @ID AND ACTIVE = 1
			end
	end
--print 'Now we have the data...  person = ' + isNull(@accPer,'Null') + ' role = ' + isNull(@accRole,'Null')
--print 'Now to get the names'
declare @accRoleName as nvarchar(200)
declare @comboNAme as nvarchar(200)
if @accRole is not null
	begin
--		print 'Get the Role Name First'
		SELECT @accRoleName = NAME FROM A_V_ROLE_DATA_BY_APPROVED_ID WHERE ID = @accRole
		set @comboname = @accRoleName
	end
declare @accPerName as nvarchar(200)
if @accPer is not null
	begin
--		print 'Now add the person'
		SELECT @accPerName = P_NAME FROM A_V_PEOPLE_BY_NTLOGIN WHERE P_ID = @accPer
		set @comboName = isNull(@comboname + ' - ','') + @accPerName
	end
declare @curReqName as nvarchar(200)
SELECT @curReqName = LATEST_REQUESTEE_NAME FROM A_TASKS WHERE ID = @ID
if isNull(@curReqName,'') != isNull(@comboName,'')
	begin
	UPDATE A_TASKS SET LATEST_REQUESTEE_NAME = @comboName WHERE ID = @ID
	end

--print 'Checking the surveys'
declare @surTest as varchar(50)
SELECT @surTest  = ID FROM A_TASK_SURVEY_LINK WHERE TASK_ID = @ID
declare @curSurState as smallint
SELECT @curSurState = HAS_SURVEY FROM A_TASKS WHERE ID = @ID
if @surTest is not null
	begin
--	print 'This one has a survey'
	if  isnull(@curSurState,'') != 1 
		begin
		UPDATE A_TASKS SET HAS_SURVEY = 1 WHERE ID = @ID
		end
	end
else
	begin
--	print 'This one does not has a survey'
	if isnull(@curSurState,'') != 0
		begin
		UPDATE A_TASKS SET HAS_SURVEY = 0 WHERE ID = @ID
		end
	end
declare @discTest as varchar(50)
SELECT @discTest  = ID FROM A_TASK_DISCUSSION_LINK WHERE TASK_ID = @ID
declare @curDiscState as smallint
SELECT @curDiscState = HAS_DISCUSSION FROM A_TASKS WHERE ID = @ID
if @discTest is not null
	begin
--	print 'This one has a discussion'
	if isnull(@curDiscState,'') != 1 
		begin
		UPDATE A_TASKS SET HAS_DISCUSSION = 1 WHERE ID = @ID
		end
	end
else
	begin
--	print 'This one does not has a discussion'	
	if isnull(@curDiscState,'') != 0
		begin
		UPDATE A_TASKS SET HAS_DISCUSSION = 0 WHERE ID = @ID
		end
	end
declare @childTest as varchar(50)
SELECT @childTest  = ID FROM A_TASKS WHERE PARENT_ID = @ID
declare @curChildState as smallint
SELECT @curChildState = HAS_CHILD FROM A_TASKS WHERE ID = @ID
if @childTest is not null
	begin
--	print 'This one have child'
	if isnull(@curChildState,'') != 1 
		begin
		UPDATE A_TASKS SET HAS_CHILD = 1 WHERE ID = @ID
		end
	end
else
	begin
--	print 'This one does not have child'	
	if isnull(@childTest,'') != 0
		begin
		UPDATE A_TASKS SET HAS_CHILD = 0 WHERE ID = @ID
		end
	end

declare @procedureTest as varchar(50)
SELECT @procedureTest  = ID FROM A_TASK_REFERENCE_PROCEDURES WHERE TASK_ID = @ID
declare @curProcState as smallint
SELECT @curProcState = HAS_REF_PROC FROM A_TASKS WHERE ID = @ID
if @procedureTest is not null
	begin
--	print 'This one have child'
	if isnull(@curProcState,'') != 1 
		begin
		UPDATE A_TASKS SET HAS_REF_PROC = 1 WHERE ID = @ID
		end
	end
else
	begin --	print 'This one does not have child'	
	if isnull(@curProcState,'') != 0
		begin
		UPDATE A_TASKS SET HAS_REF_PROC = 0 WHERE ID = @ID
		end
	end

declare @objTest as varchar(50)
SELECT @objTest  = ID FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @ID
declare @curObjState as smallint
SELECT @curObjState = HAS_REF_OBJ FROM A_TASKS WHERE ID = @ID
if @objTest is not null
	begin
--	print 'This one has an object'
	if isnull(@curObjState,'') != 1 
		begin
		UPDATE A_TASKS SET HAS_REF_PROC = 1 WHERE ID = @ID
		end
	end
else
	begin
--	print 'This one does not have Object'	
	if isnull(@curObjState,'') != 0
		begin
		UPDATE A_TASKS SET HAS_REF_PROC = 0 WHERE ID = @ID
		end
	end

declare @fileTest as varchar(50)
SELECT @fileTest  = ID FROM A_TASK_REF_FILES WHERE TASK_ID = @ID
declare @curFileState as smallint
SELECT @curFileState = HAS_FILE FROM A_TASKS WHERE ID = @ID
if @fileTest is not null
	begin
--	print 'This one has a File'
	if isnull(@curFileState,'') != 1 
		begin
		UPDATE A_TASKS SET HAS_FILE = 1 WHERE ID = @ID
		end
	end
else
	begin
--	print 'This one does not have a File'	
	if isnull(@curFileState,'') != 0
		begin
		UPDATE A_TASKS SET HAS_FILE = 0 WHERE ID = @ID
		end
	end

declare @monTest as varchar(50)
SELECT @monTest  = ID FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @ID
declare @curMonState as smallint
SELECT @curMonState = HAS_MONITOR FROM A_TASKS WHERE ID = @ID
if @monTest is not null
	begin
--	print 'This one has a Monitor'
	if isnull(@curMonState,'') != 1 
		begin
		UPDATE A_TASKS SET HAS_MONITOR = 1 WHERE ID = @ID
		end
	end
else
	begin
--	print 'This one does not have a Monitor'	
	if isnull(@curMonState,'') != 0
		begin
		UPDATE A_TASKS SET HAS_MONITOR = 0 WHERE ID = @ID
		end
	end


declare @actStartDate datetime,@parentID varchar(50),@parentStartDate dateTime
SELECT @actStartDate = ACTUAL_START_DATE,@parentID = PARENT_ID FROM A_TASKS WHERE ID = @ID
SELECT @parentStartDate = ACTUAL_START_DATE FROM A_TASKS WHERE ID = @parentID
if @parentStartDate is null and @actStartDate is not null
	begin
	UPDATE A_TASKS SET ACTUAL_START_DATE = @actStartDate WHERE ID = @parentID
	end






