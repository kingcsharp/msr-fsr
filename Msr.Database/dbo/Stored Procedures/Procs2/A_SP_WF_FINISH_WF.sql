



CREATE              PROCEDURE A_SP_WF_FINISH_WF
@wfsID nvarchar(50),
@strNTLogin nvarchar(50)
as
BEGIN TRANSACTION
declare @STAT as nvarchar(50)
declare @ALL_REVS as char(1)
SELECT 
	@STAT = STATUS_ON_COMPLETION, 
	@ALL_REVS = ALL_REVS
FROM A_WORKFLOWS_STARTED WHERE ID = @wfsID
print '@@@@@@@@@@@@@@@@Finishing the workflow with stat = ' + @STAT + ' and ALL revs = ' + isNull(@ALL_REVS,'NULL')
--First Update the object this was about to the current Status
print 'Updating the object with wfs_id = ' + @wfsID + ' to the Stat'
UPDATE A_OBJECTS SET STATUS = @STAT WHERE WFS_ID = @wfsID
if @@ERROR <> 0 goto problem
--Now if the All Revs is checked find out the root and make all revs
--equal to this status
declare @root as nvarchar(50),@objTable varchar(50)
SELECT @root = ROOT, @objTable = OBJ_TABLE
	FROM A_OBJECTS WHERE WFS_ID = @wfsID

--If we are doing this to all revs then just update them all to deleted
if @ALL_REVS = '1'
	begin
		print 'Updating all things with root = ' + @root + ' to have stat = ' + @STAT
		UPDATE A_OBJECTS SET STATUS = @STAT WHERE ROOT = @root
		if @@ERROR <> 0 goto problem
	end
--If we are not doing it to all revs and we are deleting it then we will need to roll
--back to the old rev.  This means we would roll back to the revision with the highest number
--that is now marked old.
else
	begin
		if @STAT = 'DELETED'
			begin
				print 'We just deleted one revision so we need to set the old one to Approved'
				declare @oldRev as int
				SELECT @oldRev = MAX(REV) FROM A_OBJECTS WHERE ROOT = @ROOT AND STATUS='OLD'
				print 'The old REV is ' + isNull(convert(varchar(10),@oldRev),'NULL')
				if @@ERROR <> 0 goto problem
				if not(@oldRev is null)
					begin
						print 'UPDATE A_OBJECTS SET STATUS = ''APPROVED'' WHERE ROOT = ''' + @ROOT + ''' AND REV = ''' + isnull(convert(varchar(50),@oldRev),'NULL') + ''''
						UPDATE A_OBJECTS SET STATUS = 'APPROVED' WHERE ROOT = @ROOT AND REV = @oldRev
						if @@ERROR <> 0 goto problem
					end
				else
					print 'There were no more revisions so we do not need to set any of them to Approved'
			end
	end

--If this was for approval then we need to set anything that is approved but revising to old
if @STAT = 'APPROVED'
	begin
		Print 'Stat is approved so we need to set the others to old'
		print 'UPDATE A_OBJECTS SET STATUS = ''OLD'' WHERE STATUS LIKE ''APPROVED%'' AND ROOT = ''' + @ROOT + ''' AND WFS_ID <> ''' +  @wfsID + ''''
		UPDATE A_OBJECTS SET STATUS = 'OLD', LOCKED_BY = NULL, LOCKED_BY_NAME=NULL WHERE STATUS LIKE 'APPROVED%' AND ROOT = @ROOT AND (WFS_ID <> @wfsID or WFS_ID is NULL)
		if @@ERROR <> 0 goto problem
	end
--Now we need to finish the Workflow
UPDATE A_WORKFLOWS_STARTED SET FINISHED = 1,FINISHED_DATE = getDate(),DRCM = getDate(),MODBY = @strNTLogin
WHERE ID = @wfsID
if @@ERROR <> 0 goto problem
--Some objects do something special when they are approved so lets do that now
exec A_SP_OBJECT_WF_FINISHED @wfsID,@strNTLogin
if @@ERROR <> 0 goto problem

if @objTable = 'A_ACTUAL_PARTS_HISTORY'
	exec A_SP_ACTUAL_PARTS_UPDATE_SAFETY_LEVEL_DATA @root
if @@ERROR <> 0 goto problem


fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_WF_FINISH_WF with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_WF_FINISH_WF and we will terminate and not finish anything '
return 1




