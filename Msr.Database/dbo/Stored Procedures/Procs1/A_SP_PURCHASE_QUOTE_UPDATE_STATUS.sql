





CREATE       PROCEDURE DBO.A_SP_PURCHASE_QUOTE_UPDATE_STATUS
@PID varchar(50),
@QID varchar(50),
@strNTLogin varchar(50)
AS
print 'We are updating the status for Purchase = ' + isNULL(@PID,'NULL') +
	' and Quote = ' + isNull(@QID,'NULL')

if @QID is null
	SELECT @QID = QUOTE_ID FROM A_PURCHASE_QUOTE_STATUS WHERE PURCHASE_ID = @PID

declare @tester varchar(50), @ret varchar(50)
SELECT @tester = ID FROM A_PURCHASE_QUOTE_STATUS WHERE
PURCHASE_ID = @PID AND QUOTE_ID = @QID

if @tester is null
	begin
	print 'There is no status so making one now'
	INSERT INTO A_PURCHASE_QUOTE_STATUS (ID,PURCHASE_ID,QUOTE_ID,STATUS)
		VALUES (newID(),@PID,@QID,'NEW')
	end
set @tester = NULL


declare @custCo varchar(50),@supplierCo varchar(50),@sRoot varchar(50),@cRoot varchar(50),
	@sRootName varchar(50), @cRootName varchar(50),@pRet varchar(50)
SELECT @custCo = CUSTOMER_CO FROM A_QUOTES_HISTORY WHERE ID = @QID
SELECT @supplierCo = SUPPLIER_ID FROM A_QUOTES_HISTORY WHERE ID = @QID
SELECT @sRoot = ROOT_CO FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supplierCo
SELECT @cRoot = ROOT_CO FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @custCo
SELECT @cRootName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @cRoot
SELECT @sRootName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @sRoot

print 'Customer is ' + @cRoot + ' ' + @cRootName + ' Supplier is ' + @sRoot + ' ' + @sRootName

if @cRoot = @sRoot
	begin
	print 'The Customer and supplier are the same'
 	set @ret = 'INT_ACCT_OPTIONAL'
	end
else
	begin
	SELECT @tester = ID FROM A_ORDER_ITEMS WHERE 
		PURCHASE_HIST_ID = @PID AND QUOTE_ID = @QID AND ACCOUNT_ID is NULL 
		AND ADD_COST_ID IS NOT NULL
	if @tester is not null	set @ret = 'ITEM_NEEDS_ACCOUNT'
	else
		begin
		set @ret = 'ALL_ITEMS_HAVE_ACCOUNTS'
		end
	end

UPDATE A_PURCHASE_QUOTE_STATUS SET STATUS = @ret,DRCM = getDAte(),MODBY = @strNTLogin WHERE 
PURCHASE_ID = @PID AND QUOTE_ID = @QID

set @tester = NULL
SELECT @tester = ID 
	FROM A_PURCHASE_QUOTE_STATUS 
	WHERE PURCHASE_ID = @PID AND STATUS = 'ITEM_NEEDS_ACCOUNT'

if @tester is not null	set @pRet = 'ITEM_NEEDS_ACCOUNT'
	else set @pRet = 'ALL_ITEMS_HAVE_ACCOUNTS'

UPDATE A_PURCHASES_HISTORY SET PURCHASE_STATUS = @pRet,DRCM = getDAte(),MODBY = @strNTLogin WHERE 
	ID = @PID





