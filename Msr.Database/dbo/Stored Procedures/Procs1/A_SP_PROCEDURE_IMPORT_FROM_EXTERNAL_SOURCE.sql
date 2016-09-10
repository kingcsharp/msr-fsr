













CREATE              procedure dbo.A_SP_PROCEDURE_IMPORT_FROM_EXTERNAL_SOURCE
@newID varchar(2000) OUTPUT,
@msgs varchar(2000)OUTPUT,
@PROCEDURE_ID varchar(50),
@PROCEDURE_NAME varchar(2000),
@ANS_ID varchar(50),
@PROC_TYPE_ID varchar(50),
@strNTLogin varchar(50)
AS
if not exists(SELECT * FROM A_TT_VERBS WHERE ID = @PROC_TYPE_ID)
	SELECT @PROC_TYPE_ID = v.ID FROM A_TT_VERBS v,A_TT_VERBS_HISTORY h WHERE h.NAME = @PROC_TYPE_ID AND v.HISTORY_REF_ID = h.ID

declare @alreadyExists tinyint
set @alreadyExists = 1
declare @rootCo varchar(50)
SELECT @rootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
declare @sql varchar(4000)
print '-- Start Importing a procedure -- '
declare @intProcRootID varchar(50),@intProcHistID varchar(50),@intProcObjID varchar(50)
exec A_SP_Z_EXT_OBJ_GET_INTERNAL_DATA
@intProcRootID OUTPUT,@intProcHistID OUTPUT,@intProcObjID OUTPUT,
@PROCEDURE_ID,'A_PROCEDURES_HISTORY',@strNTLogin
if @intProcRootID is not null
	begin
	PRINT 'FOUND AN EXTERNAL REFERENCE'
	PRINT 'intProcHistID = ' + @intProcHistID
	set @newID = @intProcHistID
	goto updateProcData
	end
if @ANS_ID is not null
	begin
	SELECT @intProcHistID = HISTORY_REF_ID FROM A_PROCEDURES WHERE ID = @ANS_ID
	SELECT @intProcObjID = OBJECT_ID FROM A_PROCEDURES_HISTORY WHERE ID = @intProcHistID
	if @intProcHistID is not null
		begin
		set @intProcRootID = @ANS_ID
		INSERT INTO A_OBJECT_EXTERNAL_REF 
			(ID,HISTORY_ID,ROOT_OBJ_ID,ITEM_TABLE,EXTERNAL_REF_ID,DRCM,MODBY)
		VALUES
			(newID(),@intProcHistID,@ANS_ID,'A_PROCEDURES_HISTORY',
				@rootCo + '___' + @PROCEDURE_ID,getDAte(),@strNTLogin)
		goto updateProcData
		end
	end
print '-------Creating a new procedure'
set @alreadyExists = 0
if @intProcRootID is null
	begin
--	SELECT @intProcHistID = OBJ_ID,@intProcObjID = ID,@intProcRootID = ROOT FROM A_OBJECTS WHERE ROOT = @PROCEDURE_ID AND STATUS LIKE 'APPROVED%'
	if @intProcHistID is null
		begin
		print 'Creating a new procedure'
		exec A_SP_PROCEDURES_UPDATE_ONE_PROCEDURE
			@intProcObjID OUTPUT,
			null,				--@messages nvarchar(500) OUTPUT,
			null,				--@objID
			@rootCo,			--COMPANY
			@PROC_TYPE_ID,			--VERB
			@PROCEDURE_NAME,		--NAME
			null,				--COMMENTS
			1,				--STEPS IN AP
			0,				--WIP_MSG
			1,				--SECURITY_LEVEL
			null,				--SYS_ID
			0,				--Duration
			'TIME_SYS_MINUTES', 		--Duration Type
			@strNTlogin			--strNTLogin
		set @intProcRootID = @intProcObjID
		if @intProcRootID is null
			begin
			print 'ERRROR -- Tried to make a proc and failed'
			goto problem
			end
		else
			begin
			print 'Made a new procedure with the ID of ' + @intProcRootID

			end
		SELECT @intProcHistID = ID FROM A_PROCEDURES_HISTORY WHERE OBJECT_ID = @intProcObjID
		INSERT INTO A_OBJECT_EXTERNAL_REF 
			(ID,HISTORY_ID,ROOT_OBJ_ID,ITEM_TABLE,EXTERNAL_REF_ID,DRCM,MODBY)
		VALUES
			(newID(),@intProcHistID,@intProcObjID,'A_PROCEDURES_HISTORY',
				@rootCo + '___' + @PROCEDURE_ID,getDAte(),@strNTLogin)
		exec A_SP_OBJECTS_QUICK_APPROVE @intProcObjID,@strNTLogin
		exec A_SP_PROCEDURES_FINISH_WF  @intProcHistID,@intProcObjID,@strNTLogin
		set @newID = @newID + ' Created a new Procedure ID = ' + @intProcObjID
		end
	end

updateProcData:
if @intProcHistID is null
	print 'ERROR 3423423'
else
	UPDATE A_PROCEDURES_HISTORY SET VERB = @PROC_TYPE_ID,NAME = @PROCEDURE_NAME WHERE ID = @intProcHistID

set @newID = @intProcHistID

fin:
 if @alreadyExists = 1
 	begin
	print 'Already Exists'
 	--uncomment the next line to make the system remake procedures which do not have any tasks associated with them.
 	--if exists(SELECT ID FROM A_TASKS WHERE PROCEDURE_ID = @intProcRootID)
 	--	set @newID = 'EXISTS'
 	end	


problem:










