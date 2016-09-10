





CREATE     PROCEDURE A_SP_WF_FINISH_GROUP_STARTED
@myGS as nvarchar(50),
@approver as nvarchar(50),
@strNTLogin as nvarchar(50)
as
begin Transaction
print 'Finishing a group'
print 'GroupStarted ID = ' + @myGS
print 'Approver = ' + @approver

UPDATE A_WORKFLOW_GROUP_STARTED SET
APPROVED = 1,
APPROVER = @approver,
APPROVE_DATE = getdate(),
FINISHED = 1,
DRCM = getDate(),
MODBY = @strNTLogin
WHERE ID = @myGS
if @@error <> 0 goto PROBLEM

print 'Now deleting the PEOPLE with Pending approvals for this group started ID = ' + @myGS
DELETE FROM A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS WHERE WFGS_ID = @myGS
if @@error <> 0 goto PROBLEM

fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_WF_FINISH_GROUP_STARTED with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_WF_FINISH_GROUP_STARTED and we will terminate and not finish anything '
return 1

