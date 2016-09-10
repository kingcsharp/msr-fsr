



CREATE           PROCEDURE A_SP_OBJECT_START_WF 
	@msg varchar(500) OUTPUT,
	@msg2 varchar(500) OUTPUT,
	@objID varchar(50),
	@wfID varchar(50),
	@revComment varchar(2000),
	@statOnCompletion varchar(50),
	@allRevs char(1),
	@strNTLogin varchar(50)
AS
Begin Transaction
set @msg = 'Starting the WF for an object'
--We are going to need an ID so set it up
declare @wfsID as nvarchar(30)
exec sp_GetUniqueID3 @wfsID OUTPUT
--We need to create a workflow that will be started for this object
print 'Creating a Workflow with the Id = ' + @wfsID
set @msg = @msg + 'Creating a Workflow with the Id = ' + @wfsID
INSERT INTO A_WORKFLOWS_STARTED (ID,WF_ID,STARTED_BY,START_DATE,STATUS_ON_COMPLETION,ALL_REVS,MODBY,DRCM)
VALUES(@wfsID,@wfID,@strNTLogin,getDate(),@statOnCompletion,@allRevs,@strNTLogin,getDate())
if @@ERROR <> 0 goto Problem
UPDATE A_OBJECTS SET
WFS_ID = @wfsID
WHERE ID = @objID
if @@ERROR <> 0 goto Problem
--Save the revision comments to this object, Set the Status and set the WFS_ID
print 'Updating the Objects table setting the REV_INFO = ' + @revComment + ' Setting status = IN_WORKFLOW or APPROVED_BUT_DELETING'
if @statOnCompletion = 'APPROVED'
	begin
		set @msg = @msg + 'Updating the Objects table setting the REV_INFO = ' + @revComment + ' Setting status = IN_WORKFLOW'
		UPDATE A_OBJECTS SET
			REV_INFO = @revComment,STATUS = 'IN_WORKFLOW',
			LOCKED_BY = NULL,UNLOCKED_BY = @strNTLogin
		WHERE ID = @objID
		if @@ERROR <> 0 goto Problem
	end
if @statOnCompletion = 'DELETED'
	begin
		set @msg = @msg + 'Updating the Objects table setting the status = APPROVED_BUT_DELETING'
		UPDATE A_OBJECTS SET
			STATUS = 'APPROVED_BUT_DELETING'
		WHERE ID = @objID
		if @@ERROR <> 0 goto Problem
	end

--Now That we started the workflow we need to tell the system to get to work on the workflow
print 'Calling Update that workflow'
set @msg = @msg + 'Calling Update that workflow'
exec A_SP_WORKFLOW_STARTED_UPDATE @wfsID,@strNTLogin
if @@ERROR <> 0 goto Problem

COMMIT Transaction
return 0

Problem: 
print 'Error Submiting to workflow  Transactions Rolled back and item was not submitted'
if @@trancount > 0 	ROLLBACK TRANSACTION
Return 1





