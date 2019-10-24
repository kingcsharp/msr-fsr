CREATE PROCEDURE [dbo].[A_SP_PROCEDURE_IMPORT_STEP_FROM_EXTERNAL_SOURCE]
	@newID varchar(2000) OUTPUT,
	@msgs varchar(2000)OUTPUT,
	@PROCEDURE_HIST_ID varchar(50),
	@STEP_TEXT varchar(4000),
	@Title nvarchar(2000),
	@PRINT_ORDER varchar(50),
	@REF_DOC_ID varchar(50),
	@COMMENT varchar(2000),
	@STEP_TIME INT,
	@EXTRA_NOTE varchar(2400),
	@DEFAULT_ROLE_ID varchar(50),
	@SERIALIZE varchar(50),
	@SUCCESS_MONITOR varchar(50),
	@INTERNAL_LOCATION varchar(50),
	@LOC_TYPE varchar(50),
	@strNTLogin varchar(50)
AS

declare @rootCo varchar(50),@procObjID varchar(50)

SELECT @procObjID = OBJECT_ID FROM A_PROCEDURES_HISTORY WHERE ID = @PROCEDURE_HIST_ID
SELECT @rootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
declare @sql varchar(4000),@sText varchar(4000)
print '-- Start Importing a procedure Step -- '
set @sText = isNull(@STEP_TEXT,'') + '<<nl/>>' + isNull(@COMMENT + '<<nl/>><<nl/>>','') + isNUll(@EXTRA_NOTE,'')
declare @stepID varchar(50)

exec sp_getUniqueID3 @stepID output

-- ON THE IMPORT THEY ARE PASSING IN THE DEFAULT ROLE ID BY NAME SOMETIMES NOT BY THE ID
-- SO IF IT'S A ROLEN NAME, LOOK UP THE ID

SET @DEFAULT_ROLE_ID = LTRIM(RTRIM(@DEFAULT_ROLE_ID))

IF ISNUMERIC(@DEFAULT_ROLE_ID) = 0
BEGIN
	SELECT TOP 1 @DEFAULT_ROLE_ID = R.ID
	FROM A_V_ROLES_APPROVED_DATA R
	WHERE R.[NAME] = @DEFAULT_ROLE_ID
	ORDER BY R.ID
END

INSERT INTO A_PROCEDURE_STEPS
	(ID,PROCEDURE_ID,STEP_TEXT,PRINT_ORDER,DRCM,MODBY,DURATION,DURATION_TYPE,TITLE, ROLES)
VALUES
	(
	@stepID,
	@PROCEDURE_HIST_ID,
	@sText,
	@PRINT_ORDER,
	getDate(),
	@strNTLogin,
	@STEP_TIME,
	'TIME_SYS_MINUTES',
	@Title,
	@DEFAULT_ROLE_ID
	)

INSERT INTO A_PROCEDURE_OBJECT_LINK
	(ID,PROCEDURE_ID,STEP_ID,APPROVED_OBJECT_ID,QTY,QTY_TYPE,RELATIONSHIP,DRCM,MODBY,LABOR_ROLE)
VALUES
	(
	newID(),
	@PROCEDURE_HIST_ID,
	@stepID,
	@DEFAULT_ROLE_ID,
	@STEP_TIME,
	'TIME_SYS_MINUTES',
	'ROLE_TO_VIEW',
	getDate(),
	@strNTLogin,
	NULL
	)
declare @monID varchar(50)

