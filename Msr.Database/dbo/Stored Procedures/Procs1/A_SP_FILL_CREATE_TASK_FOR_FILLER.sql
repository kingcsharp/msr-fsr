







CREATE         PROCEDURE DBO.A_SP_FILL_CREATE_TASK_FOR_FILLER
@fillID varchar(50)
AS
declare @fillerPerson varchar(50),@fillerRole varchar(50),@fillBy varchar(50),
	@supplier varchar(50),@customer varchar(50),@purchaser varchar(50), @foDesc nvarchar(1000),
	@fProdName nvarchar(1000), @fProcName nvarchar(1000),@fillCustName nvarchar(1000),@supplierName nvarchar(50),
	@fPurchaseID varchar(50),@fProdID varchar(50),@purchItemID varchar(50),@purchHistID varchar(50)
print 'Creating the task for the person or role to fill #' + @fillID
SELECT @fillBY = FILL_BY,@customer = CUSTOMER, @supplier = SUPPLIER,
		@purchaser = PURCHASER_ID, @foDesc = FILL_OBJ_DESC, @fProdName = PROD_NAME, @fProdID = PROD_ID,
		@fProcName = PROC_NAME,@fillCustName = CUST_NAME,@supplierName = SUP_NAME,@fPurchaseID = PURCHASE_ID,
		@purchItemID = PURCH_ITEM_ID,@purchHistID = PURCHASE_HIST_ID
	FROM A_V_FILLS_SEARCH WHERE ID = @fillID
print 'This is supposed to be filled by ' + @fillBy
if @fillBy = 'PARENT_FILL_ITEM' goto fin
if @fillBY = 'SUPPLIER'
	begin
	print 'This is supposed to be filled by the supplier, so go find the role to fill'
	SELECT @fillerRole = ROLE_ID FROM A_ADMIN_ROLE_JOBS WHERE CO_ID = @supplier AND JOB = 'ORDER_FILLING'
	if @fillerRole is null 
		SELECT @fillerRole = ID FROM A_APPROVED_ROLES 
			WHERE IS_ADMIN = 1 
				AND CREATING_CO = (SELECT ROOT_CO FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supplier)
	end

if @fillBY = 'CUSTOMER'
	begin
	print 'This is supposed to be filled by the customer, so make the purchaser fill it'
	SET @fillerPerson = @purchaser

	if @fillerPerson is null 
		SELECT @fillerRole = ID FROM A_APPROVED_ROLES 
			WHERE IS_ADMIN = 1 
				AND CREATING_CO = (SELECT ROOT_CO FROM A_COMPANIES_APPROVED_DATA WHERE ID = @customer)
	end

declare @newTaskID varchar(50),@messages varchar(50),@desc nvarchar(2000),@startDate datetime,@stopDate datetime
exec A_SP_FILL_FIGURE_OUT_START_AND_STOP_DATES @startDate OUTPUT,@stopDate OUTPUT,@purchItemID
SELECT @startDate = dbo.A_FN_DATE_TIME_GET_LOCAL_TIME_FOR_PERSON(@purchaser,@startDate),
	@stopDate = dbo.A_FN_DATE_TIME_GET_LOCAL_TIME_FOR_PERSON(@purchaser,@stopDate)
set @desc = 'Fill ' + isNull(@foDesc,'') + 
	' for ' + isNull(@fillCustName, ' Some Customer') +
	' purchase ' + isNull('#' + @fPurchaseID,' ') +
	' from ' + isNull(@supplierName,' Some Supplier') +
	'. ' + isNull('Product Name = ' + @fProdName,' Not sure of the product') +
	'. ' + isNull('Procedure Name = ' + @fProcName,' Not sure of the procedure') + '.'



exec A_SP_TASKS_UPDATE_TASK @newTaskID OUTPUT,@messages OUTPUT,
	null, --ID
	null, --Parent,
	@purchaser,--Requestor
	@fillerPerson, --REQUESTEE_ID,
	@fillerRole, --GROUP_REQUESTEE_ID
	@desc, --DESCRIPTION 
	@desc, --Title
	'SYS-FILL', --SYSTEM_TASK,
	null, --COMMENT,
	'3', --Security Level
	null, --counter
	@startDate, --orig planned Start Date
	@stopDate, --orig planned stop date,
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
	null, --@REF_PROC_LIST varchar(8000),
	null, --@REF_FILE_LIST varchar(8000),
	null, --@DISCUSSIONS varchar(8000),
	null, --@SURVEYS varchar(8000),
	null, --@MEETINGS varchar(8000),
	null, --Object varchar(8000),
	null, --@COMPANIES varchar(8000),
	null, --@SYSTEM_PEOPLE_TO_EMAIL varchar(8000),
	'1', --@PRIORITY smallInt,
	null, --@ADDITIONAL_ASSIGNEES varchar(8000),
	@purchaser --strNTLogin
print 'Created the task with the ID of ' + @newTaskID
IF NOT EXISTS (SELECT * FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @newTaskID)
	INSERT INTO A_TASK_ORDER_INFORMATION (TASK_ID,FILL_ID,PURCHASE_HIST_ID) VALUES(@newTaskID,@fillID,@purchHistID)
else
	UPDATE A_TASK_INFORMATION SET FILL_ID = @fillID,PURCHASE_HIST_ID = @purchHistID WHERE TASK_ID = @newTaskID

UPDATE A_FILLS SET [TASK_ID] = @newTaskID WHERE ID = @fillID
declare @SUBMIT_STATUS varchar(50)
UPDATE A_TASKS SET IS_FILL = 1 WHERE ID = @newTaskID
exec A_SP_TASK_SUBMIT @SUBMIT_STATUS OUTPUT,@messages OUTPUT,@newTaskID,@purchaser



fin:







