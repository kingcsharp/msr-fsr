








CREATE         PROCEDURE dbo.A_SP_TASK_AUTOMATIC_ORDER_UPDATER
@taskID varchar(50),
@strNTLogin varchar(50)
AS
declare @parentTaskID varchar(50), @pSysID varchar(50),@purchID varchar(50),@rootTask varchar(50),
	@SYSTEM_TASK varchar(50)
SELECT @SYSTEM_TASK = SYSTEM_TASK FROM A_TASKS WHERE ID = @taskID
exec A_SP_TASK_GET_CALL_ROOT_TASK_ID @rootTask OUTPUT,@taskID
SELECT @parentTaskID = PARENT_ID FROM A_TASKS WHERE ID = @taskID

print 'System Task = ' + @SYSTEM_TASK
if @SYSTEM_TASK in ('SYS_SHIPPING')
	begin
	print 'This is a shipping task.'
	INSERT INTO A_TASK_ACTION_OBJECT_LINK 
	SELECT newID(),ol.TASK_ID,ol.OBJECT_ID,'ACTION_OBJECT',getDate(),'A_SP_TASK_AUTOMATIC_ORDER_UPDATER',p.QTY
		FROM A_TASK_OBJECT_LINK ol,A_V_ACTUAL_PARTS_APPROVED_DATA p WHERE ol.OBJECT_ID = p.ID AND ol.TASK_ID = @taskID
	end


if isNull(@SYSTEM_TASK,'') = 'SYS_RECEIVE'
	begin
	print 'This is a receive task we need to set the to location from the parent task if we can.'
	if not exists(SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @taskID)
		begin
		print 'Inserting The order info'
		INSERT INTO A_TASK_ORDER_INFORMATION 
		(TASK_ID,MODBY,DRCM)
		SELECT
		@taskID,@strNTLogin,getDate()
		end
	if (SELECT ACTUAL_TO_LOC FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @taskID) is null
		begin
		print 'Updating the order info'
		UPDATE A_TASK_ORDER_INFORMATION SET
			ACTUAL_TO_LOC = (SELECT ACTUAL_TO_LOC FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = (SELECT PARENT_ID FROM A_TASKS WHERE ID = @taskID)),
			MODBY = @strNTLogin,
			DRCM = getDate()
			WHERE TASK_ID = @taskID
		end
	if not exists(SELECT * FROM A_TASK_ACTION_OBJECT_LINK WHERE TASK_ID = @taskID)
		begin
		print 'Inserting the action object'
		INSERT INTO A_TASK_ACTION_OBJECT_LINK (ID,TASK_ID,OBJECT_ID,RELATIONSHIP,DRCM,MODBY,QTY)
			SELECT newID(),@taskID,OBJECT_ID,'ACTION_OBJECT',getDate(),@strNTLogin,QTY 
			FROM A_TASK_ACTION_OBJECT_LINK WHERE
					TASK_ID = (SELECT PARENT_ID FROM A_TASKS WHERE ID = @taskID)
		end
	end