if @STEP_TEXT LIKE '<<bb>>In Process Inspections<</bb>><<nl/>>%' OR
	@STEP_TEXT LIKE '<<bb>>Final Inspection<</bb>><<nl/>>%' OR
	@STEP_TEXT LIKE '<<bb>>Receiving Inspection<</bb>><<nl/>>%' 
	begin
	exec A_SP_MONITOR_TEMPLATES_UPDATE_TEMPLATE
	@monID OUTPUT, --@newID nvarchar(50) OUTPUT,
	null, --@messages nvarchar(2000) OUTPUT,
	null, --@ID nvarchar(50),
	'YES_NO', --@MONITOR_TYPE  nvarchar(50),
	null, --@INPUT_TYPE  nvarchar(50),
	'Pass inspection?  If not, describe problem.', --@DESCRIPTION nvarchar(2000),
	null, --@START_SYSTEM_TASK nvarchar(50),
	null, --@START_TYPE nvarchar(50),
	null, --@STOP_SYSTEM_TASK nvarchar(50),
	null, --@STOP_TYPE nvarchar(50),
	null, --@COUNTER_OR_CLOCK nvarchar(50),
	null, --@CLOCK_UNIT nvarchar(50),
	null, --@HIGHEST_THRESHOLD nvarchar(50),
	null, --@HIGH_THRESHOLD nvarchar(50),
	null, --@TARGET nvarchar(50),
	null, --@LOW_THRESHOLD nvarchar(50),
	null, --@LOWEST_THRESHOLD nvarchar(50),
	'EQUAL', --@SHOULD_BE nvarchar(50),
	'1', --@OPINION nvarchar(50),
	'0', --@HIDE_TARGET nvarchar(50),
	'0', --@USE_RESULT nvarchar(50),
	null, --@FAIL_STOP nvarchar(50),
	@stepID, --@STEP_ID nvarchar(50),
	'1', --@CORRECT_ANSWER varchar(50),
	null, --@TEXT_TARGET nvarchar(50),
	null, --@TASK_ID varchar(50),
	null, --@TOLERANCE varchar(50),
	@procObjID, --@RELATED_OBJECT_ID nvarchar(50),
	'DONOTCLOSE', --@FAIL_ACTION varchar(50),
	null, --@TARGET_OBJECT_TYPE varchar(50),
	null, --@TARGET_OBJECT varchar(50),
	null, --@SKIP_MODE varchar(50),
	'0', --@CANT_CHANGE tinyint,
	'0', --@ALWAYS_PASS tinyint,
	@strNTLogin --nvarchar(50)

	end

if @STEP_TEXT LIKE '<<bb>>Test<</bb>><<nl/>>%' 
	begin
	exec A_SP_MONITOR_TEMPLATES_UPDATE_TEMPLATE
	@monID OUTPUT, --@newID nvarchar(50) OUTPUT,
	null, --@messages nvarchar(2000) OUTPUT,
	null, --@ID nvarchar(50),
	'YES_NO', --@MONITOR_TYPE  nvarchar(50),
	null, --@INPUT_TYPE  nvarchar(50),
	'All Test Items Pass? Record all test data in the comment.', --@DESCRIPTION nvarchar(2000),
	null, --@START_SYSTEM_TASK nvarchar(50),
	null, --@START_TYPE nvarchar(50),
	null, --@STOP_SYSTEM_TASK nvarchar(50),
	null, --@STOP_TYPE nvarchar(50),
	null, --@COUNTER_OR_CLOCK nvarchar(50),
	null, --@CLOCK_UNIT nvarchar(50),
	null, --@HIGHEST_THRESHOLD nvarchar(50),
	null, --@HIGH_THRESHOLD nvarchar(50),
	null, --@TARGET nvarchar(50),
	null, --@LOW_THRESHOLD nvarchar(50),
	null, --@LOWEST_THRESHOLD nvarchar(50),
	'EQUAL', --@SHOULD_BE nvarchar(50),
	'0', --@OPINION nvarchar(50),
	'0', --@HIDE_TARGET nvarchar(50),
	'0', --@USE_RESULT nvarchar(50),
	null, --@FAIL_STOP nvarchar(50),
	@stepID, --@STEP_ID nvarchar(50),
	'1', --@CORRECT_ANSWER varchar(50),
	null, --@TEXT_TARGET nvarchar(50),
	null, --@TASK_ID varchar(50),
	null, --@TOLERANCE varchar(50),
	@procObjID, --@RELATED_OBJECT_ID nvarchar(50),
	'DONOTCLOSE', --@FAIL_ACTION varchar(50),
	null, --@TARGET_OBJECT_TYPE varchar(50),
	null, --@TARGET_OBJECT varchar(50),
	null, --@SKIP_MODE varchar(50),
	'0', --@CANT_CHANGE tinyint,
	'0', --@ALWAYS_PASS tinyint,
	@strNTLogin --nvarchar(50)
	end

