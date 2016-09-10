



CREATE    PROCEDURE dbo.A_SP_PURCHASE_ITEM_INVOICE_ITEM
@ID varchar(50),
@FILL_ITEM_ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Invoicing order Item # ' + isNull(@ID,'NULL') + ' FILL ITEM ID = ' + isNull(@FILL_ITEM_ID,'NULL')
declare @acctID varchar(50),@price money,@invoiceID varchar(50),@nextInvoiceDate datetime,@firstInvoiceDate datetime,
	@acctHistID varchar(50),@period varchar(50),@periodNumber int,@invDate dateTime,
	@invTrigger varchar(50),@purchHistID varchar(50),@pplID varchar(50),@pplBillType varchar(50),
	@intBillIT tinyInt,@myBillType varchar(10),@exCostID varchar(50),@parentID varchar(50),
	@acctType varchar(50),@invItemID varchar(50),@purchaseID varchar(50),
	@custCo varchar(50),@supplierCo varchar(50),@purchaser varchar(50),@quoteID varchar(50),@custSinglePONum varchar(50),
	@custLineItem varchar(50),@myCost money,@taskID varchar(50),@failedMonitors tinyint,@postDate datetime,@TAX_RATE float,
	@myFillQty float,@myUnitPrice money

SELECT @supplierCo = SUPPLIER_ID,@parentID = PARENT,@exCostID = ADD_COST_ID, @acctID = ACCOUNT_ID,@price = TOTAL_PRICE,@purchHistID = PURCHASE_HIST_ID,
	@pplID = PROD_PRICE_LIST, @myBillType = BILL_TYPE,@custLineItem = CUST_LINE_ITEM FROM A_ORDER_ITEMS WHERE ID = @ID

if @acctID is null
	begin
	SELECT @acctID = ACCOUNT_ID FROM A_ORDER_ITEMS WHERE PARENT = @ID
	if @acctID is null
		begin
		print 'Account ID is null have to exit'
		goto fin
		end
	end




SELECT @custCo = PURCHASING_CO,@purchaser = PURCHASER,@purchaseID = OBJECT_ID,@custSinglePONum = CUST_PURCH_NUM FROM A_PURCHASES_HISTORY WHERE ID = @purchHistID
SELECT @myCost = PRICE,@myFillQty = FILL_QTY,@myUnitPrice = PRICE / FILL_QTY FROM A_FILLS WHERE ID = @FILL_ITEM_ID
SELECT @pplBillType = INVOICE_FROM FROM A_V_PROD_PRICE_LIST_APPROVED_DATA WHERE ID = @pplID
SELECT @acctType = ACCT_TYPE,@TAX_RATE = ISNULL(TAX_RATE,0) FROM A_V_ACCOUNTS_APPROVED_DATA WHERE ID = @acctID
SELECT @taskID = TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE FILL_ITEM_ID = @FILL_ITEM_ID
SELECT @postDate = ACTUAL_STOP_DATE FROM A_TASKS WHERE ID = @taskID

if exists(SELECT ID FROM A_MONITOR_TEMPLATES WHERE  IS_PASSING = 0 AND 
	(
	TASK_ID = @taskID OR
	TASK_ID IN (SELECT ID FROM A_TASKS WHERE PARENT_ID = @taskID)
	)
)
	set @FailedMonitors = 1


-- if exists(SELECT ID FROM A_PURCHASES_HISTORY WHERE ID = @purchHistID AND PURCHASE_STATUS IN ('ITEM_NEEDS_ACCOUNT','WAITING_FILLS','ALL_ITEMS_HAVE_ACCOUNTS'))
-- 	begin
-- 	print ' So the problem is that something looks wrong in the purchase'
-- 	declare @pStat varchar(50)
-- 	SELECT @pStat = PURCHASE_STATUS FROM A_PURCHASES_HISTORY WHERE ID = @purchHistID
-- 	print ' The purchase status = ' + @pStat + ' and histID = ' + @purchHistID
-- 	print '**********EXITTING*************'
-- 	goto fin
-- 	end
if exists(SELECT ID FROM A_TASKS WHERE ID = @taskID AND not(STATUS IN ('FINISHED','CLOSED','COMPLETED')))
 	begin
 	print ' So the problem is that the task is not finished'
 	print '**********EXITTING*************'
 	goto fin
 	end

