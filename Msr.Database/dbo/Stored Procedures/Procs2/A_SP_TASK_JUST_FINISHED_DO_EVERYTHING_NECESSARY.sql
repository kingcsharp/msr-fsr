











CREATE                                             procedure dbo.A_SP_TASK_JUST_FINISHED_DO_EVERYTHING_NECESSARY
@taskID varchar(50),
@strNTLogin varchar(50)
AS
print 'IN A_SP_TASK_JUST_FINISHED_DO_EVERYTHING_NECESSARY'
declare 
	@sysID varchar(50),@it nvarchar(50),@curs Cursor,
 	@customerCo varchar(50),
	@toLoc varchar(50),@origReq varchar(50),@parentID varchar(50),
	@parentSys varchar(50),@isQA tinyint,@isQ tinyint,
	@procID varchar(50),@stepID varchar(50),@purchHistID varchar(50),
	@purchItemID varchar(50),@ACTUAL_STOP_DATE datetime


declare @myStopDate datetime
SELECT @myStopDate = THE_DATE FROM A_TASK_DATES WHERE TASK_ID = @taskID AND DATE_TYPE = 'ACTUAL_STOP'
if @myStopDate is null
	begin
	exec A_SP_TASK_DATES_UPDATE @taskID,'ACTUAL_STOP',@myStopDate,@strNTLogin
	end