if @STEP_TEXT LIKE '<<bb>>Certify<</bb>><<nl/>>%' 
	begin
	exec A_SP_MONITOR_TEMPLATES_UPDATE_TEMPLATE
	@monID OUTPUT, --@newID nvarchar(50) OUTPUT,
	null, --@messages nvarchar(2000) OUTPUT,
	null, --@ID nvarchar(50),
	'YES_NO', --@MONITOR_TYPE  nvarchar(50),
	null, --@INPUT_TYPE  nvarchar(50),
	'Certified?  If not, describe problem.', --@DESCRIPTION nvarchar(2000),
	null, --@START_SYSTEM_TASK nvarchar(50),
	null, --@START_TYPE nvarchar(50),
	null, --@STOP_SYSTEM_TASK nvarchar(50),
	null, --@STOP_TYPE nvarchar(50),
	null, --@COUNTER_OR_CLOCK nvarchar(50),
	null, --@CLOCK_UNIT nvarchar(50),
	null, --@HIGHEST_THRESHOLD nvarchar(50),
	null, --@HIGH_THRESHOLD nvarchar(50),
	null, --@TARGET nvarchar(50),
	null, --@LOW_THRESHOLD nvarchar(50),
	null, --@LOWEST_THRESHOLD nvarchar(50),
	'EQUAL', --@SHOULD_BE nvarchar(50),
	'1', --@OPINION nvarchar(50),
	'0', --@HIDE_TARGET nvarchar(50),
	'0', --@USE_RESULT nvarchar(50),
	null, --@FAIL_STOP nvarchar(50),
	@stepID, --@STEP_ID nvarchar(50),
	'1', --@CORRECT_ANSWER varchar(50),
	null, --@TEXT_TARGET nvarchar(50),
	null, --@TASK_ID varchar(50),
	null, --@TOLERANCE varchar(50),
	@procObjID, --@RELATED_OBJECT_ID nvarchar(50),
	'DONOTCLOSE', --@FAIL_ACTION varchar(50),
	null, --@TARGET_OBJECT_TYPE varchar(50),
	null, --@TARGET_OBJECT varchar(50),
	null, --@SKIP_MODE varchar(50),
	'0', --@CANT_CHANGE tinyint,
	'0', --@ALWAYS_PASS tinyint,
	@strNTLogin --nvarchar(50)
	end

if @STEP_TEXT like '%Serialize%'
	begin
	if isNull(@SERIALIZE,'') = 'MONITOR'
		exec A_SP_MONITOR_TEMPLATES_UPDATE_TEMPLATE
			@monID OUTPUT, --@newID nvarchar(50) OUTPUT,
			null, --@messages nvarchar(2000) OUTPUT,
			null, --@ID nvarchar(50),
			'TEXT', --@MONITOR_TYPE  nvarchar(50),
			null, --@INPUT_TYPE  nvarchar(50),
			'Record the serial numbers in the comments section of this monitor.', --@DESCRIPTION nvarchar(2000),
			null, --@START_SYSTEM_TASK nvarchar(50),
			null, --@START_TYPE nvarchar(50),
			null, --@STOP_SYSTEM_TASK nvarchar(50),
			null, --@STOP_TYPE nvarchar(50),
			null, --@COUNTER_OR_CLOCK nvarchar(50),
			null, --@CLOCK_UNIT nvarchar(50),
			null, --@HIGHEST_THRESHOLD nvarchar(50),
			null, --@HIGH_THRESHOLD nvarchar(50),
			null, --@TARGET nvarchar(50),
			null, --@LOW_THRESHOLD nvarchar(50),
			null, --@LOWEST_THRESHOLD nvarchar(50),
			'EQUAL', --@SHOULD_BE nvarchar(50),
			'0', --@OPINION nvarchar(50),
			'0', --@HIDE_TARGET nvarchar(50),
			'0', --@USE_RESULT nvarchar(50),
			null, --@FAIL_STOP nvarchar(50),
			@stepID, --@STEP_ID nvarchar(50),
			'0', --@CORRECT_ANSWER varchar(50),
			null, --@TEXT_TARGET nvarchar(50),
			null, --@TASK_ID varchar(50),
			null, --@TOLERANCE varchar(50),
			@procObjID, --@RELATED_OBJECT_ID nvarchar(50),
			'CONTINUE', --@FAIL_ACTION varchar(50),
			null, --@TARGET_OBJECT_TYPE varchar(50),
			null, --@TARGET_OBJECT varchar(50),
			null, --@SKIP_MODE varchar(50),
			'1', --@CANT_CHANGE tinyint,
			'1', --@ALWAYS_PASS tinyint,
			@strNTLogin --nvarchar(50)
	else
		UPDATE A_PROCEDURE_STEPS SET SYSTEM_TASK = 'SYS_SERIALIZE' WHERE ID = @stepID
	end


