CREATE  PROCEDURE dbo.A_SP_ACCOUNTS_PAYMENT_ADD_BACK_ALL_MONEY
@paymentID varchar(50),
@strNTLogin varchar(50)
AS
print 'A_SP_ACCOUNTS_PAYMENT_ADD_BACK_ALL_MONEY'
declare @curs as CURSOR,@it as varchar(50),@amt as money
set @curs = Cursor For SELECT INVOICE_ID,AMOUNT FROM A_ACCOUNT_INVOICE_PAYMENT_LINK WHERE PAYMENT_ID = @paymentID
open @curs
Fetch Next from @curs Into @it,@amt
while (@@fetch_status = 0)
Begin
	UPDATE A_ACCOUNT_INVOICES SET AMT_PAID = AMT_PAID - @amt WHERE ID = @it
	Fetch Next from @curs Into @it,@amt
End
close @curs
Deallocate @curs









