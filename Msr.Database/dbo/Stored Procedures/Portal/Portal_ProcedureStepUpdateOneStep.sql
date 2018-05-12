CREATE          PROCEDURE Portal_ProcedureStepUpdateOneStep
@newID nvarchar(50) OUTPUT,
@messages nvarchar(500) OUTPUT,
@ID nvarchar(50),
@STEP_TEXT nvarchar(4000),
@PROC_OBJ_ID nvarchar(50),
@COMMENTS nvarchar(2000),
@START_ON_COUNTER nvarchar(50),
@COUNTER_VALUE nvarchar(50),
@COUNTER_UNIT nvarchar(50),
@FROM_START_OR_STOP nvarchar(50),
@REL_OR_ABS nvarchar(50),
@SYSTEM_TASK nvarchar(50),
@DESTINATION nvarchar(50),
@SPECIFIC_LOCATION nvarchar(50),
@REFERENCE_VERB nvarchar(50),
@REFERENCE_OBJECT nvarchar(50),
@REFERENCE_THEORIES nvarchar(50),
@GOTO_STEP nvarchar(50),
@GOTO_STEP_ID nvarchar(50),
@CYCLES nvarchar(50),
@CYCLE_ON_COUNTER nvarchar(50),
@CYCLE_COUNT nvarchar(50),
@CYCLE_UNIT nvarchar(50),
@ReferenceProcs varchar(8000),
@precedingSteps varchar(8000),
@DURATION float,
@DURATION_TYPE nvarchar(50),
@strNTLogin nvarchar(50),
@Title nvarchar(max),
@EquipmentTime float = null,
@Roles nvarchar(max)
AS
print 'Starting procedure A_SP_PROCEDURE_STEP_UPDATE_ONE_STEP'
print 'Get the value of the Procedure ID for this Procedure Object ID'
declare @pID as nvarchar(50)
SELECT @pID = ID FROM A_PROCEDURES_HISTORY WHERE OBJECT_ID = @PROC_OBJ_ID
if @ID is null
	begin
		print 'ID is Null do we need to create this step'
		exec sp_GetUniqueID3 @newID OUTPUT
		print 'Got a new ID = ' + @newID
		INSERT INTO A_PROCEDURE_STEPS (ID) VALUES (@newID)
	end
else
	begin
		print 'The ID is not null so we are just updating step ID = ' + @ID
		set @newID = @ID
	end
print 'Now update all the values with the data passed in'
UPDATE A_PROCEDURE_STEPS SET
STEP_TEXT = @STEP_TEXT,
Title = @Title,
COMMENTS = @COMMENTS,
PROCEDURE_ID = @pID,
START_ON_COUNTER = @START_ON_COUNTER,
COUNTER_VALUE = @COUNTER_VALUE,
COUNTER_UNIT = @COUNTER_UNIT,
REL_OR_ABS = @REL_OR_ABS,
FROM_START_OR_STOP = @FROM_START_OR_STOP,
SYSTEM_TASK = @SYSTEM_TASK,
DESTINATION = @DESTINATION,
SPECIFIC_LOCATION = @SPECIFIC_LOCATION,
REFERENCE_VERB = @REFERENCE_VERB,
REFERENCE_OBJECT = @REFERENCE_OBJECT,
GOTO_STEP = @GOTO_STEP,
GOTO_STEP_ID = @GOTO_STEP_ID,
CYCLES = @CYCLES,
CYCLE_ON_COUNTER = @CYCLE_ON_COUNTER ,
CYCLE_COUNT = @CYCLE_COUNT, 
CYCLE_UNIT = @CYCLE_UNIT, 
DURATION = @DURATION,
DURATION_TYPE = @DURATION_TYPE,
DRCM = getDate(),
MODBY = @strNTLogin,
EquipmentTime = @EquipmentTime,
Roles =@Roles
WHERE ID = @newID

print 'Adding links to the reference Theories'
DELETE FROM A_PROCEDURE_STEP_THEORY_LINK WHERE PROC_STEP_ID = @newID
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @REFERENCE_THEORIES,', '
INSERT INTO A_PROCEDURE_STEP_THEORY_LINK(ID,PROC_STEP_ID,THEORY_ID,DRCM,MODBY)
	SELECT newID(),@newID,ltrim(IT),getDate(),@strNTLogin FROM #TempItems

print 'Adding links to the reference Procedures'
DELETE FROM A_PROCEDURE_STEP_PROCEDURE_LINK WHERE PROCEDURE_STEP = @newID
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @ReferenceProcs,', '
INSERT INTO A_PROCEDURE_STEP_PROCEDURE_LINK(ID,PROCEDURE_STEP,PROCEDURE_LINK,DRCM,MODBY)
	SELECT newID(),@newID,ltrim(IT),getDate(),@strNTLogin FROM #TempItems

print 'Adding preceding steps to the reference Procedures'
DELETE FROM A_PROCEDURE_STEP_PRECEDING_STEPS WHERE MY_STEP = @newID
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @precedingSteps,', '
INSERT INTO A_PROCEDURE_STEP_PRECEDING_STEPS (ID,MY_STEP,PREV_STEP,DRCM,MODBY,PROCEDURE_ID)
	SELECT newID(),@newID,ltrim(IT),getDate(),@strNTLogin,@pID FROM #TempItems



exec A_SP_PROCEDURE_SET_PRINT_ORDER @PID,@strNTLogin









