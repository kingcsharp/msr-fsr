/*
ACCOUNT_ID
LAST_INVOICE_DATE
LAST_INVOICE_AMOUNT
PAYMENTS_SINCE_INVOICE
PAYMENT_DUE_DATE
CURRENT_BALANCE
*/


CREATE PROCEDURE dbo.A_SP_ACCOUNT_GET_LATEST_INVOICE_DATA
@acctID varchar(50),
@modby varchar(50)
AS
declare 
@lastInvoiceDate datetime,
@lastInvoiceAmt money,
@paymentDueDate datetime,
@paymentsSinceInvoice money,
@currentBalance money

print 'Getting the latest invoice data for account # ' + @acctID
SELECT top 1 @lastInvoiceDate = INVOICE_DATE,
	@lastInvoiceAmt = TOTAL_DUE,
	@paymentDueDate = DUE_DATE
	FROM A_ACCOUNT_INVOICES WHERE ACCOUNT_ID = @acctID AND STATUS = 'INVOICED' ORDER BY INVOICE_DATE DESC

SELECT @paymentsSinceInvoice = PAYMENT_AMOUNT,
	@currentBalance = TOTAL_DUE
	FROM A_ACCOUNT_INVOICES WHERE ACCOUNT_ID = @acctID AND STATUS = 'CREATING'

SELECT @lastInvoiceDate AS LAST_INVOICE_DATE,@lastInvoiceAmt AS LAST_INVOICE_AMOUNT,@paymentDueDate AS  PAYMENT_DUE_DATE,
	@paymentsSinceInvoice AS PAYMENTS_SINCE_INVOICE,@currentBalance AS CURRENT_BALANCE,
	@acctID AS ACCOUNT_ID