print '############We are going to invoice this one'
print 'The acctType = ' + @acctType + ' and the exCost = ' + isNull(@exCostID,'NULL')
print 'The account id is  ' + isNull(@acctID,'NULL')
print 'The purchase hist id = '+ @purchHistID
print 'The price List is = '+ @pplID
print 'The @pplBillType is = '+ @pplBillType
print 'My Bill Type = ' + @myBillType

--set @intBillIt = 0
--if @pplBillType = 'ACTUAL' and @myBillType in ('ACTUAL','ACT_EST') set @intBillIT = 1
--if @pplBillType = 'ESTIMATED' and @myBillType in ('ESTIMATE','ACT_EST') set @intBillIT = 1

--if @intBillIT = 1 
IF not exists(SELECT * FROM A_ACCOUNT_INVOICE_ITEMS WHERE FILL_ID = @FILL_ITEM_ID AND PURCH_ITEM_ID = @ID AND ACCOUNT_ID = @acctID)
	begin
	exec A_SP_ACCOUNT_INVOICES_GET_CURRENT_ACTIVE_INVOICE_OR_MAKE_ONE @invoiceID output,@acctID,@purchaseID,@strNTLogin
	exec sp_GetUniqueID3 @invItemID OUTPUT
	INSERT INTO A_ACCOUNT_INVOICE_ITEMS (
	ID,INVOICE_ID,ACCOUNT_ID,PURCH_ITEM_ID,
	STATUS,DRCM,MODBY,AMOUNT,
	DATE_POSTED,PURCHASE_ID,ITEM_TYPE,CUSTOMER_CO,
	SUPPLIER_CO,PURCHASER_ID,QUOTE_ID,CUST_SINGLE_PO,
	CUST_LINE_ITEM,FILL_ID,FAILED_MONITOR,TAX_RATE,TAX,QTY,UNIT_PRICE
	)
	VALUES (
	@invItemID,@invoiceID,@acctID,@ID,
	'CURRENT',getDate(),@strNTLogin,@myCost,
	@postDate,@purchaseID,'INV_ITEM_DEBIT',@custCo,
	@supplierCo,@purchaser,@quoteID,@custSinglePONum,
	@custLineItem,@FILL_ITEM_ID,@FailedMonitors,@TAX_RATE,(@myCost*(@TAX_RATE/100)),
	@myFillQty,@myUnitPrice
	)

	declare @invItemName nvarchar(2000)
	exec dbo.A_SP_ACCOUNT_INVOICE_ITEM_CREATE_NAME @invItemName OUTPUT,@invItemID
	UPDATE A_ACCOUNT_INVOICE_ITEMS SET DESCRIPTION = @invITemName WHERE ID = @invItemID
	exec A_SP_ACCOUNT_INVOICE_SET_TOTALS @invoiceID,@strNTLogin
	exec A_SP_ACCOUNT_UPDATE_ACCOUNT_AND_FORECASTS_WITH_INVOICE_ITEM_SUMS @acctID
	exec A_SP_ACCOUNT_INVOICE_INVOICE_IF_TIME @invoiceID,@strNTLogin
	end
--else
--	print 'We are not billing this one to the purchase account'
--exec A_SP_ACCOUNT_INVOICE_ITEM_DEBIT_ALL_INTERNAL_ACCOUNTS @ID,@strNTLogin


--invoiceChildren:

-- Declare @it nvarchar(50),@curs Cursor
-- set @curs = Cursor For SELECT ID FROM A_ORDER_ITEMS WHERE PARENT = @ID
-- open @curs
-- Fetch Next from @curs Into @it
-- while (@@fetch_status = 0)
-- Begin
-- 	print 'going to invoice the child item = ' + @it
-- 	exec A_SP_PURCHASE_ITEM_INVOICE_ITEM @it,@strNTLogin
-- 	Fetch Next from @curs Into @it
-- End
-- close @curs
-- Deallocate @curs


fin:

















