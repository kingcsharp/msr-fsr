














CREATE                PROCEDURE DBO.A_SP_ORDERS_FINISH_WF
	@myID nvarchar(50),
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
as
begin transaction
print 'Finishing the wf for an Order'
declare @myObj as nvarchar(50)
declare @myRoot as nvarchar(50)
if @objID is null
	SELECT @myObj = OBJECT_ID FROM A_ORDERS_HISTORY WHERE ID = @myID
else
	begin
	set @myObj = @objID
	SELECT @myID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID
	end

SELECT @myRoot = ROOT FROM A_OBJECTS WHERE ID = @myObj
print 'Updating A_ORDERS to latest rev'
declare @tester as nvarchar(50) --Test to see if it is in the table
SELECT @tester = ID FROM A_ORDERS WHERE ID = @myRoot 
if @tester is Null  --it is not so add it
	begin
		print 'tester was null so i need to go ahead and make a record in A_ORDERS for this item'
		INSERT INTO A_ORDERS(ID,HISTORY_REF_ID,DRCM,MODBY) VALUES (@myRoot,@myID,getDate(),@strNTLogin)
	end

--so now make sure that the value in A_ORDERS is the latest
declare @curID as nvarchar(50)
SELECT @curID = OBJ_ID FROM A_OBJECTS WHERE ROOT = @myROOT AND STATUS IN
('APPROVED','APPROVED_BUT_REVISING','APPROVED_BUT_DELETING')
exec A_SP_UPDATE_TABLE_FIELD @curID,@myRoot,'A_ORDERS','HISTORY_REF_ID'
print 'Order progress needs to be set to approved by Customer Co'
UPDATE A_ORDERS_HISTORY SET PROGRESS = 'APPROVED_BY_CUSTOMER' WHERE ID = @myID
UPDATE A_ORDERS_HISTORY 
	SET PRICE =
		(SELECT SUM(TOTAL_PRICE) FROM A_ORDER_ITEMS 
		WHERE PARENT IS NULL AND ORDER_ID = @myID)
	WHERE ID = @myID


exec A_SP_ORDER_LOCK_IN_PRICE_LISTS @myRoot,@strNTLogin
exec A_SP_QUOTE_ORDER_LINK_CREATE_LINK_FOR_ORDER @myRoot,@strNTLogin
exec A_SP_ORDER_UPDATE_FORECAST_FUNNEL_FOR_ORDER_ITEMS @myID,@strNTLogin
print 'creating the parts'
exec A_SP_ORDER_SET_UP_CUSTOMER_PARTS @myRoot,@strNTLogin
if @@ERROR <> 0 GOTO problem
UPDATE A_ORDERS SET STATUS = (SELECT STATUS FROM A_OBJECTS WHERE ROOT = @myRoot) WHERE ID = @myRoot
if @@ERROR <> 0 GOTO problem


fin:
if @@trancount > 0 	commit transaction
return 0
problem:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_ORDERS_FINISH_WF and we will terminate and not finish anything '
return 1














