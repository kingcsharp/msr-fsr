






CREATE          PROCEDURE A_SP_WORKFLOW_STARTED_UPDATE 
	@wfsID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
BEGIN TRANSACTION
--First Find out what Stage We are on
declare @myStage as nvarchar(50)
declare @myWF_ID as nvarchar(50)
SELECT @myStage = WF_STAGE, @myWF_ID = WF_ID FROM A_WORKFLOWS_STARTED WHERE ID = @wfsID
print 'In A_SP_UPDATE_WORKFLOW_STARTED with wfsID = ' + @wfsID
print 'my current Stage is ' + isNull(@myStage,'NULL')
print 'The WorkFlow is ' + isNull(@myWF_ID,'NULL')
--Now Find the stage that should be currently running
--If the stage is null then the workflow has not been started so
--go get the stage from the workflow stage link table
if @myStage is null
	begin
		print 'Since it was null i need to get the first one.'
		select TOP 1 @myStage = WF_STAGE_ID FROM A_V_WORKFLOWS_WITH_STAGES WHERE isNull(STAGE_HIDE,0) = 0 AND  WF_ID = @myWF_ID ORDER BY NUM
		print 'The first one is number ' + @myStage
		UPDATE A_WORKFLOWS_STARTED SET WF_STAGE = @myStage WHERE ID = @wfsID
		if @@ERROR <> 0 GOTO problem
	end
--now check to see that all the groups in this stage have been started
declare @curGroup Cursor
Set @CurGroup = Cursor for SELECT WF_GROUP_ID from A_V_WF_STAGES_WITH_GROUPS WHERE isnull(GROUP_HIDE,0) = 0 and STAGE_ID = @myStage
declare @myGroupID nvarchar(50)
Open @CurGroup
Fetch Next from @CurGroup Into @myGroupID
print 'Now we are going to check to make sure that there is a group started for every group in the current stage'
while (@@fetch_status = 0)
	Begin
		print 'The current group I am checking for is ' + @myGroupID	
		declare @groupTester as nvarchar(50)
		exec A_SP_WF_GROUP_START_WF_GROUP @wfsID,@myStage,@myGroupID,@strNTLogin
		if @@ERROR <> 0 GOTO problem
		Fetch Next from @CurGroup Into @myGroupID
	End
--Now all the groups have been started and finished as required.
--We need to see if there are anymore Groups that need to be finished in the current stage
print 'Now checking the stage to see if it is actually completed'
declare @stage_tester as nvarchar(50)
SELECT @stage_tester = ID from A_WORKFLOW_GROUP_STARTED 
WHERE  WFS_ID = @wfsID AND WF_STAGE_ID = @myStage and FINISHED is null
if @@ERROR <> 0 goto problem
--If there are no Groups that fit this description then the stage is done
IF @stage_tester is null
	begin
		print 'All Groups are completed'
		--get the next stage number
		declare @myCurrentStageOrderNumber as nvarchar(50)
		SELECT @myCurrentStageOrderNumber = NUM FROM A_WORKFLOW_STAGE_LINK 
			WHERE WF_ID = @myWF_ID and WF_STAGE_ID = @myStage
		print 'My Current Stage OrderNumber is ' + @myCurrentStageOrderNumber
		--now get the next stage
		declare @myNewStage as nvarchar(50)
		SELECT @myNewStage = WF_STAGE_ID FROM A_WORKFLOW_STAGE_LINK 
			WHERE NUM = @myCurrentStageOrderNumber + 1 AND WF_ID = @myWF_ID
		print 'The next stage ID number is ' + isNull(@myNewStage,'NULL')
		if @myNewStage is null
			begin
				print 'Calling the procedure to Finish the WorkFlow'
				exec A_SP_WF_FINISH_WF @wfsID,@strNTLogin
				if @@ERROR <> 0 goto problem
			end
		else
			begin
				UPDATE A_WORKFLOWS_STARTED SET WF_STAGE = @myNewStage WHERE ID = @wfsID
				exec A_SP_WORKFLOW_STARTED_UPDATE @wfsID,@strNtLogin
				if @@ERROR <> 0 goto problem
			end
	end
else
	begin
		print 'All Groups are not completed'
	end

fin:
if @@trancount > 0 COMMIT TRANSACTION
return 0

problem:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_WF_FINISH_GROUP_STARTED and we will terminate and not finish anything '
return 1






