








/*
STORED PROCEDURE CALLED IN
MODULE: procedures/viewProcedure.asp
Stored Proc: A_SP_PROCEDURE_DELETE
*/
CREATE           PROCEDURE A_SP_PROCEDURE_STEP_DELETE
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
Declare @postStep nvarchar(50),@procHistID varchar(50)
SELECT @procHistID = PROCEDURE_ID FROM A_PROCEDURE_STEPS WHERE ID = @ID

Declare @postStepsCursor Cursor
set @postStepsCursor = Cursor For SELECT ID FROM A_V_PROCEDURE_STEPS_WITH_PREVIOUS_STEP  WHERE PREV_STEP = @ID
open @postStepsCursor
Fetch Next from @postStepsCursor
Into @postStep
while (@@fetch_status = 0)
Begin
	print 'Updating the post step = ' + @postStep
	INSERT INTO A_PROCEDURE_STEP_PRECEDING_STEPS (ID,MY_STEP,PREV_STEP,DRCM,MODBY,PROCEDURE_ID,OLD_PROCEDURE_ID)
	SELECT newID(),@postStep,PREV_STEP,getDate(),@strNTLogin,PROCEDURE_ID,OLD_PROCEDURE_ID 
		FROM A_PROCEDURE_STEP_PRECEDING_STEPS WHERE MY_STEP = @ID
	Fetch Next from @postStepsCursor Into @postStep
End
close @postStepsCursor
Deallocate @postStepsCursor
print 'Delete My Records in previos steps'
DELETE FROM A_PROCEDURE_STEP_PRECEDING_STEPS WHERE MY_STEP = @ID
print 'Delete My Post step relations'
DELETE FROM A_PROCEDURE_STEP_PRECEDING_STEPS WHERE PREV_STEP = @ID
print 'Prev Step stuff erased.'
print 'now to remove anywhere we are cycled to'
UPDATE A_PROCEDURE_STEPS SET GOTO_STEP_ID = NULL WHERE GOTO_STEP_ID = @ID
print 'now to delete the step itself'
DELETE FROM A_PROCEDURE_STEPS WHERE ID = @ID
print 'set to null where we are refed as the previous step'
UPDATE A_PROCEDURE_STEPS SET OLD_STEP_ID = NULL WHERE OLD_STEP_ID = @ID
print 'DELETE MONITORS'
DELETE FROM A_MONITOR_TEMPLATES WHERE STEP_ID = @ID
print 'Delete The Step'
DELETE FROM A_PROCEDURE_STEPS WHERE ID = @ID
print 'reorder the procedures print order'


exec A_SP_PROCEDURE_SET_PRINT_ORDER @procHistID,@strNTLogin












