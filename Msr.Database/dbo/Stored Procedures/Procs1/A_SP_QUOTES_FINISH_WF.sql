






CREATE          PROCEDURE DBO.A_SP_QUOTES_FINISH_WF
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
print 'Finishing the wf for a Quote'
declare @myObj as nvarchar(50)
declare @myRoot as nvarchar(50)
SELECT @myObj = OBJECT_ID FROM A_QUOTES_HISTORY WHERE ID = @myID
SELECT @myRoot = ROOT FROM A_OBJECTS WHERE ID = @myObj
print 'Updating A_QUOTES to latest rev'
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_QUOTES WHERE ID = @myRoot 
if @tester is Null  --it is not so add it
	begin
		print 'tester was null so i need to go ahead and make a record in A_QUOTES for this item'
		INSERT INTO A_QUOTES (ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@myID,getDate(),@strNTLogin)
	end

--so now make sure that the value in A_QUOTES is the latest
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_QUOTES','HISTORY_REF_ID'

if @curID is not null
	begin
	print 'Now we need to set the quote status to sent to customer because it is now going to be on them to accept it on their order'
	UPDATE A_QUOTES_HISTORY SET PROGRESS = 'SENT_TO_CUSTOMER' WHERE ID = @curID
	UPDATE A_QUOTE_ORDER_LINK SET STATUS = 'Q_SENT_TO_CUST' WHERE QUOTE_ID = @curID
	end

declare @ordID varchar(50)
SELECT @ordID = ORDER_ID FROM A_QUOTES_HISTORY WHERE ID = @curID
exec A_SP_ORDER_UPDATE_QUOTE_STATUS_FOR_ORDER @ordID,@strNTLogin

declare @myTaskID varchar(50),@taskPerson varchar(50)
SELECT @myTaskID = TASK_ID FROM A_QUOTE_ORDER_LINK WHERE QUOTE_ID = @curID
SELECT @taskPerson = REQUESTEE_ID FROM A_TASKS WHERE ID = @myTaskID
declare @RET_STATUS varchar(50)
declare @MSGS varchar(500)
if @myTaskID is not null exec A_SP_TASK_QUICK_CLOSE @RET_STATUS OUTPUT,@MSGS OUTPUT, @myTaskID,@taskPerson

declare @qaTask varchar(50),@QOL_ID varchar(50)
SELECT @qaTask = QA_TASK_ID,@QOL_ID = ID  FROM A_QUOTE_ORDER_LINK WHERE QUOTE_ID = @curID
if @qaTask is null
	begin
	print 'This QOL item does not have an accpt task and it should'
	exec A_SP_QUOTE_ORDER_LINK_CREATE_QA_TASK @QOL_ID,@strNTLogin
	end

exec A_SP_QUOTE_UPDATE_FORECAST_FUNNEL_FOR_QUOTE_ITEMS @myID,@strNTLogin





