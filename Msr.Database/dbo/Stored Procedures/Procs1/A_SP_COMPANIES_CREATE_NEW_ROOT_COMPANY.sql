






CREATE        PROCEDURE [dbo].[A_SP_COMPANIES_CREATE_NEW_ROOT_COMPANY] 
@objID varchar(50),
@strNTLogin varchar(50)
AS
BEGIN TRANSACTION
print 'We are creating a new root company using objID = ' + isNull(@objID,'NULL')
declare @CO_ID as varchar(50),
	@PARENT_ID as varchar(50),
	@approvalStatus as varchar(50),
	@rev as smallint
SELECT @CO_ID = ID, @PARENT_ID = PARENT FROM A_COMPANIES_HISTORY WHERE OBJECT_ID = @objID
if @PARENT_ID is not null
	goto fin
print 'The Parent ID is null so this is a root company'
print 'Check to see if this is an approved object and it is rev 1'
SELECT @approvalStatus = STATUS,@rev = REV FROM A_OBJECTS WHERE ID = @objID
if @approvalStatus <> 'APPROVED' or @rev <> 1
	goto fin
print 'It is an approved company, so getting the person to make'
declare @loginName as varchar(50),
@newTester as varchar(50)
SELECT @loginName = LOGIN_NAME FROM A_COMPANIES_NEW_PERSON_DATA WHERE CO_ID = @CO_ID
SELECT @newTester = ID FROM A_PEOPLE_HISTORY WHERE LOGIN = @loginName
while @newTester is not null
	begin
	print 'There was already someone with user ID = ' + @loginName
	set @loginName = @loginName + '_1'
	print 'LoginName changed to ' + @loginName
	set @newTester = null
	print 'ReSelecting'
	SELECT @newTester = ID FROM A_PEOPLE_HISTORY WHERE LOGIN = @loginName
	print 'Tester = ' + @newTester
	end
print 'The final login that was not already used is ' + @loginName
UPDATE A_COMPANIES_NEW_PERSON_DATA SET LOGIN_NAME = @loginName where CO_ID = @CO_ID
print 'Now I need to make this person and approve them because they are the leader of this company'
declare @adminID as varchar(50)
exec A_SP_COMPANIES_ROOT_CREATE_ADMINISTRATOR @adminID OUTPUT,@CO_ID,@strNTLogin

--###################################################################
--## Insert the SYSTEM Procedures   
--###################################################################
print 'Creating the System Procedures'
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_E_ACCESS','E-Access',@adminID
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_PROVIDE_AND_CONSUMED','Provide and consume',@adminID
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_PROVIDE_AND_STAY','Provide and stay',@adminID
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_PROVIDE_TAKE_BACK','Provide and take back',@adminID
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_RECEIVE','Receive',@adminID
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_SEND','Send',@adminID
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_SHIPPING','Shipping',@adminID
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_REMOVE','Remove',@adminID
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_INSTALL','Install',@adminID
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_CONSUME','Consume',@adminID
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_CREATE','Create',@adminID
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_DNR','Diagnose And Repair',@adminID
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_COMP_TEST','Computerized Test',@adminID
exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK 'SYS_SERIALIZE','Serialize Parts',@adminID


--#######################################################################
--##Add the First WorkFlow which is for Administration
--#######################################################################
print '########   Adding a WorkFlow'
declare @gID as varchar(50),@sID as varchar(50),@wfID as varchar(50)
exec sp_GetUniqueID3 @gID OUTPUT
exec sp_GetUniqueID3 @sID OUTPUT
exec sp_GetUniqueID3 @wfID OUTPUT

INSERT INTO A_WF_GROUPS (ID,NAME,DRCM,MODBY) VALUES (@gID,'Admins',getDate(),@adminID)  
if @@ERROR <> 0 GOTO Problem
INSERT INTO A_WF_GROUP_PEOPLE_LINK (WF_GROUP_ID,USER_ID,DRCM,MODBY) VALUES (@gID,@adminID,getDate(),@adminID)  
if @@ERROR <> 0 GOTO Problem
INSERT INTO A_WF_STAGES (ID,NAME,DRCM,MODBY) VALUES (@sID,'Admin Stage',getDate(),@adminID)  
if @@ERROR <> 0 GOTO Problem
INSERT INTO A_WF_STAGE_GROUP_LINK (WF_STAGE_ID,WF_GROUP_ID,DRCM,MODBY) VALUES (@sID,@gID,getDate(),@adminID)  
if @@ERROR <> 0 GOTO Problem
INSERT INTO A_WORKFLOWS (ID,NAME,DRCM,MODBY) VALUES (@wfID,'Admin Workflow',getDate(),@adminID)  
if @@ERROR <> 0 GOTO Problem
INSERT INTO A_WORKFLOW_STAGE_LINK (WF_ID,WF_STAGE_ID,NUM,DRCM,MODBY) VALUES (@wfID,@sID,'1',getDate(),@adminID)  
if @@ERROR <> 0 GOTO Problem
INSERT INTO A_WF_ACT_LINK (ID,WF_ID,ACT_ID,DRCM,MODBY) SELECT newID(),@wfID,ID,getDate(),@adminID FROM A_WF_ACTIVITIES
if @@ERROR <> 0 GOTO Problem


fin:
COMMIT TRANSACTION
print 'Normal Finish Creating a Company'
return 0

Problem:
print 'The bad news is we are not going to finish making this company.'
if @@TRANCOUNT > 0 ROLLBACK TRANSACTION
Return 1








