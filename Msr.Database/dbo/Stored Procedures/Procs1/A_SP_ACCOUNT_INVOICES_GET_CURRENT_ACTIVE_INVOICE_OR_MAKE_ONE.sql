


CREATE    PROCEDURE dbo.A_SP_ACCOUNT_INVOICES_GET_CURRENT_ACTIVE_INVOICE_OR_MAKE_ONE
@invoiceID varchar(50) OUTPUT,
@acctID varchar(50),
@purchaseID varchar(50),
@strNTLogin varchar(50)
AS

declare @firstInvoiceDate datetime,@acctHistID varchar(50),@period varchar(50),
		@periodNumber int,@invTrigger varchar(50),@acctType varchar(50),
		@grace int,@invDate datetime

SELECT @invTrigger = INVOICE_TRIGGER FROM A_V_ACCOUNTS_APPROVED_DATA WHERE ID = @acctID

if @invTrigger <> 'ALL_PURCHASE_ITEMS'
	SELECT @invoiceID = ID FROM A_ACCOUNT_INVOICES WHERE ACCOUNT_ID = @acctID and STATUS = 'CREATING'
else
	SELECT @invoiceID = ID FROM A_ACCOUNT_INVOICES WHERE ACCOUNT_ID = @acctID and STATUS = 'CREATING' and PURCHASE_ID = @purchaseID

if @invoiceID is null
	begin
	exec sp_GetUniqueID3 @invoiceID OUTPUT
	INSERT INTO A_ACCOUNT_INVOICES (ID,ACCOUNT_ID,DRCM,MODBY,STATUS,PURCHASE_ID,INVOICE_TYPE)
		VALUES(@invoiceID,@acctID,getDate(),@strNTLogin,'CREATING',@purchaseID,@invTrigger)
	print 'There was not a current invoice so I made one ID = ' + @invoiceID
	SELECT @firstInvoiceDate = FIRST_INVOICE_DATE,@acctHistID = HISTORY_REF_ID,
		@period = INVOICE_PERIOD_TYPE,@periodNumber = INVOICE_PERIOD_NUMBER,
		@invTrigger = INVOICE_TRIGGER,@acctType = ACCT_TYPE,@grace = PAYMENT_GRACE_PERIOD
		FROM A_V_ACCOUNTS_APPROVED_DATA WHERE ID = @acctID

	if @acctType <> 'PURCHASING_ACCOUNT'
		begin
		print 'This is not a purchasing account so the first invoice date should be 1/1/2006'
		set @firstInvoiceDate = '1/1/2006'
		set @periodNumber = 1
		set @period = 'MONTH'
		set @invTrigger = 'PERIODIC'
		end
	if @invTrigger in ('ALL_PURCHASE_ITEMS','MANUAL')
		select @firstInvoiceDate = null,@period = null,@periodNumber = null


	if @invTrigger <> 'EVERY_DEBIT'
		begin
			print '$######## Not every Debit'
			exec A_SP_INVOICE_GET_DATE @invDate OUTPUT,@firstInvoiceDate,@period,@periodNumber
		end
	else
		begin
			print '$######## every Debit'
			set @invDate = getDate()
		end
	print 'The InvDate is '
	print @invDate

	declare @prevInvoiceID varchar(50),@prevBalance money
	SELECT top 1 @prevInvoiceID = ID, @prevBalance = TOTAL_DUE
		FROM A_ACCOUNT_INVOICES 
		WHERE ACCOUNT_ID = @acctID and ID <> @invoiceID
		ORDER BY INVOICE_DATE DESC 

	UPDATE A_ACCOUNT_INVOICES SET INVOICE_DATE = @invDate,
		DUE_DATE = dateAdd(dd,isNull(@grace,14),@invDate),
		TOTAL_DUE = isNull(@prevBalance,0), PREVIOUS_BALANCE = isNULL(@prevBalance,0),
		PAYMENT_AMOUNT = 0,DISPUTED_AMOUNT =0,LATE_FEES = 0
		WHERE ID = @invoiceID
	end





