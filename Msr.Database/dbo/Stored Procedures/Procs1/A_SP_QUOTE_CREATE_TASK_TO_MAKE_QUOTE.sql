
CREATE       PROCEDURE dbo.A_SP_QUOTE_CREATE_TASK_TO_MAKE_QUOTE
@quoteOrderLinkID varchar(50),
@strNTLogin varchar(50)
AS

declare @supplierCo varchar(50),@supQuoteRole varchar(50),@orderID varchar(50),@custCo varchar(50),@custRootCo varchar(50),
	@supRootCo varchar(50),@JOB varchar(50),@custPerson varchar(50)
SELECT @supplierCo = SUPPLIER,@orderID = ORDER_ID,@custCo = CUSTOMER FROM A_QUOTE_ORDER_LINK WHERE ID = @quoteOrderLinkID
SELECT @supRootCo = ROOT_CO FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supplierCo
SELECT @custRootCo = ROOT_CO FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @custCo

print 'The supplier Company is ' + @supplierCo
print 'The customer Company is ' + @custCo
print 'The root Supplier Company is ' + @supRootCo
print 'The customer Root Company is ' + @custRootCo

if @supRootCo = @custRootCo set @JOB = 'QUOTE_HANDELING'
else set @JOB = 'EXTERNAL_QUOTE_HANDELING'

SELECT @supQuoteRole = ROLE_ID FROM A_ADMIN_ROLE_JOBS WHERE CO_ID = @supplierCo and JOB = @JOB
if @supQuoteRole is null
	begin
	print 'There is no role set up to handle the quotes for this company.'
	SELECT @supQuoteRole = ID FROM A_APPROVED_ROLES 
		WHERE IS_ADMIN = 1 AND CREATING_CO = (SELECT ROOT_CO FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supplierCo)
	end
print 'The supQuoteRole is ' + isNull(@supQuoteRole,'NULL')
declare @desc varchar(500),@newTaskID varchar(50),@messages varchar(500),@startDate dateTime,@stopDate dateTime
set @desc = 'Create Purchase Agreement Response for Purchase Agreement # ' + @orderID
SELECT  @startDate = getDate(), @stopDate = dateAdd(dd,2,getDate())

SELECT @custPerson = CUSTOMER_PERSON FROM A_V_ORDERS_APPROVED_DATA WHERE ID = @orderID
	
exec A_SP_TASKS_UPDATE_TASK @newTaskID OUTPUT,@messages OUTPUT,
	null, --ID
	null, --Parent,
	@custPerson,--Requestor
	null, --REQUESTEE_ID,
	@supQuoteRole, --GROUP_REQUESTEE_ID
	@desc, --DESCRIPTION 
	null, --SYSTEM_TASK,
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
	@strNTlogin --strNTLogin
print 'Created the task with the ID of ' + @newTaskID
UPDATE A_QUOTE_ORDER_LINK SET [TASK_ID] = @newTaskID WHERE ID = @quoteOrderLinkID
declare @SUBMIT_STATUS varchar(50)
UPDATE A_TASKS SET IS_QUOTE = 1 WHERE ID = @newTaskID
exec A_SP_TASK_SUBMIT @SUBMIT_STATUS OUTPUT,@messages OUTPUT,@newTaskID,@strNTLogin






