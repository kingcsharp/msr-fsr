



CREATE   PROCEDURE A_SP_APPROVALS_FURTHER_EDIT
@msg nvarchar(500) OUTPUT,
@theID nvarchar(50) OUTPUT,
@WFS_ID nvarchar(50),
@WF_STAGE_ID nvarchar(50),
@WF_GROUP_ID nvarchar(50),
@APPROVER nvarchar(50),
@strNTLogin nvarchar(50)
AS
declare @message as nvarchar(500),@newID varchar(50)
print 'Entering A_SP_APPROVALS_APPROVE_GROUP'
print 'First we willl deny the current workflow'
exec A_SP_APPROVALS_DENY_ONE
	@newID OUTPUT,
	@message OUTPUT,
	@WFS_ID,
	@WF_GROUP_ID,
	@WF_STAGE_ID,
	'A_SP_APPROVALS_FURTHER_EDIT',
	@strNTLogin 
print 'Then we will change the locked by to be us'
--we need the object ID
declare @objID as nvarchar(50)
SELECT @objID = ID FROM A_OBJECTS WHERE WFS_ID = @WFS_ID
UPDATE A_OBJECTS SET LOCKED_BY = @strNTLogin WHERE ID = @objID
SET @theID = @objID





