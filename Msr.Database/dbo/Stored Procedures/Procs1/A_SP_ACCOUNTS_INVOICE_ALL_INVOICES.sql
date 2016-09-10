



CREATE   PROCEDURE dbo.A_SP_ACCOUNTS_INVOICE_ALL_INVOICES
AS
Declare @ID varchar(50)
Declare @curs Cursor
set @curs = 
		Cursor For SELECT DISTINCT INVOICE_ID FROM A_ACCOUNT_INVOICE_ITEMS
open @curs
Fetch Next from @curs Into @ID
while (@@fetch_status = 0)
Begin
	exec A_SP_ACCOUNT_INVOICE_INVOICE_IF_TIME @ID,'7'
	Fetch Next from @curs Into @ID
End
close @curs
Deallocate @curs






