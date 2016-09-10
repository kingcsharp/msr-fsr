
CREATE              PROCEDURE [dbo].[A_SP_PURCHASE_ITEM_INVOICE_ITEM_TO_ACCOUNT] 
@ID varchar(50),
@acctID varchar(50),
@strNTLogin varchar(50)
AS

declare @price money,@invoiceID varchar(50),@nextInvoiceDate datetime,@firstInvoiceDate datetime,
	@acctHistID varchar(50),@period varchar(50),@periodNumber int,@invDate dateTime,
	@invTrigger varchar(50),@purchHistID varchar(50),@pplID varchar(50),@pplBillType varchar(50),
	@intBillIT tinyInt,@myBillType varchar(10),@acctType varchar(50),@grace int,@purchaseID varchar(50),
	@purchaser varchar(50),@custCo varchar(50),@supplierCo varchar(50),@orderHistID varchar(50),
	@orderID varchar(50),@quoteID varchar(50),@exCostID varchar(50),@acctName varchar(2000),
	@custSinglePONum varchar(50), @custLineItem varchar(50)

print 'In [A_SP_PURCHASE_ITEM_INVOICE_ITEM_TO_ACCOUNT]'

if @acctID is null 
	begin
	print 'The account ID is null so this does not work'
	goto fin
	end

SELECT @acctName = NAME,@acctType = ACCT_TYPE FROM A_V_ACCOUNTS_APPROVED_DATA WHERE ID = @acctID
Print '------------The account we are debitting id ' + isnull(@acctName,'NONAM')

SELECT @price = TOTAL_PRICE - isNull((SELECT sum(TOTAL_PRICE) FROM A_ORDER_ITEMS WHERE PARENT = @ID AND PROC_SYS_ID = 'SYS_SHIPPING'),0)
,@purchHistID = PURCHASE_HIST_ID,@pplID = PROD_PRICE_LIST, 
@myBillType = BILL_TYPE,@supplierCO = SUPPLIER_ID,@orderHistID = ORDER_ID,@quoteID = QUOTE_ID,
@exCostID = ADD_COST_ID,@custLineItem = CUST_LINE_ITEM
FROM A_V_ORDER_ITEMS_ALL_DATA WHERE ID = @ID

SELECT @custSinglePONum = CUST_PURCH_NUM FROM A_PURCHASES_HISTORY WHERE ID = @purchHistID

if @exCostID is null and @acctType = 'PURCHASING_ACCOUNT' 
	begin
		print 'Failed 1'
		goto fin
	end
if @exCostID is not null and @acctType <> 'PURCHASING_ACCOUNT' goto fin
SELECT @orderID = ORDER_ID FROM A_PURCHASES_HISTORY WHERE ID = @purchHistID
SELECT @custCo = CUSTOMER_CO,@purchaser = CUSTOMER_PERSON FROM A_V_ORDERS_APPROVED_DATA WHERE ID = @orderID
SELECT @purchaseID = ROOT FROM A_OBJECTS WHERE OBJ_ID = @purchHistID
if exists(SELECT * FROM A_ACCOUNT_INVOICE_ITEMS 
		WHERE PURCH_ITEM_ID = @ID AND ACCOUNT_ID = @acctID AND STATUS <> 'DISPUTED')
	begin
	print 'This item is already currently active on an invoice.'
	goto fin
	end 	
if @acctID is null 
	begin
	print 'The account ID is null so this does not work'
	goto fin
	end

exec A_SP_ACCOUNT_INVOICES_GET_CURRENT_ACTIVE_INVOICE_OR_MAKE_ONE @invoiceID output,@acctID,@purchaseID,@strNTLogin

declare @invItemID varchar(50)
exec sp_GetUniqueID3 @invItemID OUTPUT

INSERT INTO A_ACCOUNT_INVOICE_ITEMS (ID,INVOICE_ID,ACCOUNT_ID,PURCH_ITEM_ID,STATUS,DRCM,MODBY,
	AMOUNT,DATE_POSTED,PURCHASE_ID,ITEM_TYPE,CUSTOMER_CO,SUPPLIER_CO,PURCHASER_ID,QUOTE_ID,
	CUST_SINGLE_PO,CUST_LINE_ITEM
	)
	VALUES (@invItemID,@invoiceID,@acctID,@ID,'CURRENT',getDate(),@strNTLogin,
	@price,getDate(),@purchaseID,'INV_ITEM_DEBIT',@custCo,@supplierCo,@purchaser,@quoteID,
	@custSinglePONum,@custLineItem
	)

declare @invItemName nvarchar(2000)
exec dbo.A_SP_ACCOUNT_INVOICE_ITEM_CREATE_NAME @invItemName OUTPUT,@invItemID
UPDATE A_ACCOUNT_INVOICE_ITEMS SET DESCRIPTION = @invITemName WHERE ID = @invItemID
exec A_SP_ACCOUNT_INVOICE_SET_TOTALS @invoiceID,@strNTLogin
exec A_SP_ACCOUNT_UPDATE_ACCOUNT_AND_FORECASTS_WITH_INVOICE_ITEM_SUMS @acctID
exec A_SP_ACCOUNT_INVOICE_INVOICE_IF_TIME @invoiceID,@strNTLogin

fin:













