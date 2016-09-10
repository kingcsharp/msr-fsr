


















CREATE                                 PROCEDURE dbo.A_SP_FILL_FILL_WITH_ACTUAL_PART 
@fillID varchar(50),
@fillRootID varchar(50),
@strNTLogin varchar(50)
AS
begin transaction
declare @fillObjID as varchar(50)
declare @mySQL varchar(7500)
if @fillRootID is null 
	begin
	print 'The Fill Root ID is NULL this makes no sense'
	goto problem
	end
else
	print 'The Fill ROOT ID is ' + @fillRootID
SELECT @fillObjID = (SELECT OBJECT_ID FROM A_ACTUAL_PARTS_HISTORY h,A_ACTUAL_PARTS a 
						WHERE h.ID = a.HISTORY_REF_ID AND a.ID = @fillRootID)

Print 'Filling a fill with an actual part'
print 'Fil ID = ' + @fillID
declare @objTable varchar(50),@ID varchar(50),@fillQty int,@partQty int,@objID varchar(50),@sysID varchar(50)
SELECT @objTable = OBJ_TABLE,@ID = OBJ_ID,@objID = OBJ_REF_ID FROM A_V_APPROVED_OBJECTS WHERE ID = @fillRootID
print 'The table is ' + isnull(@objTable,'Null and object Root ID = ' + isNull(@fillRootID,'NULL'))
print 'The ID is = ' + @ID
SELECT @partQty = QTY FROM A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK WHERE HISTORY_REF_ID = @ID
if @@ERROR <> 0 goto problem
declare @myOrderItemID as varchar(50)
SELECT @myOrderItemID = PURCH_ITEM_ID FROM A_V_FILLS_SEARCH WHERE ID = @fillID
print 'My purchase Item ID is ' + @myOrderItemID
exec A_SP_ORDER_ITEM_UPDATE_QTYS @myOrderItemID
if @partQty is null or @partQty = 0
	begin
	print 'The QTY was null or 0 so we are not going to fill'
	goto PROBLEM
	end
SELECT @fillQty = QTY_NEEDS_FILLING,@sysID = SYS_PROC_ID FROM A_V_FILLS_SEARCH WHERE ID = @fillID
if @@ERROR <> 0 goto problem
if @fillQty <=0 
	begin
	print 'The fill QTY = 0 this is not right... '
	SELECT * FROM A_V_FILLS_SEARCH WHERE ID = @fillID		

	goto problem
	end

if @partQty < @fillQty
	begin
	print 'The part is less than the fill so we need to split the fill somehow'
	declare @newFillID varchar(50)
	exec sp_GetUniqueID3 @newFillID OUTPUT
	INSERT INTO A_FILLS (ID,FILL_OBJ_ID,FILL_QTY,FILLER,DRCM,MODBY,FILL_BY,PURCH_ITEM_ID,TASK_ID,SUB_FILL_FOR)
		SELECT @newFillID,FILL_OBJ_ID,FILL_QTY,FILLER,DRCM,MODBY,FILL_BY,PURCH_ITEM_ID,TASK_ID,@fillID FROM
			A_FILLS WHERE ID = @fillID
	set @fillQty = @partQty
	set @fillID = @newFillID
	print ' The new fill ID = ' + @fillID
	end
if @partQty > @fillQty
	begin
	print 'The part is more than the fill so we need to split the part'
	declare @newDist varchar(4000),@newIDList varchar(50)
	set @newDist = convert(varchar(50),(@partQty - @fillQty))  + '_____' + convert(varchar(50),@fillQty)
	if @@ERROR <> 0 goto problem
	DECLARE @newID varchar(50)
	exec A_SP_ACTUAL_PARTS_SPLIT_PART
	@newID OUTPUT,
	@newIDList OUTPUT,
	@fillObjID,
	@newDist ,
	@strNTLogin
	if @@ERROR <> 0 goto problem
	end

print 'We have made the necessary adjustments for this fill item to go into this fill'
print 'Let''s verify though'
declare @partAdjustedQty float
SELECT @partAdjustedQty = QTY FROM A_O_ACTUAL_PARTS_HISTORY WHERE ID = @ID
if @@ERROR <> 0 goto problem
print 'The adjusted parts qty = ' + convert(varchar(50),@partAdjustedQty)
print 'The fill qty = ' + convert(varchar(50),@fillQty)
if @fillQty = @partAdjustedQty
	begin
	print 'The QTY is equal so it is a good fit'
	print 'Inserting this item in as the fill item now' 
	UPDATE A_FILLS SET FILL_OBJ_ID = @fillRootID, FILL_QTY = @partAdjustedQty, 
		FILLER = @strNTLogin,MODBY = @strNTLogin,DRCM = getDate() 
	WHERE ID = @fillID
	UPDATE A_ACTUAL_PARTS_HISTORY SET AP_STATUS = 'ap_in_fill' WHERE OBJECT_ID IN (SELECT ID FROM A_OBJECTS WHERE ROOT = @fillRootID)
	print 'now we should go ahead and fill our child items that are waiting on us.'
	declare @purchItemID varchar(50)
	SELECT @purchItemID = PURCH_ITEM_ID FROM A_FILLS WHERE ID = @fillID
	exec A_SP_ORDER_ITEM_UPDATE_QTYS @purchItemID

	declare @myTaskID varchar(50),@taskPerson varchar(50)
	SELECT @myTaskID = TASK_ID FROM A_FILLS WHERE ID = @fillID
	SELECT @taskPerson = REQUESTEE_ID FROM A_TASKS WHERE ID = @myTaskID
	declare @RET_STATUS varchar(50)
	declare @MSGS varchar(500)
	print 'Going to finish the task now' + isNull(@myTaskID,' Task ID is NULL this is a problem')
	if @myTaskID is not null exec A_SP_TASK_FINISH @myTaskID,@taskPerson
	declare @partID varchar(50),@newPartID varchar(50)
	SELECT @partID = PART_ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @fillRootID
-- 	set @mySQL = 'exec A_SP_PART_MAKE_CROSS_COMPANY_PART_FOR_ACTUAL_PART ' + 
-- 		isNull('''' + @fillRootID + '''','NULL') +',' +
-- 		isNull('''' + @fillID + '''','NULL') +',' +
-- 		isNull('''' + NULL + '''','NULL') +',' +
-- 		isNull('''' + @strNTLogin + '''','NULL')
-- 	exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @mySQL,@strNTLogin
	end
	
--if exists(SELECT ID FROM A_ORDER_ITEMS WHERE PARENT is NULL AND ID = @purchItemID)
--	begin
--	print 'This fill is the root so we are going to execute it'
	set @mySQL =  'exec A_SP_FILL_START_PROCEDURE_IF_POSSIBLE ' 
		+ isnull('''' + @fillID + '''','NULL') + ','
		+ isnull('''' + @strNTLogin + '''','NULL') + ''
	exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @mySQL,@strNTLogin

		
--	end

fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_FILL_FILL_WITH_ACTUAL_PART with no errors'
return 0
PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_FILL_FILL_WITH_ACTUAL_PART and we will terminate and not finish anything '
return 1



















