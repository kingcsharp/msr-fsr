



CREATE   PROCEDURE A_SP_APPROVALS_DENY_ONE
@newID varchar(50) OUTPUT,
@msg nvarchar(500) OUTPUT,
@WFS_ID nvarchar(50),
@WF_GROUP_ID nvarchar(50),
@WF_STAGE_ID nvarchar(50),
@reason nvarchar(4000),
@strNTLogin nvarchar(50)
AS
print 'Entering A_SP_APPROVALS_DENY_ONE'
set @msg = 'Denying group where WFS_ID = ' + @WFS_ID + ' WF_STAGE_ID = ' + @WF_STAGE_ID + ' WF_GROUP_ID = ' + @WF_GROUP_ID
set @msg = @msg + 'with Reason = ' + isNull(@reason,'Null')
declare @tester as nvarchar(50)
SELECT @tester = g.ID FROM A_WORKFLOW_GROUP_STARTED g,A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS p WHERE
g.ID = p.WFGS_ID AND
g.WFS_ID = @WFS_ID AND 
g.WF_STAGE_ID = @WF_STAGE_ID AND 
g.WF_GROUP_ID = @WF_GROUP_ID AND
p.PERSON = @strNTLogin

if @tester is null
	begin
		set @msg = @msg + ' There is no WFGS that meets this criteria'
		print ' There is no WFGS that meets this criteria '
	end
 else
	begin
		exec A_SP_WF_DENY_GROUP_STARTED @tester,@strNTLogin,@reason,@strNTLogin
		set @msg = @msg + 'Denied the group ' + @tester
		print 'Denied the group ' + @tester
	end





