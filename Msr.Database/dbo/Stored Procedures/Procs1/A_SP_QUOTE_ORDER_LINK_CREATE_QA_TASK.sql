

CREATE   PROCEDURE DBO.A_SP_QUOTE_ORDER_LINK_CREATE_QA_TASK
@QOL_ID varchar(50),
@strNTLogin varchar(50)
AS
print 'We need to get the person who did the task id'
declare @qTaskID varchar(50),@reqID varchar(50)
SELECT @qTaskID = TASK_ID FROM A_QUOTE_ORDER_LINK WHERE ID = @QOL_ID
SELECT @reqID = REQUESTEE_ID FROM A_TASKS WHERE ID = @qTaskID

print 'Creating a task for QA'
declare @custPerson varchar(50),@orderID varchar(50),@supplierID varchar(50),@qhID varchar(50),
	@quoteID varchar(50),@orderName varchar(50)
SELECT @orderID = ORDER_ID,@supplierID = SUPPLIER,@qhID = QUOTE_ID 
	FROM A_QUOTE_ORDER_LINK WHERE ID = @QOL_ID
SELECT @quoteID = ID FROM A_QUOTES WHERE HISTORY_REF_ID = @qhID
SELECT @orderName = oh.DESCRIPTION FROM A_ORDERS o,A_ORDERS_HISTORY oh WHERE o.HISTORY_REF_ID = oh.ID AND o.ID = @orderID
print 'The Order ID is ' + @orderID
print 'Now to get the customer Person ID from the order info'
SELECT @custPerson = CUSTOMER_PERSON FROM A_V_ORDERS_APPROVED_DATA WHERE ID = @orderID
print ' The customer person is ' + @custPerson
declare @desc varchar(500),@newTaskID varchar(50),@messages varchar(500),@startDate dateTime,@stopDate dateTime,@supplierName nvarchar(200)
SELECT @supplierName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supplierID
set @desc = 'Accept Quote # ' + @quoteID + ' from ' + @supplierName + ' for RFQ #' + @orderID + ' (' + @orderName + ')'
SELECT  @startDate = dbo.timeToLocal(getDate(),@strNTLogin), @stopDate = dbo.timeToLocal(dateAdd(dd,2,getDate()),@strNTLogin)
exec A_SP_TASKS_UPDATE_TASK @newTaskID OUTPUT,@messages OUTPUT,
	null, --ID
	null, --Parent,
	@reqID,--Requestor
	@custPerson, --REQUESTEE_ID,
	null, --GROUP_REQUESTEE_ID
	@desc, --DESCRIPTION 
	@desc, --Title
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
UPDATE A_QUOTE_ORDER_LINK SET [QA_TASK_ID] = @newTaskID WHERE ID = @QOL_ID
declare @SUBMIT_STATUS varchar(50)
UPDATE A_TASKS SET IS_QUOTE_ACCEPT = 1 WHERE ID = @newTaskID
exec A_SP_TASK_SUBMIT @SUBMIT_STATUS OUTPUT,@messages OUTPUT,@newTaskID,@strNTLogin








