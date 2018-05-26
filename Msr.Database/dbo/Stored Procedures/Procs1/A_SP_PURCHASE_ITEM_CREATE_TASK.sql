



CREATE                        PROCEDURE dbo.A_SP_PURCHASE_ITEM_CREATE_TASK
@purchItemID varchar(50),
@parentTaskID varchaR(50),
@strNTLogin varchar(50)
AS
declare @mySQL varchar(4000),@pItemID nvarchar(50), @curs2 Cursor,
@newID2 varchar(50),@messages2 varchar(2000),@curs Cursor,
@purchObjID varchar(50),@COMMENTS varchar(4000),@it nvarchar(50),
@requesteeRole varchar(50),
@taskName nvarchar(2000),@systemID varchar(50),
@procedureName varchar(2000),@procedureID varchar(50),
@fillObj varchar(50),@newTaskID nvarchar(50),@messages nvarchar(2000),
@taskComment nvarchar(2000),@supplierID varchar(50),@SUBMIT_STATUS varchar(50),
@prodHistID varchar(50),@mgrRole varchar(50),@requesteeID varchar(50),
@purchaser varchar(50),@purchHistID varchar(50),@stepInAP varchar(10),
@dest as varchar(50),@fromLoc varchar(50),@toLoc varchar(50),
@procHistID varchar(50),@objProdAppliesTo varchar(50),
@supHistID as varchar(50),@custPers varchar(50),@newTaskGroupName varchar(2000),
@parentOrderRole as varchar(50),@testTaskRole as varchar(50),@myRole varchar(50),@taskProc varchar(50),
@fillID varchar(50),@parentFillID varchar(50),@leadTime float,@leadTimeUnits varchar(50),@pplID varchar(50),
@pplHistID varchar(50),@origStartDate datetime,@origStopDate datetime,@curTime dateTime,@parentFillObj varchar(50),
@acctSupplierID varchar(50)


SELECT @curTime = dbo.A_FN_DATE_TIME_GET_LOCAL_TIME_FOR_PERSON(@strNtLogin,getDate())
--Check to see if the parent is the grouper this will not matter for shipping but will if it is not a hipping task
SELECT @parentFillID = FILL_ITEM_ID,@parentOrderRole = PURCHASE_ITEM_ROLE FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @parentTaskID
SELECT @parentFillObj = OBJECT_ID FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @parentTaskID
print '%%%%%%%%%%%%%%% Creating Tasks for Purchase ITem ID = ' + @purchItemID
set @curs = Cursor For 
	SELECT ID,PROD_PRICE_LIST,
		PROD_NAME,DEST,FROM_LOC,TO_LOC,
		PROD_HIST_ID,PURCHASE_HIST_ID,PURCHASER_ID,
		STEPS_IN_AP,CUSTOMER_PERSON,SUPPLIER,
		PROC_NAME,SYS_PROC_ID,PROC_ID,FILL_OBJ_ID,
		PURCH_ITEM_ID,'Purchase Item = ' + @purchItemID,
		PROCEDURE_HIST_ID
	FROM A_V_FILLS_SEARCH 
	WHERE PURCH_ITEM_ID = @purchItemID AND (@parentFillObj is null or @parentFillObj = FILL_OBJ_ID)

open @curs
Fetch Next from @curs Into
		@fillID,@pplID,
		@taskName,@dest,@fromLoc,@toLoc,
		@prodHistID,@purchHistID,@purchaser,
		@stepInAP,@custPers,@supplierID,
		@procedureName,@systemID,@procedureID,@fillObj,
		@purchItemID,@taskComment,@procHistID
