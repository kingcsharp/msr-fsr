


CREATE          PROCEDURE DBO.A_SP_TASK_START_FOLLOWING_STEPS
@ID varchar(50),
@strNTLogin varchar(50)
AS
if @ID is null goto fin
print 'In A_SP_TASK_START_FOLLOWING_STEPS'
print 'This occurs when a task is finished.  Updating following tasks'
declare @procedureID varchar(50),@procedureStepID varchar(50),@parentID varchar(50),@parentStatus varchar(50)
SELECT @procedureID = PROCEDURE_ID,@procedureStepID = PROCEDURE_STEP_ID,@parentID = PARENT_ID FROM A_TASKS WHERE ID = @ID
SELECT @parentStatus = STATUS FROM A_TASKS WHERE ID = @parentID
if @parentStatus in ('REQUESTED','CREATING')
	goto fin
print 'Proc ID = ' + isNull(@procedureID,'NULL')
declare @myRec as int
SELECT @myRec = RECURSION_NUMBER FROM A_TASKS WHERE ID = @ID


Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For 
SELECT ID FROM A_TASKS WHERE  PARENT_ID = @parentID AND RECURSION_NUMBER = @myRec and STATUS = 'PENDING_PARENT_ACCEPTANCE'
--AND
--		PROCEDURE_STEP_ID IN 
--			(SELECT ID FROM A_PROCEDURE_STEPS WHERE
--				 PROCEDURE_ID = 
--				(SELECT HISTORY_REF_ID FROM A_PROCEDURES WHERE ID = @procedureID) 
--			AND	
--				ID IN 
--				(SELECT MY_STEP FROM A_PROCEDURE_STEP_PRECEDING_STEPS WHERE PREV_STEP = @procedureStepID
--				)
--			)
			
declare @myStepID varchar(50),@myProcID varchar(50),@myParentID varchar(50)

open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
	Begin
	print 'Looking at task = ' + @it
	if (SELECT STATUS FROM A_TASKS WHERE ID = @it) in ('PENDING_PARENT_ACCEPTANCE','Qued')
		begin
		SELECT 
			@myProcID = PROCEDURE_ID,
			@myParentID = PARENT_ID,
			@myStepID = PROCEDURE_STEP_ID 
		FROM A_TASKS WHERE ID = @it

		if not Exists(
			SELECT ID,STATUS FROM A_TASKS WHERE RECURSION_NUMBER = @myRec AND PARENT_ID = @myParentID AND STATUS NOT IN ('FINISHED','CLOSED') AND
				PROCEDURE_STEP_ID IN 
				(
					SELECT ID FROM A_PROCEDURE_STEPS WHERE PROCEDURE_ID = 
					(
						SELECT HISTORY_REF_ID FROM A_PROCEDURES WHERE ID = @myProcID
					) 
					AND	ID IN 
					(
						SELECT PREV_STEP FROM A_PROCEDURE_STEP_PRECEDING_STEPS WHERE MY_STEP = @myStepID
					)
				)
			)
			begin
				print 'It is time to execute this step'
				if exists(SELECT * FROM A_TASKS WHERE REQUESTEE_ID is NULL AND GROUP_REQUESTEE_ID IS NULL AND ID = @it)
					begin
					UPDATE A_TASKS SET REQUESTEE_ID = (SELECT REQUESTEE_ID  FROM A_TASKS WHERE ID = @ID) WHERE ID = @it
					exec A_SP_TASK_UPDATE_TASK_ASSIGNEE @it,@strNTLogin
					end
				exec A_SP_TASK_SUBMIT null,null,@it,@strNTLogin
			end
			else
				print 'Not time to execute yet'
		end
	Fetch Next from @curs Into @it
	End
close @curs

if exists(SELECT * FROM A_V_TASK_WITH_ORDER_INFORMATION 
			WHERE ID = @ID AND PURCHASE_ITEM_ROLE = 'TO_SHIP')
	begin
	print 'This was the ship to task so we should start all tasks that are not ship to tasks now'
	set @curs = Cursor For 
		SELECT ID FROM A_V_TASK_WITH_ORDER_INFORMATION 
			WHERE PARENT_ID = (SELECT PARENT_ID FROM A_TASKS WHERE ID = @ID)
				 AND PURCHASE_ITEM_ROLE = 'CHILD_PROC' 
					AND TASK_STATUS = 'PENDING_PARENT_ACCEPTANCE'
	open @curs
		Fetch Next from @curs Into @it
		while (@@fetch_status = 0)
			Begin
			print 'Found a task with number = ' + @it
			exec A_SP_TASK_SUBMIT null,null,@it,@strNTLogin
			Fetch Next from @curs Into @it
		End
	close @curs
	end


if exists(SELECT * FROM A_V_TASK_WITH_ORDER_INFORMATION 
			WHERE ID = @ID AND PURCHASE_ITEM_ROLE = 'CHILD_PROC')
	begin
	print 'This was the Procedure task so we should start the ship from task'
	set @curs = Cursor For 
		SELECT ID FROM A_V_TASK_WITH_ORDER_INFORMATION 
			WHERE PARENT_ID = (SELECT PARENT_ID FROM A_TASKS WHERE ID = @ID)
				 AND PURCHASE_ITEM_ROLE = 'FROM_SHIP' 
					AND TASK_STATUS = 'PENDING_PARENT_ACCEPTANCE'
	open @curs
		Fetch Next from @curs Into @it
		while (@@fetch_status = 0)
			Begin
			print 'Found a task with number = ' + @it
			exec A_SP_TASK_SUBMIT null,null,@it,@strNTLogin
			Fetch Next from @curs Into @it
		End
	close @curs
	end






Deallocate @curs

fin:
print 'Out Of A_SP_TASK_START_FOLLOWING_STEPS'