declare @childTaskID as varchar(50),@batchSQL varchar(2000)
set @curs = CURSOR FOR SELECT CHILD_TASK_ID FROM A_V_BATCH_TASK_CHILDREN WHERE BATCH_TASK_ID = @taskID
open @curs
fetch next from @curs into @childTaskID
while @@fetch_status = 0
	begin
	set @batchSQL = 'exec A_SP_TASK_FINISH ''' + @childTaskID + ''',''' + @strNTLogin + ''''
	exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @batchSQL,@strNTLogin
	fetch next from @curs into @childTaskID
	end
close @curs

declare @isBatch tinyint,@fillObjID varchar(50)
SELECT     @isBatch = f.BATCH_FILL,@fillObjID = f.FILL_OBJ_ID
FROM         A_TASK_ORDER_INFORMATION toi INNER JOIN
             A_FILLS f ON toi.FILL_ITEM_ID = f.ID
WHERE     (toi.TASK_ID = @taskID)
if @isBatch = 1
	begin
	UPDATE A_ACTUAL_PARTS_HISTORY SET PARENT_ID = null WHERE PARENT_ID = @fillObjID
	end


SELECT @origReq = REQUESTOR,@sysID = SYSTEM_TASK,
	@isQA = IS_QUOTE_ACCEPT, @isQ = IS_QUOTE,
	@procID = PROCEDURE_ID,@stepID = PROCEDURE_STEP_ID,
	@ACTUAL_STOP_DATE = isNull(ACTUAL_STOP_DATE,getDate()),
	@parentID = PARENT_ID
	FROM A_TASKS WHERE ID = @taskID
--set the actual stop date
UPDATE A_TASKS SET ACTUAL_STOP_DATE = @ACTUAL_STOP_DATE WHERE ID = @taskID

print 'Since we just finished a task we should always check to see if the parent task is a purchase'
SELECT @parentSys = SYSTEM_TASK FROM A_TASKS WHERE ID = @parentID

declare @FILL_ITEM_ID varchar(50)
--if its a purchase we need to do some things
SELECT 	@purchItemID = PURCHASE_ITEM_ID, 
		@purchHistID = PURCHASE_HIST_ID,
		@FILL_ITEM_ID = FILL_ITEM_ID
		FROM A_TASK_ORDER_INFORMATION 
		WHERE TASK_ID = @taskID

--if it is a Quote accept then just move it onto finished
if @isQA = 1
	exec A_SP_TASK_CLOSE null,null,@taskID,@origReq
if @isQ = 1
	exec A_SP_TASK_CLOSE null,null,@taskID,@origReq

--Shipping tasks require some extra work
if @sysID = 'SYS_SHIPPING'
 	begin
-- 	print 'This is a shipping task so we are going to change the location of the reference object.'
-- 	select @toLoc = ACTUAL_TO_LOC FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @taskID
-- 	print 'making a cursor to go through all the objects we are shipping'
-- 	set @curs = Cursor For SELECT OBJECT_ID FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @taskID
-- 	open @curs
-- 	Fetch Next from @curs Into @it
-- 	while (@@fetch_status = 0)
-- 		Begin
-- 		print 'shipping Object = ' + @it
-- 		UPDATE A_ACTUAL_PARTS_HISTORY SET LOCATION = @toLoc, AP_STATUS = 'ap_available'
-- 			WHERE OBJECT_ID in (SELECT ID FROM A_OBJECTS WHERE ROOT = @it)
-- 		Fetch Next from @curs Into @it
-- 		End
-- 	close @curs
-- 	Deallocate @curs
 	exec A_SP_TASK_CLOSE null,null,@taskID,@origReq
	
	print 'Since this was a shipping task we need to check if the parent is a provide and stay and mark it finished too'
	SELECT @parentID = PARENT_ID FROM A_TASKS WHERE ID = @taskID
	SELECT @parentSys = SYSTEM_TASK FROM A_TASKS WHERE ID = @parentID
	if @parentSys = 'SYS_PROVIDE_AND_STAY'
		exec A_SP_TASK_FINISH @parentID,@strNTLogin
	end
--SEnd tasks require some work
if @sysID = 'SYS_SEND'
	begin
	print 'This is a send task so we are going to change the location of the action objects to NULL.'
	declare @sendQty float,@sendPartQty float,
		@sendPartID varchar(50),@curSendObjectID varchar(50),@strSendDists varchar(50)
	set @curs = Cursor For SELECT OBJECT_ID,QTY FROM A_TASK_ACTION_OBJECT_LINK 
		WHERE TASK_ID = @taskID AND RELATIONSHIP = 'ACTION_OBJECT'
	open @curs
	Fetch Next from @curs Into @it,@sendQty
	while (@@fetch_status = 0)
		Begin
		print 'object ID to send = ' + @it
		SELECT @sendPartQty = QTY FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @it
		if @sendPartQty >= @sendQty
			begin
			print ' The qtys are good so continueing'
			if @sendPartQty <> @sendQty
				begin
				set @strSendDists = convert(varchar(50),@sendQty) + '_____' + convert(varchar(50),@sendPartQty - @sendQty)
				SELECT @curSendObjectID = OBJ_REF_ID FROM A_APPROVED_OBJECTS WHERE ID = @it
				exec A_SP_ACTUAL_PARTS_SPLIT_PART
					@sendPartID OUTPUT,null,@curSendObjectID, 					@strSendDists,@strNTLogin
				end
			else
				set @sendPartID = @it
			UPDATE A_ACTUAL_PARTS_HISTORY SET LOCATION = @toLoc
				WHERE OBJECT_ID in (SELECT ID FROM A_OBJECTS WHERE ROOT = @it)
			exec A_SP_ACTUAL_PART_ADD_TO_CALL @sendPartID,@taskID,'SENT',@strNTLogin
			end
		Fetch Next from @curs Into @it,@sendQty
		End
	close @curs
	Deallocate @curs
	exec A_SP_TASK_CLOSE null,null,@taskID,@origReq
	end

if @sysID = 'SYS_RECEIVE'
	begin
	print 'This is a receive task so we are going to change the location of the action objects to the destination.'
	select @toLoc = ACTUAL_TO_LOC FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @taskID
	declare @recQty float,@recPartQty float,
		@recPartID varchar(50),@curRecObjectID varchar(50),@strRecDists varchar(50)
	set @curs = Cursor For SELECT OBJECT_ID,QTY FROM A_TASK_ACTION_OBJECT_LINK 
		WHERE TASK_ID = @taskID AND RELATIONSHIP = 'ACTION_OBJECT'
	open @curs
	Fetch Next from @curs Into @it,@recQty
	while (@@fetch_status = 0)
		Begin
		print 'object ID to receive = ' + @it
		SELECT @recPartQty = QTY FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @it
		if @recPartQty >= @recQty
			begin
			print ' The qtys are good so continueing'
			if @recPartQty <> @recQty
				begin
				set @strRecDists = convert(varchar(50),@recQty) + '_____' + convert(varchar(50),@recPartQty - @recQty)
				SELECT @curRecObjectID = OBJ_REF_ID FROM A_APPROVED_OBJECTS WHERE ID = @it
				exec A_SP_ACTUAL_PARTS_SPLIT_PART
					@recPartID OUTPUT,null,@curRecObjectID,
					@strRecDists,@strNTLogin
				end
			else
				set @recPartID = @it
			UPDATE A_ACTUAL_PARTS_HISTORY SET LOCATION = @toLoc
				WHERE OBJECT_ID in (SELECT ID FROM A_OBJECTS WHERE ROOT = @it)
			exec A_SP_ACTUAL_PART_ADD_TO_CALL @recPartID,@taskID,'RECEIVED',@strNTLogin
			end
		Fetch Next from @curs Into @it,@recQty
		End
	close @curs
	Deallocate @curs
	exec A_SP_TASK_CLOSE null,null,@taskID,@origReq
	end

if @sysID = 'SYS_PROVIDE_AND_STAY'
	begin
	print 'This is a provide and stay task so we need to change the owner based on the info in the quote.'
	SELECT @customerCo = CUSTOMER_CO FROM A_V_TASK_WITH_ORDER_INFORMATION WHERE TASK_ID = @taskID
	print 'making a cursor to go through all the objects we are changing ownership of'
	set @curs = Cursor For SELECT OBJECT_ID FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @taskID
	open @curs
	Fetch Next from @curs Into @it
	while (@@fetch_status = 0)
		Begin
		print 'shipping Object = ' + @it
		UPDATE A_ACTUAL_PARTS_HISTORY SET CUR_OWNER = @customerCo
			WHERE OBJECT_ID in (SELECT ID FROM A_OBJECTS WHERE ROOT = @it)
		declare @custRootCo varchar(50)
		SELECT @custRootCo = TOP_COMPANY FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @customerCo 
		print '$$$$$$$$ exec A_SP_ACTUAL_PART_CHANGE_PART_ID ''' + @it + ''',''' + @custRootCo + ''''
		exec A_SP_ACTUAL_PART_CHANGE_PART_ID @it,@custRootCo
		exec A_SP_ACTUAL_PART_ADD_TO_CALL @it,@taskID,'SYS_PROVIDE_AND_STAY',@strNTLogin
		Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs
	exec A_SP_TASK_CLOSE null,null,@taskID,@origReq
	end

if @sysID = 'SYS_PROVIDE_AND_CONSUMED'
	begin
	print 'This is a provide and Consume task so we need to change the owner based on the info in the quote.'
	SELECT @customerCo = CUSTOMER_CO FROM A_V_TASK_WITH_ORDER_INFORMATION WHERE TASK_ID = @taskID
	print 'making a cursor to go through all the objects we are changing ownership of'
	set @curs = Cursor For SELECT OBJECT_ID FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @taskID
	open @curs
	Fetch Next from @curs Into @it
	while (@@fetch_status = 0)
		Begin
		print 'shipping Object = ' + @it
		UPDATE A_ACTUAL_PARTS_HISTORY SET CUR_OWNER = @customerCo
			WHERE OBJECT_ID in (SELECT ID FROM A_OBJECTS WHERE ROOT = @it)
		exec A_SP_ACTUAL_PART_ADD_TO_CALL @it,@taskID,'SYS_PROVIDE_AND_CONSUMED',@strNTLogin
		Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs
	exec A_SP_TASK_CLOSE null,null,@taskID,@origReq
	end

if @sysID = 'SYS_REMOVE'
	begin
	declare @objParentLocation varchar(50),@removeQty float,@partQty float,
		@remPartID varchar(50),@curObjectID varchar(50),@strDists varchar(50)
	print 'This is a remove Task' 	set @curs = Cursor For SELECT OBJECT_ID,QTY FROM A_TASK_ACTION_OBJECT_LINK 
		WHERE TASK_ID = @taskID AND RELATIONSHIP = 'ACTION_OBJECT'
	open @curs
	Fetch Next from @curs Into @it,@removeQty
	while (@@fetch_status = 0)
		Begin
		print 'object ID to remove = ' + @it
		print 'Find location of this objects root parent object'
		exec A_SP_ACTUAL_PART_GET_ROOT_PARENT_LOCATION @objParentLocation output,@it
		SELECT @partQty = QTY FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @it
		if @partQty >= @removeQty
			begin
			if @partQty <> @removeQty
				begin
					set @strDists = convert(varchar(50),@removeQty) + '_____' + convert(varchar(50),@partQty - @removeQty)
					SELECT @curObjectID = OBJ_REF_ID FROM A_APPROVED_OBJECTS WHERE ID = @it
					exec A_SP_ACTUAL_PARTS_SPLIT_PART
					@remPartID OUTPUT,null,@curObjectID,
					@strDists,@strNTLogin
				end
			else
				set @remPartID = @it

			UPDATE A_ACTUAL_PARTS_HISTORY SET PARENT_ID = NULL, AP_STATUS = 'AP_IN_CALL', LOCATION=@objParentLocation
				WHERE OBJECT_ID in (SELECT ID FROM A_OBJECTS WHERE ROOT = @remPartID)
			exec A_SP_ACTUAL_PART_ADD_TO_CALL @remPartID,@taskID,'REMOVED',@strNTLogin
			end
		Fetch Next from @curs Into @it,@removeQty
		End
	close @curs
	Deallocate @curs
	exec A_SP_TASK_CLOSE null,null,@taskID,@origReq
	end

if @sysID = 'SYS_INSTALL'
	begin
	print 'This is an install Task'
	declare @parentPartID varchar(50),@installQty float,@inPartQty float,
		@installPartID varchar(50),@curInObjectID varchar(50),@strInDists varchar(50)
	SELECT @parentPartID = OBJECT_ID FROM A_TASK_ACTION_OBJECT_LINK 
		WHERE TASK_ID = @taskID AND RELATIONSHIP = 'INSTALL_OBJECT'
	set @curs = Cursor For SELECT OBJECT_ID,QTY FROM A_TASK_ACTION_OBJECT_LINK 
		WHERE TASK_ID = @taskID AND RELATIONSHIP = 'ACTION_OBJECT'
	open @curs
	Fetch Next from @curs Into @it,@installQty
	while (@@fetch_status = 0)
		Begin
		print 'object ID to install = ' + @it
		SELECT @inPartQty = QTY FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @it
		print 'inPartQty = '
		print @installQty
		if @inPartQty >= @installQty
			begin
			print ' The qtys are good so continueing'
			if @inPartQty <> @installQty
				begin
				set @strInDists = convert(varchar(50),@installQty) + '_____' + convert(varchar(50),@inPartQty - @installQty)
				SELECT @curInObjectID = OBJ_REF_ID FROM A_APPROVED_OBJECTS WHERE ID = @it
				exec A_SP_ACTUAL_PARTS_SPLIT_PART
					@installPartID OUTPUT,null,@curInObjectID,
					@strInDists,@strNTLogin
				end
			else
				set @installPartID = @it
			exec A_SP_ACTUAL_PART_ADD_TO_CALL @installPartID,@taskID,'INSTALLED',@strNTLogin
			UPDATE A_ACTUAL_PARTS_HISTORY SET AP_STATUS = 'ap_installed',PARENT_ID = @parentPartID
				WHERE OBJECT_ID in (SELECT ID FROM A_OBJECTS WHERE ROOT = @it)
			end
		Fetch Next from @curs Into @it,@installQty
		End
	close @curs
	Deallocate @curs
	exec A_SP_TASK_CLOSE null,null,@taskID,@origReq
	end

if @sysID = 'SYS_CONSUME'
	begin
	print 'This is a consume Task'
	declare @conQty float,@conPartQty float,
		@conPartID varchar(50),@curConObjectID varchar(50),@strConDists varchar(50)
	set @curs = Cursor For SELECT OBJECT_ID,QTY FROM A_TASK_ACTION_OBJECT_LINK 
		WHERE TASK_ID = @taskID AND RELATIONSHIP = 'ACTION_OBJECT'
	open @curs
	Fetch Next from @curs Into @it,@conQty
	while (@@fetch_status = 0)
		Begin
		print 'object ID to consume = ' + @it
		SELECT @conPartQty = QTY FROM A_V_ACTUAL_PARTS_APPROVED_DATA WHERE ID = @it
		if @conPartQty >= @conQty
			begin
			print ' The qtys are good so continueing'
			if @conPartQty <> @conQty
				begin
				set @strConDists = convert(varchar(50),@conQty) + '_____' + convert(varchar(50),@conPartQty - @conQty)
				SELECT @curConObjectID = OBJ_REF_ID FROM A_APPROVED_OBJECTS WHERE ID = @it
				exec A_SP_ACTUAL_PARTS_SPLIT_PART
					@conPartID OUTPUT,null,@curConObjectID,
					@strConDists,@strNTLogin
				end
			else
				set @conPartID = @it
			UPDATE A_ACTUAL_PARTS_HISTORY SET AP_STATUS = 'ap_consumed'
				WHERE OBJECT_ID in (SELECT ID FROM A_OBJECTS WHERE ROOT = @it)
			exec A_SP_ACTUAL_PART_ADD_TO_CALL @conPartID,@taskID,'CONSUMED',@strNTLogin
			end
		Fetch Next from @curs Into @it,@conQty
		End
	close @curs
	Deallocate @curs
	exec A_SP_TASK_CLOSE null,null,@taskID,@origReq
	end


if @sysID = 'SYS_PURCHASE'
	begin
	print 'This is a purchase.'
	UPDATE A_ACTUAL_PARTS_HISTORY SET AP_STATUS = 'ap_available' 
		WHERE ID IN (SELECT HISTORY_REF_ID FROM A_ACTUAL_PARTS 
			WHERE ID IN (SELECT ACTUAL_PART_ID FROM A_ACTUAL_PARTS_CALL_DATA WHERE ROOT_TASK = @taskID))
	exec A_SP_TASK_CLOSE null,null,@taskID,@origReq
	end


if @sysID = 'SYS-FILL'
	begin
	print 'This is a fill task. We should close it automatically'
	exec A_SP_TASK_CLOSE null,null,@taskID,@origReq
	end



if @sysID is null
	begin
	print 'This is not a system task'
	if @procID is not null or @stepID is not null
		if not(isNull(@parentSys,'') = 'SYS_PURCHASE')
			exec A_SP_TASK_CLOSE null,null,@taskID,@origReq
	end


if @parentSys = 'SYS_PURCHASE'
	begin
	if not exists (SELECT * FROM A_TASKS WHERE PARENT_ID = @parentID and STATUS NOT IN ('FINISHED','CLOSED'))
		begin
		print 'The parent is a purchse and it has no children that are not completed.  Go ahead and mark it finished too'
		exec A_SP_TASK_FINISH @parentID,@strNTLogin
		end
	end

print 'We need to check to see if this is a purchase Item and if it is then we need to add the exCost
 children to the accounts that were speced to pay for them.'


declare @acctID varchar(50)
declare @sql varchar(4000)

if @FILL_ITEM_ID IS NOT NULL
	begin
	set @sql = 'exec A_SP_PURCHASE_ITEM_INVOICE_ITEM ''' + @purchItemID + ''',''' + @FILL_ITEM_ID + ''',''' + @strNTLogin + ''''
	exec(@sql)
--	exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @sql,@strNTLogin
	end
--I dont think this is necessary.  May need to add an update all tasks for purchase here though
-- SELECT @purchHistID = PURCHASE_HIST_ID FROM A_V_TASK_WITH_ORDER_INFORMATION WHERE ID = @taskID
-- if @purchHistID is not null
-- 	begin
-- 	print 'This is a purchased task so we need to check all the other items on this purchase to make sure they are started if they need to be.'
-- 	set @curs = Cursor For SELECT TASK_ID FROM A_V_TASK_WITH_ORDER_INFORMATION WHERE PURCHASE_HIST_ID = @purchHistID
-- 	open @curs
-- 	Fetch Next from @curs Into @it
-- 	while (@@fetch_status = 0)
-- 		Begin
-- 		print 'Checking task = ' + @it
-- 		--exec A_SP_TASK_REQUEST_PURCHASE_TASK_IF_IT_IS_TIME @it
-- 		Fetch Next from @curs Into @it
-- 		End
-- 	close @curs
-- 	Deallocate @curs
-- 	end

exec A_SP_TASK_CLOSE_FOLLOWING_STEPS_SPECED_IN_MONITORS @taskID,@strNTLogin
exec A_SP_TASK_END_PROCEDURE_SPECED_IN_MONITORS @taskID,@strNTLogin
exec A_SP_TASK_START_FOLLOWING_STEPS @taskID,@strNTLogin
exec A_SP_TASK_CHECK_RECURSION_STATUS @taskID


declare @parentSysTask varchar(50)
if @parentID is not null 
	begin
	SELECT @parentSysTask = isNull(SYSTEM_TASK,'') FROM A_TASKS WHERE ID = @parentID
	if @parentSysTask = 'SYS_DNR'
		UPDATE A_TASKS SET DESCRIPTION = DESCRIPTION,Title = Title WHERE ID = @parentID
	else
		begin
		--UPDATE A_TASKS SET DESCRIPTION = DESCRIPTION + isNull(@parentSysTask,' NULL ') WHERE ID = @parentID
		print 'This one has a parent.  Should we close the parent?'
		if not exists(SELECT * FROM A_TASKS WHERE PARENT_ID = @parentID AND STATUS NOT IN ('FINISHED','CLOSED'))
			begin
			print 'There are no children tasks open'
			UPDATE A_TASKS SET STATUS = 'FINISHED', ACTUAL_STOP_DATE = getDate()  WHERE ID = @parentID
			exec A_SP_TASK_JUST_FINISHED_DO_EVERYTHING_NECESSARY @parentID,@strNTlogin
			end
		else
			begin
			print 'This one has a parent.  Should we set it to Accepted and Qued?'
			if not exists(SELECT * FROM A_TASKS WHERE PARENT_ID = @parentID AND STATUS NOT IN ('FINISHED','CLOSED','Qued'))
				begin
				print 'There are no children tasks open'
				UPDATE A_TASKS SET STATUS = 'AccQued' WHERE ID = @parentID
				end
			end
		end
	end

exec A_SP_TASK_POPULATE_LINKED_ACTUAL_PARTS_WHEN_FINISHED @taskID

print 'If it is a step we should close it'
declare @curStat varchar(50)
SELECT @curStat = STATUS FROM A_TASKS WHERE ID = @taskID
if @stepID is not null and @curStat = 'FINISHED'
	begin
		print 'it is a step'
		exec A_SP_TASK_CLOSE null,null,@taskID,@origReq
	end
else
	begin
	print 'it is not a step'
	print 'Stat = ' + @curStat
	end
	

print 'Out of A_SP_TASK_JUST_FINISHED_DO_EVERYTHING_NECESSARY'