while (@@fetch_status = 0)
	Begin
	print '#$#$#$#$#$#$#$#$# Making a task for fill obj id = ' + @fillObj
	if @purchaser is null
		begin
		print 'ERRRRROORRRR -- Purchaser is null in procedure A_SP_PURCHASE_ITEM_CREATE_TASK'
		return 1
		end

	select @supHistID = HISTORY_REF_ID FROM A_COMPANIES WHERE ID = @supplierID

	if @stepInAP = 1
		SELECT top 1 @requesteeRole = OWNER_ROLE_ID FROM A_V_PROCEDURE_STEPS_WITH_LABOR_AND_PRINT_ORDER 
			WHERE PROCEDURE_ID = @procHistID AND (PREV_STEP is NULL OR P_ORDER = 1)
			ORDER BY P_ORDER,PREV_STEP
	else
		SELECT @requesteeRole = ROLE_ID FROM A_V_PROCEDURE_LABOR WHERE PROC_ID = @procHistID AND PROC_STEP IS NULL and LABOR_ROLE = 'LABOR_OWNER'

	if (@requesteeRole is null) and @objProdAppliesTo is null and exists(SELECT ID FROM A_PEOPLE WHERE ID = @fillObj)
		begin
		print 'This is a person fill'
		set @requesteeID = @fillObj
		goto makeTasks
		end

	if @requesteeRole is null
		begin
		print ' The requestee role is null so we will need to look at the product for some help '
		SELECT 	@mgrRole = MGR_TEAM FROM A_PRODUCTS_HISTORY WHERE ID = @prodHistID
		print 'The mgrRole after checking product = ' + isNULL(@mgrRole,'NULL')
		if @mgrRole is NULL
			begin
			print 'We can not find the labor role for this procedure anywhere so we will assign the filler to do this task'
			set @requesteeID = @strNTLogin
			if @procedureID is null
				begin
				print 'This product has no procedure so we need to make a dummy task'
				set @procedureName = 'No Procedure Purchase Item'
				end
			end
		else
			set @requesteeRole = @mgrRole
		end 

SELECT @acctSupplierID = SUPPLIER_ID FROM A_V_PURCHASE_ITEMS_WITH_ACCOUNT_SUPPLIER WHERE PURCHASE_ITEM_ID = @purchItemID
SELECT @requesteeRole = isNull(SUB_ROLE,@requesteeRole) FROM A_V_ROLES_APPROVED_DPARTMENT_SUB_ROLES WHERE DEPARTMENT = @acctSupplierID AND ID = @requesteeRole

