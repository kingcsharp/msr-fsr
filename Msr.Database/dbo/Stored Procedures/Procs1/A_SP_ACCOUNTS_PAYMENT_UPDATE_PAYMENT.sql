




CREATE         PROCEDURE dbo.A_SP_ACCOUNTS_PAYMENT_UPDATE_PAYMENT
@newID varchar(50) OUTPUT,
@msg varchar(2000) OUTPUT,
@ID varchar(50),
@amount varchar(50),
@paymentMethod varchar(50),
@checkNum varchar(50),
@CC_NO varchar(50),
@expMo int,
@expYear int,
@acctID varchar(50),
@strNTLogin varchar(50)
AS
print 'Updating a Payment'
declare @invoiceID varchar(50)
if @ID is null
	begin
	print 'ID is Null we need to create this Payment'
	exec sp_GetUniqueID3 @ID OUTPUT
	exec A_SP_ACCOUNT_INVOICES_GET_CURRENT_ACTIVE_INVOICE_OR_MAKE_ONE
		@invoiceID OUTPUT,@acctID,@strNTLogin
	INSERT INTO A_ACCOUNT_INVOICE_ITEMS
			(ID,ITEM_TYPE,INVOICE_ID,ACCOUNT_ID,MODBY,DESCRIPTION,
			DRCM,AMOUNT,STATUS,DATE_POSTED) 
		VALUES(@ID,'INV_ITEM_PAYMENT',@invoiceID,@acctID,@strNTLogin,'Payment Received -- Thankyou',
			getDATE(),convert(money,((-1.0) * convert(float,@amount))),'CREATING',getDate())
	INSERT INTO A_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFO (ID,INVOICE_ITEM_ID,PAYMENT_METHOD,CHECK_NO,EXP_MO,EXP_YEAR,
			RECORDED_BY)
	VALUES (newID(),@ID,@paymentMethod,@checkNum,@expMo,@expYear,@strNTLogin)
	end
else
exec A_SP_ACCOUNTS_PAYMENT_ADD_BACK_ALL_MONEY @ID,@strNTLogin
UPDATE A_ACCOUNT_INVOICE_ITEMS SET
	AMOUNT = convert(money,((-1.0) * convert(float,@amount)))
	WHERE ID = @ID

UPDATE A_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFO SET
	PAYMENT_METHOD = @paymentMethod,
	CHECK_NO = @checkNum,
	CC_NO = @CC_NO,
	EXP_MO =@expMo ,
	EXP_YEAR = @expYear,
	RECORDED_BY = @strNTLogin
	WHERE INVOICE_ITEM_ID = @ID

exec A_SP_ACCOUNT_PAYMENT_FIGURE_WHERE_IT_PAYS @ID,@strNTLogin

exec A_SP_ACCOUNT_INVOICES_GET_CURRENT_ACTIVE_INVOICE_OR_MAKE_ONE
		@invoiceID OUTPUT,@acctID,@strNTLogin

exec A_SP_ACCOUNT_INVOICE_SET_TOTALS @invoiceID,@strNTLogin
set @newID = @ID

exec A_SP_ACCOUNT_INVOICE_INVOICE_IF_TIME @invoiceID,@strNTLogin

exec A_SP_ACCOUNT_UPDATE_GENERAL_ACCOUNT_DATA @acctID,@strNTLogin








