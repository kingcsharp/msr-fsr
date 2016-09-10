





CREATE  PROCEDURE dbo.A_SP_ACCOUNTS_DELETE_PAYMENT
@paymentID varchar(50),
@strNTLogin varchar(50)
AS
declare @invID varchar(50)
SELECT @invID = INVOICE_ID FROM A_ACCOUNT_INVOICE_ITEMS WHERE ID = @paymentID
print 'Deleting a Payment'
print 'Adding back all amts paid to invoices we used to be deducted from'
exec A_SP_ACCOUNTS_PAYMENT_ADD_BACK_ALL_MONEY @paymentID,@strNTLogin
DELETE FROM A_ACCOUNT_INVOICE_PAYMENT_LINK WHERE PAYMENT_ID = @paymentID
DELETE FROM A_ACCOUNT_INVOICE_ITEMS WHERE ID = @paymentID
exec A_SP_ACCOUNT_INVOICE_SET_TOTALS @invID,@strNTLogin
exec A_SP_ACCOUNT_INVOICE_SET_TOTALS @invID,@strNTLogin











