












CREATE              PROCEDURE dbo.A_SP_FILL_ASSIGN_PROCEDURE
@fillID varchar(50),
@strNTLogin varchar(50)
AS
print 'In SP = A_SP_FILL_ASSIGN_PROCEDURE'
declare @phID varchar(50), @stepInAP smallInt,@custPers varchar(50),@requesteeRole varchar(50),
	@phName nvarchar(2000),@systemID varchar(50),@procID varchar(50),@fillObj varchar(50),
	@newTaskID nvarchar(50),@messages nvarchar(2000),@taskComment nvarchar(2000), @supplierID varchar(50),
	@SUBMIT_STATUS varchar(50),@prodHistID varchar(50),@mgrRole varchar(50),
	@requesteeID varchar(50),@purchaser varchar(50),@purchItemID varchar(50)

print 'SELECT PROCEDURE_HIST_ID, STEPS_IN_AP, CUSTOMER_PERSON, SUPPLIER_ID,
	NAME, SYSTEM_ID, PROCEDURE_ID, FILL_OBJ_ID, QUOTE_ITEM_ID,
	''Created for FIll = '' + isNull(FILL_ID,''NULL'') + '' OrderItem ID = '' + isNull(ORDER_ITEM_ID,''NULL''),
	PRODUCT_HIST_ID,ORDER_ITEM_ID  FROM A_V_FILLS_WITH_PROCEDURE_AND_PRODUCT_INFORMATION
	WHERE FILL_ID = ''' + @fillID + ''''

SELECT @phID = PROCEDURE_HIST_ID,
	@purchaser = PURCHASER_ID,
	@stepInAP = STEPS_IN_AP,
	@custPers = CUSTOMER_PERSON,
	@supplierID = SUPPLIER,
	@phName = PROC_NAME,
	@systemID = SYS_PROC_ID,
	@procID = PROC_ID,
	@fillObj = FILL_OBJ_ID,
	@purchItemID = PURCH_ITEM_ID,
	@taskComment = 'Created for FIll = ' + ID,
	@prodHistID = PROD_HIST_ID
	FROM A_V_FILLS_SEARCH
	WHERE ID = @fillID

if @purchaser is null
	begin
	print 'Purchaser is null'
	return 1
	end

print 'presystemID = '
print @systemID
declare @supHistID as varchar(50)
select @supHistID = HISTORY_REF_ID FROM A_COMPANIES WHERE ID = @supplierID
print 'Procedure Hist ID = ' + @phID
print 'Steps in AP = '
print @stepInAP
print 'systemID = '
print @systemID
print 'supplierID = '
print @supplierID

if @stepInAP = 1
	begin
	print 'We need to figure out who is going to do this task from the steps'
	print 'We will choose this owner from the steps that are first to execute'
	SELECT top 1 @requesteeRole = OWNER_ROLE_ID FROM A_V_PROCEDURE_STEPS_WITH_LABOR_AND_PRINT_ORDER 
		WHERE PROCEDURE_ID = @phID AND (PREV_STEP is NULL OR P_ORDER = 1)
	end

if @requesteeRole is null
	begin
	SELECT @requesteeRole = ROLE_ID FROM A_V_PROCEDURE_LABOR WHERE PROC_ID = @phID AND PROC_STEP IS NULL and LABOR_ROLE = 'LABOR_OWNER'
	print 'The role after checking the procedure is ' + isNULL(@requesteeRole,'NULL')
	end

if @requesteeRole is null
	begin
	print ' The requestee role is null so we will need to look at the product for some help '
	SELECT 	@mgrRole = MGR_TEAM FROM A_PRODUCTS_HISTORY WHERE ID = @prodHistID
	print 'The mgrRole after checking produst = ' + isNULL(@mgrRole,'NULL')
	if @mgrRole is NULL
		begin
		print 'We can not find the labor role for this procedure anywhere so we will assign the filler to do this task'
		set @requesteeID = @strNTLogin
		end
	end 
set @phName = @phName + ' PI = ' + @purchItemID
declare @acctSupplierID varchar(50)
SELECT @acctSupplierID = SUPPLIER_ID FROM A_V_PURCHASE_ITEMS_WITH_ACCOUNT_SUPPLIER WHERE PURCHASE_ITEM_ID = @purchItemID
SELECT @requesteeRole = isNull(SUB_ROLE,@requesteeRole) FROM A_V_ROLES_APPROVED_DPARTMENT_SUB_ROLES WHERE DEPARTMENT = @acctSupplierID AND ID = @requesteeRole
set @phName = @phName + ' PI = ' + @purchItemID + ' role = ' + @requesteeRole

print '@requesteeRole = ' + isNull(@requesteeRole,'NULL')
print '@requesteeID = ' + isNull(@requesteeID,'NULL')

print 'Lets see if we already have a task made for this procedure'
--SELECT @newTaskID = TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE FILL_ID = @fillID
if @newTaskID is NULL
	begin
	exec A_SP_TASKS_UPDATE_TASK @newTaskID OUTPUT,@messages OUTPUT,
	null, --ID
	null, --Parent,
	@purchaser,--Requestor
	@requesteeID, --REQUESTEE_ID,
	@requesteeRole, --GROUP_REQUESTEE_ID
	@phName, --DESCRIPTION 
	@phName, --Title
	@systemID, --SYSTEM_TASK,
	@taskComment, --COMMENT,
	'3', --Security Level
	null, --counter
	null, --orig planned Start Date
	null, --orig planned stop date,
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
	@procID, --@REF_PROC_LIST varchar(8000),
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
	
	print 'Since this is created as the result of an order we need to get the order information adn put it in the
			A_TASK_ORDER_INFORMATION table'
	print 'All done with the fill.  Now if the fill is for a ship then 
			we update the orderItem to have this actual part as the from location'
	
	declare @procSys as varchar(50),@dest as varchar(50),@fromLoc varchar(50),@toLoc varchar(50)
	SELECT @procSys = PROC_SYS_ID, @dest = DEST FROM A_ORDER_ITEMS WHERE ID = @purchItemID
	print 'before the fill if the dest is to and we just filled it then the from_loc must be filled'
	SELECT @fromLoc = LOCATION FROM A_ACTUAL_PARTS_HISTORY WHERE OBJECT_ID = @fillObj
	SELECT @toLoc = TO_LOC FROM A_ORDER_ITEMS WHERE ID = @purchItemID
	
	if not exists(SELECT * FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @newTaskID)
		INSERT INTO A_TASK_ORDER_INFORMATION 
		(TASK_ID,FILL_ID,ACTUAL_TO_LOC,ACTUAL_FROM_LOC)
		VALUES
		(@newTaskID,@fillID,@toLoc,@fromLoc)
	else
		UPDATE A_TASK_ORDER_INFORMATION
		SET FILL_ID = @fillID,ACTUAL_TO_LOC = @toLoc,ACTUAL_FROM_LOC = @fromLoc
		WHERE TASK_ID = @newTaskID
	
	UPDATE A_TASKS SET [PROCEDURE_ID] = @procID WHERE ID = @newTaskID
	exec A_SP_TASK_SUBMIT @SUBMIT_STATUS OUTPUT,@messages OUTPUT,@newTaskID,@custPers
	end
else
	begin
	print 'We already started this task and the ID of the task is : ' + @newTaskID
	end

print 'Now we need to make sure all the child tasks are under way as well.'
exec A_SP_TASK_CREATE_CHILD_TASKS_FOR_PROCEDURE_STEPS @newTaskID,@custPers




print 'Finished creating task'

fin:









