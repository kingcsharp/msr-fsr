








CREATE         PROCEDURE A_SP_ACCOUNTS_UPDATE_ACCOUNT
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@objID nvarchar(50),
@ID nvarchar(50),
@NAME nvarchar(2000),
@REFERENCE_PO nvarchar(200),
@REFERENCE_NAME nvarchar(200),
@REFERENCE_FILES varchar(8000),
@OPEN_DATE nvarchar(50),
@CLOSE_DATE nvarchar(50),
@SUPPLIER_CO nvarchar(50),
@CUSTOMER_CO nvarchar(50),
@CUSTOMER_BILL_CO nvarchar(50),
@OBJECT_APPLIES_TO varchar(8000),
@MAXIMUM_USES nvarchar(50),
@TOTAL_PURCHASE_LIMIT nvarchar(50),
@CREDIT_LIMIT nvarchar(50),
@APPROVAL_WF nvarchar(50),
@INVOICE_TRIGGER nvarchar(50),
@INVOICE_PERIOD_TYPE nvarchar(50),
@INVOICE_PERIOD_NUMBER nvarchar(50),
@FIRST_INVOICE_DATE nvarchar(50),
@PAYMENT_GRACE_PERIOD nvarchar(50),
@LATE_FEE_PERCENTAGE nvarchar(50),
@REAPPLY_LATE_FEE nvarchar(50),
@ACCT_TYPE varchar(50),
@LABOR_INCLUDED smallInt,
@LABOR_EXCEPTIONS varchar(8000),
@CONSUMABLES_INCLUDED smallInt,
@CONSUMABLE_EXCEPTIONS varchar(8000),
@NONCONSUMABLE_INCLUDED smallInt,
@NONCONSUMABLE_EXCEPTIONS varchar(8000),
@BILLING_EMAIL varchar(500),
@TAX_RATE float,
@strNTLogin varchar(50)
AS
print 'Updating an Account'
if @objID is null
	begin
		print 'ID is Null we need to create this Account'
		exec sp_GetUniqueID3 @newID OUTPUT
		set @ID = @newID
		INSERT INTO A_ACCOUNTS_HISTORY(ID,NAME,MODBY,DRCM) VALUES(@newID,@NAME,@strNTLogin,getDATE())
		SELECT @newID = OBJECT_ID FROM A_ACCOUNTS_HISTORY WHERE ID = @newID
	end
else
	begin
		print 'The ID is not null so we are just updating Account objectID='+@objID
		set @newID = @objID
	end


print'Updating the Account history Data'
UPDATE A_ACCOUNTS_HISTORY SET
NAME = @NAME,
REFERENCE_PO = @REFERENCE_PO,
REFERENCE_NAME = @REFERENCE_NAME,
OPEN_DATE = @OPEN_DATE,
CLOSE_DATE = @CLOSE_DATE,
SUPPLIER_CO = @SUPPLIER_CO,
CUSTOMER_CO = @CUSTOMER_CO,
CUSTOMER_BILL_CO = @CUSTOMER_BILL_CO,
MAXIMUM_USES = @MAXIMUM_USES,
TOTAL_PURCHASE_LIMIT = convert(money,@TOTAL_PURCHASE_LIMIT),
CREDIT_LIMIT = convert(money,@CREDIT_LIMIT),
APPROVAL_WF = @APPROVAL_WF,
INVOICE_TRIGGER = @INVOICE_TRIGGER,
INVOICE_PERIOD_TYPE = @INVOICE_PERIOD_TYPE,
INVOICE_PERIOD_NUMBER = @INVOICE_PERIOD_NUMBER,
FIRST_INVOICE_DATE = @FIRST_INVOICE_DATE,
PAYMENT_GRACE_PERIOD = @PAYMENT_GRACE_PERIOD,
LATE_FEE_PERCENTAGE = @LATE_FEE_PERCENTAGE,
REAPPLY_LATE_FEE = @REAPPLY_LATE_FEE,
ACCT_TYPE = @ACCT_TYPE,
LABOR_INCLUDED = @LABOR_INCLUDED,
CONSUMABLES_INCLUDED = @CONSUMABLES_INCLUDED,
NONCONSUMABLE_INCLUDED = @NONCONSUMABLE_INCLUDED,
BILLING_EMAIL = @BILLING_EMAIL,
TAX_RATE = @TAX_RATE
WHERE OBJECT_ID = @newID

declare @actID varchar(50)
SELECT @actID = ROOT FROM A_OBJECTS WHERE ID = @newID
exec A_SP_ACCOUNT_FIGURE_TOTALS @actID,@strNTLogin

print 'Updating reference File List for the Account'
print 'First delete all the ones we used to have'
DELETE FROM A_DOCUMENT_LINK WHERE OBJECT_ID = @newID
print 'making a cursor to go through the ref files string'
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @REFERENCE_FILES,', '
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Reference File = ' + @it
	exec A_SP_FILES_CREATE_LINK @newID,@it,null,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs


print 'Delete all objects this account is related to'
DELETE FROM A_ACCOUNTS_RELATED_OBJECTS WHERE ACCOUNT_ID = @ID
print 'Now add the new applicable objects'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @OBJECT_APPLIES_TO,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Object that Account Applies to = ' + @it
	INSERT INTO A_ACCOUNTS_RELATED_OBJECTS (ID,ACCOUNT_ID,OBJ_ID,DRCM,MODBY)
	VALUES (newID(),@ID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Delete all Labor Exceptions'
DELETE FROM A_ACCOUNT_PRODUCT_EXCEPTIONS WHERE ACCT_ID = @ID AND EX_TYPE = 'LABOR'
print 'Now add the new exceptions'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @LABOR_EXCEPTIONS,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Labor Exception = ' + @it
	INSERT INTO A_ACCOUNT_PRODUCT_EXCEPTIONS (ID,ACCT_ID,PRODUCT_ID,EX_TYPE,DRCM,MODBY)
	VALUES (newID(),@ID,ltrim(@it),'LABOR',getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs


print 'Delete all Consumable Part Exceptions'
DELETE FROM A_ACCOUNTS_PART_EXCEPTIONS WHERE ACCT_ID = @ID AND EX_TYPE = 'CONSUMABLE'
print 'Now add the new exceptions'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @CONSUMABLE_EXCEPTIONS,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding CONSUMABLE Exception = ' + @it
	INSERT INTO A_ACCOUNTS_PART_EXCEPTIONS (ID,ACCT_ID,PART_ID,EX_TYPE,DRCM,MODBY)
	VALUES (newID(),@ID,ltrim(@it),'CONSUMABLE',getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Delete all NONConsumable Part Exceptions'
DELETE FROM A_ACCOUNTS_PART_EXCEPTIONS WHERE ACCT_ID = @ID AND EX_TYPE = 'NONCONSUMABLE'
print 'Now add the new exceptions'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @NONCONSUMABLE_EXCEPTIONS,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding NONCOSUMABLE Exception = ' + @it
	INSERT INTO A_ACCOUNTS_PART_EXCEPTIONS (ID,ACCT_ID,PART_ID,EX_TYPE,DRCM,MODBY)
	VALUES (newID(),@ID,ltrim(@it),'NONCONSUMABLE',getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs










