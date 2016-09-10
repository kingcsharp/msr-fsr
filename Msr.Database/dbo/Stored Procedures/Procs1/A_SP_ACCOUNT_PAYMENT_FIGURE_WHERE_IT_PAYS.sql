
CREATE  procedure dbo.A_SP_ACCOUNT_PAYMENT_FIGURE_WHERE_IT_PAYS
@paymentID varchar(50),
@strNTLogin varchar(50)
AS
--Get the account that we are dealing with
declare @acctID varchar(50)
SELECT @acctID = ACCOUNT_ID FROM A_ACCOUNT_INVOICE_ITEMS WHERE ID = @paymentID
print 'The Account ID = ' + @acctID
--subtract this payment from all the invoices that it was applied to
Declare @ID varchar(50),@invID varchar(50),@AMOUNT money
Declare @curs Cursor
set @curs = 
		Cursor For SELECT ID,INVOICE_ID,AMOUNT FROM A_ACCOUNT_INVOICE_PAYMENT_LINK 
		WHERE PAYMENT_ID = @paymentID
open @curs
Fetch Next from @curs Into @ID,@invID,@AMOUNT
while (@@fetch_status = 0)
Begin
	print 'Subtracting from invoice = ' + @invID + ' the amount ' + convert(varchar(50),@AMOUNT)
	UPDATE A_ACCOUNT_INVOICES SET AMT_PAID = AMT_PAID + convert(money,(-1) * @AMOUNT) WHERE ID = @invID
	Fetch Next from @curs Into @ID,@invID,@AMOUNT
End
close @curs
Deallocate @curs
--delete the link of this payment to all the invoices it used to be on
DELETE FROM A_ACCOUNT_INVOICE_PAYMENT_LINK WHERE PAYMENT_ID = @paymentID
--get the amount of this payment and set up all the counters we will use to
--calculate how much money we have spent
declare @paymentAmount money,@amtSpent money,@amtLeft money,@amtToPay money,
	@amtPaid money, @newItemsAmt money, @amtPaidOnThisInvoice money
SELECT @paymentAmount = convert(money,(-1)*AMOUNT) from A_ACCOUNT_INVOICE_ITEMS where ID = @paymentID
set @amtSpent = 0
set @amtLeft = @paymentAmount

--now start with the oldest unpaid invoice and use the money as we go
set @curs = Cursor For SELECT ID,NEW_ITEMS_AMT,AMT_PAID FROM A_ACCOUNT_INVOICES 
	WHERE ACCOUNT_ID = @acctID AND isNull(AMT_PAID,0) < isNull(NEW_ITEMS_AMT,0)
open @curs
Fetch Next from @curs Into @ID,@newItemsAmt,@amtPaid
while (@@fetch_status = 0)
Begin
	set @amtToPay = isNull(@newItemsAmt,0) - isNull(@amtPaid,0)
	if @amtToPay > @amtLeft 
		set @amtToPay = @amtLeft
	print 'Paying ' + convert(varchar(50),@amtToPay)
	print 'to invoice = ' + @ID
	
--	UPDATE A_ACCOUNT_INVOICES SET AMT_PAID = isNull(AMT_PAID,0) + @amtToPay WHERE ID = @ID
	INSERT INTO A_ACCOUNT_INVOICE_PAYMENT_LINK (ID,PAYMENT_ID,INVOICE_ID,AMOUNT,DRCM,MODBY)
		VALUES(newID(),@paymentID,@ID,@amtToPay,getDate(),@strNTLogin)
	SELECT @amtPaidOnThisInvoice = sum(AMOUNT) FROM A_ACCOUNT_INVOICE_PAYMENT_LINK WHERE INVOICE_ID = @ID
	UPDATE A_ACCOUNT_INVOICES SET AMT_PAID = @amtPaidOnThisInvoice WHERE ID = @ID
	set @amtLeft = @amtLeft - @amtToPay	
	if @amtLeft <= 0 goto doneSubtracting
	Fetch Next from @curs Into @ID,@newItemsAmt,@amtPaid
End
close @curs
Deallocate @curs
doneSubtracting:
--put the amount we spent in the payment info box
UPDATE A_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFO SET AMOUNT_SPENT = @paymentAmount - @amtLeft







