





CREATE         procedure dbo.A_SP_ACCOUNT_INVOICE_INVOICE_IF_TIME
@ID varchar(50),
@strNTLogin varchar(50)
AS
if not exists(SELECT * FROM A_ACCOUNT_INVOICES WHERE ID = @ID AND STATUS = 'CREATING')
	goto fin
print 'is it time to invoice invoice # ' + @ID
declare @acctID varchar(50),@invTrigger varchar(50),@invNow tinyint,
@invDate datetime,@purchaseID varchar(50),@purchaseHistID varchar(50),
@invoiceType varchar(50)
SELECT @invoiceType = INVOICE_TYPE FROM A_ACCOUNT_INVOICES WHERE ID = @ID
if @invoiceType = 'MANUAL' 
	goto fin
SELECT @acctID = ACCOUNT_ID, @invDate = INVOICE_DATE FROM A_ACCOUNT_INVOICES WHERE ID = @ID
SELECT @invTrigger = INVOICE_TRIGGER  FROM A_V_ACCOUNTS_APPROVED_DATA 
	WHERE ID = @acctID
set @invNow = 0
if @invTrigger = 'EVERY_DEBIT' 
	set @invNow = 1
if @invTrigger = 'PERIODIC' and getDate() > @invDate
	set @invNow = 1
if @invTrigger = 'ALL_PURCHASE_ITEMS'
	begin
	SELECT @purchaseID = PURCHASE_ID FROM A_ACCOUNT_INVOICES WHERE ID = @ID
	SELECT @purchaseHistID = HISTORY_REF_ID FROM A_PURCHASES WHERE ID = @purchaseID
	if not exists (SELECT * FROM A_TASK_ORDER_INFORMATION o WHERE FILL_ITEM_ID IS NOT NULL and o.PURCHASE_HIST_ID = @purchaseHistID and not exists(SELECT ID FROM A_ACCOUNT_INVOICE_ITEMS WHERE FILL_ID = o.FILL_ITEM_ID))
		set @invNow = 1
	else
		print 'It is not time yet'
	end
if @invNow = 0
	goto fin

if @invDate is null
	begin
	declare @dueDate datetime
	SELECT @invDate = max(ACTUAL_STOP_DATE) FROM A_V_INVOICE_TASK_STOP_DATES WHERE INVOICE_ID = @ID
	SELECT @dueDate = dateAdd(d,PAYMENT_GRACE_PERIOD,@invDate) FROM A_V_ACCOUNTS_APPROVED_DATA WHERE ID = @acctID
	UPDATE A_ACCOUNT_INVOICES set INVOICE_DATE = @invDate,DUE_DATE = @dueDate where ID = @ID
	end
	
UPDATE A_ACCOUNT_INVOICES set STATUS = 'INVOICED' where ID = @ID
UPDATE A_ACCOUNT_INVOICE_ITEMS SET STATUS = 'INVOICED', DATE_INVOICED = @invDate WHERE INVOICE_ID = @ID



exec A_SP_ACCOUNT_FIGURE_TOTALS @acctID,@strNTLogin

fin:






