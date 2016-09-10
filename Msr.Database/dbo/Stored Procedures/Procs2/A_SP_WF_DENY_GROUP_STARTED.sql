






CREATE   PROCEDURE A_SP_WF_DENY_GROUP_STARTED
@myGS as nvarchar(50),
@denier as nvarchar(50),
@reason as nvarchar(4000),
@strNTLogin as nvarchar(50)
as
print 'Deying a group'
print 'GroupStarted ID = ' + @myGS
print 'Denier = ' + @denier
UPDATE A_WORKFLOW_GROUP_STARTED SET
DENIER = @strNTLogin,
FINISHED = 1,
DRCM = getDate(),
MODBY = @strNTLogin
WHERE ID = @myGS

print 'Now deleting the PEOPLE with Pending approvals for this group started ID = ' + @myGS
DELETE FROM A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS WHERE WFGS_ID = @myGS

--Now we need to Deny the Workflow as well
declare @myWF as nvarchar(50)
SELECT @myWF = WFS_ID FROM A_WORKFLOW_GROUP_STARTED WHERE ID = @myGS
print 'Denying the workflow started ' + @myWF
exec A_SP_WF_DENY_WF_STARTED @myWF,@myGS,@denier,@reason,@strNTLogin