if isNull(@SUCCESS_MONITOR,'0') = '1'
	begin
	exec A_SP_MONITOR_TEMPLATES_UPDATE_TEMPLATE
	@monID OUTPUT, --@newID nvarchar(50) OUTPUT,
	null, --@messages nvarchar(2000) OUTPUT,
	null, --@ID nvarchar(50),
	'YES_NO', --@MONITOR_TYPE  nvarchar(50),
	null, --@INPUT_TYPE  nvarchar(50),
	'Completed Step Successfully?', --@DESCRIPTION nvarchar(2000),
	null, --@START_SYSTEM_TASK nvarchar(50),
	null, --@START_TYPE nvarchar(50),
	null, --@STOP_SYSTEM_TASK nvarchar(50),
	null, --@STOP_TYPE nvarchar(50),
	null, --@COUNTER_OR_CLOCK nvarchar(50),
	null, --@CLOCK_UNIT nvarchar(50),
	null, --@HIGHEST_THRESHOLD nvarchar(50),
	null, --@HIGH_THRESHOLD nvarchar(50),
	null, --@TARGET nvarchar(50),
	null, --@LOW_THRESHOLD nvarchar(50),
	null, --@LOWEST_THRESHOLD nvarchar(50),
	'EQUAL', --@SHOULD_BE nvarchar(50),
	'1', --@OPINION nvarchar(50),
	'0', --@HIDE_TARGET nvarchar(50),
	'0', --@USE_RESULT nvarchar(50),
	null, --@FAIL_STOP nvarchar(50),
	@stepID, --@STEP_ID nvarchar(50),
	'1', --@CORRECT_ANSWER varchar(50),
	null, --@TEXT_TARGET nvarchar(50),
	null, --@TASK_ID varchar(50),
	null, --@TOLERANCE varchar(50),
	@procObjID, --@RELATED_OBJECT_ID nvarchar(50),
	'DONOTCLOSE', --@FAIL_ACTION varchar(50),
	null, --@TARGET_OBJECT_TYPE varchar(50),
	null, --@TARGET_OBJECT varchar(50),
	null, --@SKIP_MODE varchar(50),
	'0', --@CANT_CHANGE tinyint,
	'0', --@ALWAYS_PASS tinyint,
	@strNTLogin --nvarchar(50)
	end


if @STEP_TEXT like '%Move to Shop%'
	begin
	UPDATE A_PROCEDURE_STEPS SET 
		SYSTEM_TASK = 'SYS_RECEIVE',
		DESTINATION = 'SUPPLIER_ADDRESS'
	WHERE ID = @stepID
	end
if	@STEP_TEXT like '%Move from Shop%'
	begin
	UPDATE A_PROCEDURE_STEPS SET 
		SYSTEM_TASK = 'SYS_SEND',
		DESTINATION = 'ORIG_OBJ_LOCATION'
	WHERE ID = @stepID
	end
if	@STEP_TEXT like '%Receive at Customer%'
	begin
	UPDATE A_PROCEDURE_STEPS SET 
		SYSTEM_TASK = 'SYS_RECEIVE',
		DESTINATION = 'ORIG_OBJ_LOCATION'
	WHERE ID = @stepID
	end


if exists(SELECT ID FROM A_V_THEORY_APPROVED_DATA WHERE NAME LIKE '%' + @REF_DOC_ID + '%')
	begin
	declare @docID varchar(50)
	SELECT top 1 @docID = ID FROM A_V_THEORY_APPROVED_DATA WHERE NAME LIKE '%' + @REF_DOC_ID + '%'
	INSERT INTO A_PROCEDURE_STEP_THEORY_LINK (ID,PROC_STEP_ID,THEORY_ID,DRCM,MODBY)
		VALUES
		(newID(),@stepID,@docID,getDate(),@strNTLogin)
	end

declare @intLocID varchar(50)
SELECT @intLocID = ID FROM A_V_LOCATIONS_APPROVED_DATA WHERE ID = @INTERNAL_LOCATION
if @intLocID is not null
	begin
	UPDATE A_PROCEDURE_STEPS 
	SET 
		SYSTEM_TASK = 'SYS_RECEIVE',
		DESTINATION = 'SPEC_LOCATION',
		SPECIFIC_LOCATION = @intLocID 
	WHERE 
		ID = @stepID

	end
if @LOC_TYPE is not null
	begin
	UPDATE A_PROCEDURE_STEPS 
	SET 
		SYSTEM_TASK = 'SYS_RECEIVE',
		DESTINATION = @LOC_TYPE,
		SPECIFIC_LOCATION = @intLocID 
	WHERE 
		ID = @stepID
	end

fin:

















