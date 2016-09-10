

CREATE   PROCEDURE dbo.A_SP_PURCHASE_ITEM_DONT_BILL
@newID varchar(200) OUTPUT,
@msgs varchar(200) OUTPUT,
@purchItemID varchar(50),
@actPartID varchar(50),
@strNTLogin varchar(50)
AS
print 'Not Billing for a purchase item ' + @purchItemID
print 'The part Number is ' + @actPartID
declare @FILL_ID varchar(50)
SELECT @FILL_ID = ID FROM A_FILLS WHERE PURCH_ITEM_ID = @purchItemID AND FILL_OBJ_ID = @actPartID
UPDATE A_FILLS SET DONT_BILL = 1 WHERE ID = @FILL_ID
declare @partName varchar(2000),@qty float
SELECT @partName = SYS_NAME,@qty = QTY FROM A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK WHERE ID = @actPartID
declare @ordQty float,@price money
SELECT @ordQty = QTY, @price = TOTAL_PRICE FROM A_ORDER_ITEMS WHERE ID = @purchItemID
declare @myMoney money
SELECT @myMoney = (@qty/@ordQty) * @price
print 'credit = '
print convert(varchar(50),@myMoney,4)
declare @invoiceID varchar(50)
SELECT @invoiceID = INVOICE_ID FROM A_ACCOUNT_INVOICE_ITEMS WHERE PURCH_ITEM_ID = @purchItemID
if @invoiceID is not null
	begin
	exec A_SP_ACCOUNT_INVOICE_EDIT_CREDIT_OR_DEBIT
	null,null,null,@invoiceID,'CREDIT',null,@myMoney,null,@partName,
	null,null,null,@strNTLogin
	end

