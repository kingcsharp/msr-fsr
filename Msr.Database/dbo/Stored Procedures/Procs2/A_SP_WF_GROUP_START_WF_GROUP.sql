








CREATE        PROCEDURE A_SP_WF_GROUP_START_WF_GROUP
	@wfsID nvarchar(50),
	@wfStage nvarchar(50),
	@wfGroup nvarchar(50),
	@strNTLogin nvarchar(50)

AS
BEGIN TRANSACTION
declare @myID as nvarchar(50)
SELECT @myID = ID FROM A_WORKFLOW_GROUP_STARTED 
	WHERE WFS_ID = @wfsID AND WF_STAGE_ID = @wfstage AND WF_GROUP_ID = @wfGroup
print 'So now I am in A_SP_START_WF_GROUP'
if @myID is null
	begin
		exec sp_GetUniqueID3 @myID OUTPUT
		print 'Inserting a new GRoup with ID = ' + @wfGroup
		INSERT INTO A_WORKFLOW_GROUP_STARTED (ID,WFS_ID,WF_STAGE_ID,WF_GROUP_ID,DATE_STARTED,DRCM,MODBY)
		VALUES (@myID,@wfsID,@wfStage,@wfGroup,getDate(),getDate(),@strNTLogin)
		if @@ERROR <> 0 goto problem
		print 'Going to create GRoup Data'
		exec A_SP_WF_CREATE_WORKFLOW_GROUP_DATA @myID,@strNTLogin
		if @@ERROR <> 0 goto problem
	end
--Now we need to check and see if the initiator was in the group
--If the initiator was then we need to finish that group
print 'Group Data Creation completed'
print 'Now we need to see if the group has a member that is the same as the starter'
declare @Curs cursor
Set @Curs = Cursor for SELECT WFGS_ID,PERSON from A_V_WF_PENDING_APPROVALS_WITH_STARTER_AND_INITER WHERE PERSON = STARTED_BY
declare @myGS nvarchar(50)
declare @myPerson nvarchar(50)
Open @Curs
Fetch Next from @Curs Into @myGS,@myPerson
while (@@fetch_status = 0)
	Begin
		print 'This groupStarted ' + @myGS
		print 'has a member that is the starter so we should finish it ' + @myPerson
		exec A_SP_WF_FINISH_GROUP_STARTED @myGS,@myPerson,@strNTLogin
		if @@ERROR <> 0 goto problem
		Fetch Next from @Curs Into @myGS,@myPerson
	End
print 'We just finished the goups if they had a member that was the starter'
exec A_SP_WF_GROUP_SEND_EMAILS_ABOUT_PENDING_APPROVAL @myID,@strNTLogin
fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_WF_FINISH_GROUP_STARTED with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_WF_FINISH_GROUP_STARTED and we will terminate and not finish anything '
return 1



