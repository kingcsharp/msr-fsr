


CREATE  PROCEDURE dbo.A_SP_ACCOUNT_INVOICES_MANUALLY_DELETE_ONE
@result varchar(50) OUTPUT,
@messages varchar(2000) OUTPUT,
@invoiceID varchar(50),
@strNTLogin varchar(50)
AS

if exists (SELECT * FROM A_ACCOUNT_INVOICE_ITEMS WHERE INVOICE_ID = @invoiceID)
	begin
	set @result = 'Cannot delete an invoice with invoice items'
	goto fin
	end
DELETE FROM A_ACCOUNT_INVOICES WHERE ID = @invoiceID
fin:




