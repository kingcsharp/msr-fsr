





CREATE       PROCEDURE DBO.A_SP_ACCOUNT_INVOICE_SET_TOTALS
@invoiceID varchar(50),
@strNTLogin varchar(50)
AS
UPDATE A_ACCOUNT_INVOICES 
	SET NEW_ITEMS_AMT = isNull((
			SELECT SUM(AMOUNT) 
			FROM A_ACCOUNT_INVOICE_ITEMS 
			WHERE INVOICE_ID = @invoiceID AND ITEM_TYPE='INV_ITEM_DEBIT'),0) 
	WHERE ID = @invoiceID
UPDATE A_ACCOUNT_INVOICES 
	SET PAYMENT_AMOUNT = isNull((
			SELECT SUM(AMOUNT) 
			FROM A_ACCOUNT_INVOICE_ITEMS 
			WHERE INVOICE_ID = @invoiceID 
					AND ITEM_TYPE in ('INV_ITEM_PAYMENT','INV_ITEM_CREDIT')),0) 
	WHERE ID = @invoiceID
UPDATE A_ACCOUNT_INVOICES 
	SET TOTAL_TAX = isNull((
			SELECT SUM(TAX) 
			FROM A_ACCOUNT_INVOICE_ITEMS 
			WHERE INVOICE_ID = @invoiceID AND ITEM_TYPE='INV_ITEM_DEBIT'),0) 
	WHERE ID = @invoiceID


UPDATE A_ACCOUNT_INVOICES 
	SET TOTAL_DUE = isNull(PREVIOUS_BALANCE,0) + isNull(PAYMENT_AMOUNT,0) + isNull(LATE_FEES,0) + isNull(NEW_ITEMS_AMT,0) + isNull(TOTAL_TAX,0)
	WHERE ID = @invoiceID
UPDATE A_ACCOUNT_INVOICES SET INVOICE_BALANCE = isNull(NEW_ITEMS_AMT,0) + isNull(PAYMENT_AMOUNT,0) +  + isNull(TOTAL_TAX,0)
	 WHERE ID = @invoiceID

declare @curs as CURSOR,@it varchar(50),@poNums varchar(1000)
set @curs = cursor for SELECT DISTINCT CUST_SINGLE_PO FROM A_ACCOUNT_INVOICE_ITEMS WHERE INVOICE_ID = @invoiceID AND CUST_SINGLE_PO is not null
open @curs
fetch next from @curs into @it
while @@fetch_status = 0
	begin
	set @poNums = isNull(@poNums + ', ','') + @it
	fetch next from @curs into @it
	end
close @curs
deallocate @curs
print 'PO Nums = ' + isnull(@poNums,'NULL')
UPDATE A_ACCOUNT_INVOICES SET PO_NUMBER = @poNums WHERE ID = @invoiceID


declare @acctID varchar(50)
SELECT @acctID = ACCOUNT_ID FROM A_ACCOUNT_INVOICES WHERE ID = @invoiceID
exec A_SP_ACCOUNT_FIGURE_TOTALS @acctID,@strNTLogin
print 'updating balance'





