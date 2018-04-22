CREATE             procedure [dbo].[A_SP_PROCEDURE_COPY_ONE]
	@newObjID nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@copyPrefix nvarchar(50),
	@strNTLogin nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one
INSERT INTO A_PROCEDURES_HISTORY (ID,VERB,NAME,COMMENTS,SECURITY_LEVEL,STEPS_IN_AP,WIP_MSG,DRCM,MODBY,SYSTEM_ID,DURATION,DURATION_TYPE,IsActive)
SELECT @newID as ID,VERB,NAME,COMMENTS,SECURITY_LEVEL,STEPS_IN_AP,WIP_MSG,getDate(),@strNTLogin,SYSTEM_ID,DURATION,DURATION_TYPE,IsActive
FROM A_PROCEDURES_HISTORY WHERE ID = @strID
--if it is a copy then change the name
if len(@copyPrefix) > 0
	begin
	UPDATE A_PROCEDURES_HISTORY SET
	NAME = @copyPrefix + NAME
	WHERE
	ID = @newID
	end
--find out what object id the new one got
print 'Copied the procedure'
SELECT @newObjID = OBJECT_ID FROM A_PROCEDURES_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID
print 'Copying the Monitors'
exec A_SP_PROCEDURE_COPY_MONITORS @strID,null,@newID,null,@strNTLogin
--now we need to copy the object links
print 'Copying the object Links'
exec A_SP_PROCEDURE_COPY_OBJECT_LINKS @strID,null,@newID,null,@strNTLogin


--now we need to copy all the steps
Declare @Step nvarchar(50),@newStepID varchar(50)
Declare @StepCursor Cursor
set @StepCursor = Cursor For SELECT ID FROM A_PROCEDURE_STEPS  WHERE PROCEDURE_ID = @strID
open @StepCursor
Fetch Next from @StepCursor
Into @Step
while (@@fetch_status = 0)
Begin
	print 'Copying the step = ' + @Step
	exec A_SP_PROCEDURE_STEP_COPY @newStepID output,@Step,@strID,@newID,@strNTLogin
	Fetch Next from @StepCursor Into @Step
End
close @StepCursor
Deallocate @StepCursor

print 'doing all the preceding steps'
print 'SELECT newID(),MY_STEP,PREV_STEP,getDate(),''' + @strNTLogin + ''',PROCEDURE_ID 
FROM A_V_PROCEDURE_STEPS_PREV_STEP_BY_OLD_STEP_RELATIONSHIP
WHERE PROCEDURE_ID = ''' + @newID + ''''
--Copy the preceding steps
INSERT INTO A_PROCEDURE_STEP_PRECEDING_STEPS(
[ID],[MY_STEP],[PREV_STEP],[DRCM],[MODBY],[PROCEDURE_ID])
SELECT newID(),MY_STEP,PREV_STEP,getDate(),@strNTLogin,PROCEDURE_ID 
FROM A_V_PROCEDURE_STEPS_PREV_STEP_BY_OLD_STEP_RELATIONSHIP
WHERE PROCEDURE_ID = @newID

declare @pCursor as cursor
declare @myPID as varchar(50)
set @pCursor = Cursor For SELECT GOTO_STEP_ID FROM A_PROCEDURE_STEPS WHERE PROCEDURE_ID = @newID
open @pCursor
Fetch Next from @pCursor
Into @myPID
while (@@fetch_status = 0)
Begin
	print 'fixing Got Step for Old ID = ' + @myPID
	UPDATE A_PROCEDURE_STEPS
	SET GOTO_STEP_ID = (SELECT ID FROM A_PROCEDURE_STEPS p WHERE p.OLD_STEP_ID = @myPID AND PROCEDURE_ID = @newID)
	WHERE GOTO_STEP_ID = @myPID
	Fetch Next from @pCursor Into @myPID
End
close @pCursor
Deallocate @pCursor



print 'We need to copy all the comments too'

INSERT INTO A_PROCEDURE_COMMENTS (ID,DATA,LOC,PROC_HIST_ID,DRCM,MODBY,PRINT_ORDER)
SELECT newID(),DATA,LOC,@newID,getDate(),@strNTLogin,PRINT_ORDER FROM A_PROCEDURE_COMMENTS
WHERE PROC_HIST_ID = @strID