makeTasks:
	print '@requesteeRole = ' + isNull(@requesteeRole,'NULL')
	print '@requesteeID = ' + isNull(@requesteeID,'NULL')

	if @requesteeRole is null and @requesteeID is null
		begin
			set @taskName = isnull(@taskName,'') + ' ---Could not find a requestee so I made purchaser the requestee'
			set @requesteeID = @purchaser
		end
	print 'Lets see if we already have a task made for this procedure'
	set @newTaskID = null

	if isNull(@parentOrderRole,'') = 'GROUPER'
		SELECT @newTaskID = oi.TASK_ID FROM A_TASK_ORDER_INFORMATION oi,A_TASK_OBJECT_LINK ol
			WHERE oi.PURCHASE_ITEM_ID = @purchItemID AND ol.TASK_ID = oi.TASK_ID 
				AND ol.OBJECT_ID = @fillObj and oi.PURCHASE_ITEM_ROLE = 'CHILD_PROC'
	else
		SELECT @newTaskID = oi.TASK_ID FROM A_TASK_ORDER_INFORMATION oi,A_TASK_OBJECT_LINK ol
			WHERE oi.PURCHASE_ITEM_ID = @purchItemID AND ol.TASK_ID = oi.TASK_ID 
				AND ol.OBJECT_ID = @fillObj and oi.PURCHASE_ITEM_ROLE = 'CHILD_PROC'

	--if the parent is a grouper and we are not a shipping task then we have to be part of the same fill
	if 	isNull(@parentOrderRole,'') = 'GROUPER' and isNull(@systemID,'') <> 'SYS_SHIPPING'
		begin
		print 'The system task is not shipping it is ' + isNull(@systemID,'NULL')
		if @parentFillID <> @fillID
			goto finTaskMaking
		end

	if @newTaskID is NULL
		begin
		select @pplHistID = HISTORY_REF_ID FROM A_PROD_PRICE_LIST WHERE ID = @pplID
		print 'now we need to see if we are creating a procedure with shipping'
		set @newTaskGroupName = NULL
		set @taskProc = @procedureID
		if isNull(@parentOrderRole,'') <> 'GROUPER'
			if exists(SELECT * FROM A_ORDER_ITEMS WHERE PARENT = @purchItemID AND PROC_SYS_ID = 'SYS_SHIPPING')
				begin
				print 'WE ARE CREATING A PURCH ITEM GROUPER WITH SHIPPING!!!!!!'
				select @newTaskGroupName = 'Ship and do the ' + @taskName + ' on ' + OBJ_DESC FROM
					A_V_APPROVED_OBJECTS WHERE ID = @fillOBj
				set @taskName = @newTaskGroupName
				set @myRole = 'GROUPER'
				set @taskProc = null
				print '!!!!!!!!!!! PPHIST ID = ' + @pplHistID
 				select @leadTime = PRODUCTION_TIME,@leadTimeUnits = PRODUCTION_TIME_UNIT 
 					FROM A_PROD_PRICE_LIST_HISTORY WHERE ID = @pplHistID
 				select @origStopDate = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@leadTimeUnits,@curTime,@leadTime)
 				select @origStartDate = getDate()
				end
			else
				begin
				set @myRole = 'SOLO_PROC'
 				select @leadTime = PRODUCTION_TIME,@leadTimeUnits = PRODUCTION_TIME_UNIT 
 					FROM A_PROD_PRICE_LIST_HISTORY WHERE ID = @pplHistID
 				select @origStopDate = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@leadTimeUnits,@curTime,@leadTime)
 				select @origStartDate = getDate()
				end
		else
			begin
			if @DEST = 'to'
				begin
				set @myRole = 'TO_SHIP'
  				SELECT @origStartDate = isNull(ORIG_PLANNED_START_DATE,@curTime) 
					FROM A_TASKS WHERE ID = @parentTaskID
 				select @leadTime = PRODUCTION_TIME,@leadTimeUnits = PRODUCTION_TIME_UNIT 
 					FROM A_PROD_PRICE_LIST_HISTORY WHERE ID = @pplHistID
 				select @origStopDate = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@leadTimeUnits,@origStartDate,@leadTime)
				end
			if @DEST = 'from'
				begin
 				set @myRole = 'FROM_SHIP'
 				select @origStartDate = ORIG_PLANNED_STOP_DATE
 						FROM A_V_TASK_SEARCH 
 						WHERE PURCHASE_ITEM_ROLE = 'CHILD_PROC' 
 								AND PARENT_ID = @parentTaskID
 				set @origStartDate = isNull(@origStartDate,getDate())
				select @leadTime = PRODUCTION_TIME,@leadTimeUnits = PRODUCTION_TIME_UNIT 
					FROM A_PROD_PRICE_LIST_HISTORY WHERE ID = @pplHistID
				select @origStopDate = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@leadTimeUnits,@origStartDate,@leadTime)
				end
			if @DEST is NULL
				begin
				set @myRole = 'CHILD_PROC'
 				select @origStartDate = ORIG_PLANNED_STOP_DATE
 						FROM A_V_TASK_SEARCH 
 						WHERE PURCHASE_ITEM_ROLE = 'TO_SHIP' 
 								AND PARENT_ID = @parentTaskID
 				set @origStartDate = isNull(@origStartDate,getDate())
				select @leadTime = PRODUCTION_TIME,@leadTimeUnits = PRODUCTION_TIME_UNIT 
					FROM A_PROD_PRICE_LIST_HISTORY WHERE ID = @pplHistID
				select @origStopDate = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@leadTimeUnits,@origStartDate,@leadTime)
				end
			end

		exec A_SP_TASKS_UPDATE_TASK @newTaskID OUTPUT,@messages OUTPUT,
		null, --ID
		@parentTaskID, --Parent,
		@purchaser,--Requestor
		@requesteeID, --REQUESTEE_ID,
		@requesteeRole, --GROUP_REQUESTEE_ID
		@taskName, --DESCRIPTION 
		@taskName, --Title
		@systemID, --SYSTEM_TASK,
		@taskComment, --COMMENT,
		'3', --Security Level
		null, --counter
		@origStartDate, --orig planned Start Date
		@origStopDate, --orig planned stop date,
		null, --@ORIG_PLANNED_COUNTER_START numeric,
		null, --@ORIG_PLANNED_COUNTER_STOP numeric,
		null, --@CUR_PLANNED_START_DATE dateTime,
		null, --@CUR_PLANNED_STOP_DATE dateTime,
		null, --@CUR_PLANNED_COUNTER_START numeric,
		null, --@CUR_PLANNED_COUNTER_STOP numeric,
		null, --@ACTUAL_START_DATE dateTime,
		null, --@ACTUAL_STOP_DATE dateTime,
		null, --@ACTUAL_COUNTER_START numeric,
		null, --@ACTUAL_COUNTER_STOP numeric,
		@taskProc, --@REF_PROC_LIST varchar(8000),
		null, --@REF_FILE_LIST varchar(8000),
		null, --@DISCUSSIONS varchar(8000),
		null, --@SURVEYS varchar(8000),
		null, --@MEETINGS varchar(8000),
		@fillObj, --Object varchar(8000),
		null, --@COMPANIES varchar(8000),
		null, --@SYSTEM_PEOPLE_TO_EMAIL varchar(8000),
		'1', --@PRIORITY smallInt,
		null, --@ADDITIONAL_ASSIGNEES varchar(8000),
		@custPers --strNTLogin
		print 'Created A Task with the ID of ' + @newTaskID
		--exec A_SP_TASK_CREATE_MONITORS_FROM_PROCEDURE_INFO @newTaskID,@strNTLogin
		print 'before the fill if the dest is to and we just filled it then the from_loc must be filled'
		if @dest = 'to'
			begin
			SELECT @fromLoc = LOCATION FROM A_ACTUAL_PARTS_HISTORY WHERE OBJECT_ID = @fillObj
			SELECT @toLoc = TO_LOC  FROM A_ORDER_ITEMS WHERE ID = @purchItemID
			end

		if not exists(SELECT * FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @newTaskID)
			INSERT INTO A_TASK_ORDER_INFORMATION 
			(TASK_ID) VALUES (@newTaskID)

		UPDATE A_TASK_ORDER_INFORMATION SET
			ACTUAL_TO_LOC = @toLoc,ACTUAL_FROM_LOC = @fromLoc,
			PURCHASE_ITEM_ID = @purchItemID,PURCHASE_HIST_ID = @purchHistID,
			DRCM = getDate(),MODBY = @strNTLogin,
			FILL_ITEM_ID = @fillID
			WHERE TASK_ID = @newTaskID

		print '+++++++++++++++++++ Set the to Loc to ' + @toLoc
		
		--Update the Purchase Role
			UPDATE A_TASK_ORDER_INFORMATION SET
				PURCHASE_ITEM_ROLE = @myRole WHERE TASK_ID = @newTaskID

		if @myRole = 'GROUPER' or @myRole = 'SOLO_PROC'
			exec A_SP_TASK_SUBMIT @SUBMIT_STATUS OUTPUT,@messages OUTPUT,@newTaskID,@requesteeID
		else
			UPDATE A_TASKS SET STATUS = 'PENDING_PARENT_ACCEPTANCE' WHERE ID = @newTaskID 		if isNull(@myRole,'') <> 'GROUPER'
			UPDATE A_TASKS SET [PROCEDURE_ID] = @procedureID WHERE ID = @newTaskID

		if exists(SELECT * FROM A_TASKS WHERE ID = @parentTaskID AND SYSTEM_TASK = 'SYS_PURCHASE')
			begin
			SELECT @purchObjID = OBJECT_ID FROM A_PURCHASES_HISTORY WHERE ID = @purchHistID
			SELECT @COMMENTS = COMMENTS FROM A_PURCHASE_FUTURE_FILL_ITEM WHERE PURCHASE_ID = @purchObjID
			exec A_SP_TASK_COMMENT_UPDATE_ONE_COMMENT null,null,null,@newTaskID,@COMMENTS,'ALL',@strNTlogin
			end
		end
	else
		begin
		print 'We already created this task and the ID of the task is : ' + @newTaskID
		end

	exec A_SP_TASK_AUTOMATIC_ORDER_UPDATER @newTaskID,@strNTLogin

	print 'Now we need to make sure all the child tasks are under way as well.'
	exec A_SP_TASK_CREATE_CHILD_TASKS_FOR_PROCEDURE_STEPS @newTaskID,@custPers

	if @myRole = 'GROUPER'
		begin
		set @curs2 = Cursor For SELECT ID FROM A_ORDER_ITEMS 
			WHERE PARENT = @purchItemID AND ADD_COST_ID IS NULL AND DEST = 'to'
		open @curs2
			Fetch Next from @curs2 Into @pItemID
			while (@@fetch_status = 0)
				Begin
				print 'Creating a task for purchase item = ' + @pItemID
				exec A_SP_PURCHASE_ITEM_CREATE_TASK @pItemID,@newTaskID,@strNTLogin
				Fetch Next from @curs2 Into @pItemID
				End
		close @curs2


		exec A_SP_PURCHASE_ITEM_CREATE_TASK @purchItemID,@newTaskID,@strNTLogin


		set @curs2 = Cursor For SELECT ID FROM A_ORDER_ITEMS 
			WHERE PARENT = @purchItemID AND ADD_COST_ID IS NULL AND DEST = 'from'
		open @curs2
			Fetch Next from @curs2 Into @pItemID
			while (@@fetch_status = 0)
				Begin
				print 'Creating a task for purchase item = ' + @pItemID
				exec A_SP_PURCHASE_ITEM_CREATE_TASK @pItemID,@newTaskID,@strNTLogin
				Fetch Next from @curs2 Into @pItemID
				End
		close @curs2

		Deallocate @curs2


		end --Grouper stuff
	if @procedureID is null
		begin
		declare @gg varchar(2000),@meDate datetime
		set @meDate = dateAdd(n,5,getDate())
		set @gg = 'exec A_SP_TASK_QUICK_CLOSE null,null,''' + @newTaskID + ''',''' + @strNTLogin + ''''
		exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP_WITH_DATE @gg,@meDate,@strNTlogin

		end 	finTaskMaking:
	Fetch Next from @curs Into
		@fillID,@pplID,
		@taskName,@dest,@fromLoc,@toLoc,
		@prodHistID,@purchHistID,@purchaser,
		@stepInAP,@custPers,@supplierID,
		@procedureName,@systemID,@procedureID,
		@fillObj,@purchItemID,@taskComment,@procHistID
	End
close @curs
Deallocate @curs

fin:

set @mySQL = 'exec A_SP_PURCHASE_UPDATE_STATUS_OF_ALL_TASKS ' +
	isnull('''' + @purchHistID + '''','NULL') + ',' +
	isnull('''' + @strNTLogin + '''','NULL') + ''
exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @mySQL,@strNTLogin

print 'Exiting A_SP_PURCHASE_ITEM_CREATE_TASK'
























