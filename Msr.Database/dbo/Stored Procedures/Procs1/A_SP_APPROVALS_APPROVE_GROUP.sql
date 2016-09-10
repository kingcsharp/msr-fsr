


CREATE          PROCEDURE A_SP_APPROVALS_APPROVE_GROUP
@msg nvarchar(500) OUTPUT,
@WFS_ID nvarchar(50),
@WF_STAGE_ID nvarchar(50),
@WF_GROUP_ID nvarchar(50),
@APPROVER nvarchar(50),
@strNTLogin nvarchar(50)
AS
print 'Entering A_SP_APPROVALS_APPROVE_GROUP'
print 'Starting the transaction'
begin transaction
set @msg = 'Approving one group where Approver = ' + @APPROVER
declare @tester as nvarchar(50)
SELECT @tester = g.ID FROM A_WORKFLOW_GROUP_STARTED g,A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS p WHERE
g.ID = p.WFGS_ID AND
g.WFS_ID = @WFS_ID AND 
g.WF_STAGE_ID = @WF_STAGE_ID AND 
g.WF_GROUP_ID = @WF_GROUP_ID AND 
p.PERSON = @APPROVER
if @tester is null
	begin
		set @msg = @msg + ' There is no WFGS that meets this criteria'
		print ' There is no WFGS that meets this criteria '
		goto PROBLEM
	end
 else
	begin
		declare @retVal as smallInt
		exec A_SP_WF_FINISH_GROUP_STARTED @tester,@strNTLogin,@strNTLogin
		if @@error <> 0 goto PROBLEM
		set @msg = @msg + 'Finished the group ' + @tester
		print 'Finished the group ' + @tester
	end
print 'Now updating the workflow for this workflow ' + @WFS_ID
exec A_SP_WORKFLOW_STARTED_UPDATE @WFS_ID,@strNTLogin
if @@error <> 0 goto PROBLEM
print 'Done Updating'
	
fin:
print 'Finished the approval Group with no problem'
if @@trancount > 0 COMMIT TRANSACTION
return 0

PROBLEM:
print 'There was some sort of problem and we will not be finishing approving this group'
if @@TRANCOUNT > 0 ROLLBACK TRANSACTION
return 1

