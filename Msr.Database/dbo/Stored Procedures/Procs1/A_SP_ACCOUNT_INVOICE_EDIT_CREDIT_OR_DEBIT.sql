







CREATE      PROCEDURE dbo.A_SP_ACCOUNT_INVOICE_EDIT_CREDIT_OR_DEBIT
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@ACCOUNT_ID varchar(50),
@INVOICE_ID varchar(50),
@TYPE varchar(50),
@ID varchar(50),
@UNIT_PRICE varchar(50),
@QTY float,
@COMMENT varchar(2000),
@DESCRIPTION varchar(1000),
@DATE_POSTED datetime,
@CUST_SINGLE_PO varchar(50),
@CUST_LINE_ITEM varchar(50),
@TAX_RATE float,
@strNTLogin varchar(50)
AS
declare @AMOUNT money
set @AMOUNT = convert(money,@UNIT_PRICE * @QTY)
if @ACCOUNT_ID IS NULL
SELECT @ACCOUNT_ID = ACCOUNT_ID FROM A_ACCOUNT_INVOICES WHERE ID = @INVOICE_ID
print 'Updating a Credit or Debit'
declare @custID varchar(50),@supplierID varchar(50)
SELECT @custID = CUSTOMER_CO,@supplierID = SUPPLIER_CO
	 FROM A_V_ACCOUNTS_APPROVED_DATA WHERE ID = @ACCOUNT_ID

if @ID is null
	begin
		print 'ID is Null we need to create this invoice item'
		exec sp_GetUniqueID3 @newID OUTPUT
		set @ID = @newID
		INSERT INTO A_ACCOUNT_INVOICE_ITEMS (ID,INVOICE_ID,MODBY,
			DRCM,STATUS,ITEM_TYPE,DATE_POSTED,ACCOUNT_ID) 
		VALUES(@newID,@INVOICE_ID,@strNTLogin,
			getDATE(),'CURRENT',@TYPE,getDate(),@ACCOUNT_ID)
	end
else
	begin
		print 'The ID is not null so we are just updating Invoice item ID='+@ID
		set @newID = @ID
	end

if @TYPE = 'INV_ITEM_CREDIT'
	set @amount = @amount * -1
	
print'Updating the Invoice Item Data'
UPDATE A_ACCOUNT_INVOICE_ITEMS SET
DESCRIPTION = @DESCRIPTION,
COMMENTS = @COMMENT,
AMOUNT = convert(money,@AMOUNT),
UNIT_PRICE = convert(money,@UNIT_PRICE),
QTY = convert(float,@QTY),
DRCM = getDate(),
DATE_POSTED = @DATE_POSTED,
CUST_SINGLE_PO = @CUST_SINGLE_PO,
CUST_LINE_ITEM = @CUST_LINE_ITEM,
TAX_RATE = @TAX_RATE,
TAX = ((@TAX_RATE / 100) * @AMOUNT), 
MODBY = @strNTLogin
WHERE ID = @ID

declare @purchItemID varchar(50)
SELECT @purchItemID = PURCH_ITEM_ID from A_ACCOUNT_INVOICE_ITEMS WHERE ID = @ID
if @purchItemID is not null
	begin
	UPDATE A_ORDER_ITEMS SET CUST_LINE_ITEM = @CUST_LINE_ITEM WHERE ID = @purchItemID OR PARENT = @purchItemID
	declare @purchaseHistID varchar(50)
	SELECT @purchaseHistID = PURCHASE_HIST_ID FROM A_ORDER_ITEMS WHERE ID = @purchItemID
	UPDATE A_PURCHASES_HISTORY SET CUST_PURCH_NUM = @CUST_SINGLE_PO WHERE ID = @purchaseHistID
	end

print 'retotalling the invoice now'
exec A_SP_ACCOUNT_INVOICE_SET_TOTALS @INVOICE_ID,@strNTLogin
exec A_SP_ACCOUNT_FIGURE_TOTALS @ACCOUNT_ID,@strNTLogin





