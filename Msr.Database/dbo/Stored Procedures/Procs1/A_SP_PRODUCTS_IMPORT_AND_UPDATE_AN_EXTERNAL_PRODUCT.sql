


CREATE                 procedure dbo.A_SP_PRODUCTS_IMPORT_AND_UPDATE_AN_EXTERNAL_PRODUCT
@newID varchar(2000) OUTPUT,
@msgs varchar(2000)OUTPUT,
@EXTERNAL_PRODUCT_ID varchar(50),
@EXTERNAL_CUSTOMER_ID varchar(50),
@EXTERNAL_PART_ID varchar(50),
@EXTERNAL_PROCEDURE_ID varchar(50),
@EXTERNAL_PRODUCT_SUPPLIER_ID varchar(50),
@EXTERNAL_ACCOUNT_SUPPLIER_ID varchar(50),
@INTERNAL_CUSTOMER_ID varchar(50),
@INTERNAL_PRODUCT_SUPPLIER_ID varchar(50),
@INTERNAL_ACCOUNT_SUPPLIER_ID varchar(50),
@INTERNAL_ROLE_ID varchar(50),
@INTERNAL_PART_ID varchar(50),
@PRODUCT_NAME varchar(4000),
@PART_NAME varchar(2000),
@OEM varchar(200),
@MODEL varchar(200),
@PROCESS_AREA varchar(200),
@COPPER tinyInt,
@MM varchar(200),
@PRICE varchar(50),
@RESPONSE_TIME varchar(50),
@SALES_TAX varchar(50),
@INTERNAL_PROCEDURE_ID varchar(50),
@IS_KIT tinyint,
@KIT_ID varchar(100),
@KIT_QTY float,
@strNTLogin varchar(50)
AS
	
declare @rootCo varchar(50)
SELECT @rootCo = ROOT_COMPANY FROM A_V_PEOPLE_DATA_QUICK WHERE ID = @strNTLogin
if @INTERNAL_PRODUCT_SUPPLIER_ID IS NULL
	set @INTERNAL_PRODUCT_SUPPLIER_ID = @rootCo
if @INTERNAL_ACCOUNT_SUPPLIER_ID IS NULL
	SELECT @INTERNAL_ACCOUNT_SUPPLIER_ID = COMPANY FROM A_V_PEOPLE_DATA_QUICK WHERE ID = @strNTLogin

declare @sql varchar(4000)
set @newID ='-- Start Importing a product -- '
declare @intProdRootID varchar(50),@intProdHistID varchar(50),@intProdObjID varchar(50)
exec A_SP_Z_EXT_OBJ_GET_INTERNAL_DATA
@intProdRootID OUTPUT,@intProdHistID OUTPUT,@intProdObjID OUTPUT,
@EXTERNAL_PRODUCT_ID,'A_PRODUCTS_HISTORY',@strNTLogin
if @intProdRootID is not null
	begin
	print ' This product is already in ANSWER'
	set @newID = @newID + '-- This is a product already in ANSWER.'
	end

declare @intProcRootID varchar(50),@intProcHistID varchar(50),@intProcObjID varchar(50)
if @INTERNAL_PROCEDURE_ID is not null
	begin
	print 'Getting the Procedure using the internal ID'
	SELECT @intProcRootID = ID,@intProcHistID = HISTORY_REF_ID,@intProcObjID = OBJECT_ID FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID = @INTERNAL_PROCEDURE_ID
	end
else
	begin
	print 'Getting the procedure from an external source'
	exec A_SP_Z_EXT_OBJ_GET_INTERNAL_DATA
	@intProcRootID OUTPUT,@intProcHistID OUTPUT,@intProcObjID OUTPUT,
	@EXTERNAL_PROCEDURE_ID,'A_PROCEDURES_HISTORY',@strNTLogin
	end
if @intProcRootID is null and @EXTERNAL_PROCEDURE_ID <> 'NONE'
	begin
	set @newID = @newID + 'ERROR -- Could not find the procedure erroring out'
	print 'ERROR -- Could not find the procedure erroring out'
	goto fin
	end
set @newID = @newID + 'Procedure ID = ' + isNull(@intProcRootID,' NULL ')

print 'Setting up customer info'
declare @intCoRootID varchar(50),@intCoHistID varchar(50),@intCoObjID varchar(50)
if @INTERNAL_CUSTOMER_ID is not null
	begin
	print 'Getting the Company using the internal ID'
	SELECT @intCoRootID = ID,@intCoHistID = HISTORY_REF_ID,@intCoObjID = OBJECT_ID FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @INTERNAL_CUSTOMER_ID
	end
else
	begin
	print 'Getting the Company from the external reference'
	exec A_SP_Z_EXT_OBJ_GET_INTERNAL_DATA
		@intCoRootID OUTPUT,@intCoHistID OUTPUT,@intCoObjID OUTPUT,
		@EXTERNAL_CUSTOMER_ID,'A_COMPANIES_HISTORY',@strNTLogin
	end
if @intCoRootID is null
	begin
	set @newID = 'ERROR - Unknown Customer'
	print 'ERROR - Unknown Customer'
	goto fin
	end
declare @custName varchar(50)
SELECT @custName = NAME FROM A_V_COMPANIES_APPROVED_DATA_QUICK WHERE ID = @intCoRootID
set @newID = @newID + ' -- Customer Name = ' + @custName + ' -- '

 
print 'Getting the part or making the part'
declare @intPartRootID varchar(50),@intPartHistID varchar(50),@intPartObjID varchar(50)

if @INTERNAL_PART_ID is not null
	begin
	SELECT @intPartHistID = OBJ_ID,@intPartObjID = ID,@intPartRootID = ROOT FROM A_OBJECTS WHERE ROOT = @INTERNAL_PART_ID 
			AND STATUS LIKE 'APPROVED%'
	end
else
	begin
	exec A_SP_Z_EXT_OBJ_GET_INTERNAL_DATA
		@intPartRootID OUTPUT,@intPartHistID OUTPUT,@intPartObjID OUTPUT,
		@EXTERNAL_PART_ID,'A_PARTS_HISTORY',@strNTLogin
	if @intPartRootID is null
		begin
		set @newID = @newID + ' -- Creating Part -- '
		print 'Creating the part'
		if @PART_NAME is NULL
			begin
			set @newID = 'ERROR - Needed to create the part and the name was not here'
			print 'ERROR - Needed to create the part and the name was not here'
			goto fin
			end
		declare @partMessages varchar(2000)
		exec A_SP_PARTS_IMPORT_AND_UPDATE_AN_EXTERNAL_PART
			@partMessages OUTPUT,
			@intPartObjID OUTPUT,
			@EXTERNAL_PART_ID,@PART_NAME,@strNTLogin
		SELECT @intPartHistID = OBJ_ID, @intPartRootID = ROOT
			FROM A_OBJECTS WHERE ID =  @intPartObjID
		set @newID = @newID + @partMessages
		end
	end


partDone:
set @newID = @newID + ' -- Part ROOT ID = ' + isNull(@intPartRootID,'NULL')




declare @intProductSupplierRootID varchar(50),@intProductSupplierHistID varchar(50),@intProductSupplierObjID varchar(50)
exec A_SP_Z_EXT_OBJ_GET_INTERNAL_DATA
@intProductSupplierRootID OUTPUT,@intProductSupplierHistID OUTPUT,@intProductSupplierObjID OUTPUT,
@EXTERNAL_PRODUCT_SUPPLIER_ID,'A_COMPANIES_HISTORY',@strNTLogin
if @intProductSupplierRootID is null
	begin
	SELECT @intProductSupplierHistID = OBJ_ID,@intProductSupplierObjID = ID,@intProductSupplierRootID = ROOT 
		FROM A_OBJECTS 
		WHERE ROOT = @INTERNAL_PRODUCT_SUPPLIER_ID AND STATUS LIKE 'APPROVED%'
	if @intProductSupplierRootID is null
		begin
		set @newID = 'ERROR - Unknown Product Supplier'
		goto fin
		end
	end

declare @prodSupNAme varchar(50)
SELECT @prodSupName = NAME FROM A_V_COMPANIES_APPROVED_DATA_QUICK WHERE ID = @intProductSupplierRootID
set @newID = @newID + '-- Product Supplier  = ' + @prodSupName + ' --'

declare @intAcctSupplierRootID varchar(50),@intAcctSupplierHistID varchar(50),@intAcctSupplierObjID varchar(50)
exec A_SP_Z_EXT_OBJ_GET_INTERNAL_DATA
@intAcctSupplierRootID OUTPUT,@intAcctSupplierHistID OUTPUT,@intAcctSupplierObjID OUTPUT,
@EXTERNAL_ACCOUNT_SUPPLIER_ID,'A_COMPANIES_HISTORY',@strNTLogin
if @intAcctSupplierRootID is null
	begin
	SELECT @intAcctSupplierHistID = OBJ_ID,@intAcctSupplierObjID = ID,@intAcctSupplierRootID = ROOT 
		FROM A_OBJECTS 
		WHERE ROOT = @INTERNAL_ACCOUNT_SUPPLIER_ID AND STATUS LIKE 'APPROVED%'
	if @intAcctSupplierRootID is null
		begin
		set @newID = 'ERROR - Unknown Account Supplier'
		goto fin
		end
	end

declare @AcctSupNAme varchar(50)
SELECT @AcctSupName = NAME FROM A_V_COMPANIES_APPROVED_DATA_QUICK WHERE ID = @intAcctSupplierRootID
set @newID = @newID + '-- Acct Supplier  = ' + @AcctSupName + ' --'

if @intProdRootID is not null
	begin
	set @newID = @newID + ' Updating an Existing Product -- '
	UPDATE A_PRODUCTS_HISTORY SET 
		NAME = @PRODUCT_NAME,
		OEM = @OEM,
		MODEL = @MODEL,
		AREA = @PROCESS_AREA,
		CU = @COPPER,
		MM = @MM
		WHERE ID IN (SELECT HISTORY_REF_ID FROM A_PRODUCTS WHERE ID = @intProdRootID)
	print @newID
	end
else
	begin
	set @newID = @newID + ' Creating a new Product '
	exec A_SP_PRODUCT_UPDATE_ONE_PRODUCT
	@intProdObjID OUTPUT,null,
	null, 				--@objID varchar(50),
	null, 				--@PARENT_ID varchar(50),
	@intProductSupplierRootID, 	--SupplierID varchar(50),
	@PRODUCT_NAME, 		--NAME nvarchar(200),
	null,				--@COMMENTS nvarchar(2000),
	@intProcRootID, 		--@PROCEDURE_ID varchar(50),
	@intPartRootID,		--@APP_OBJECT varchar(50),
	0,					--@CUSTOMIZABLE varchar(50),
	null,				--@REQ_FORM varchar(8000),
	null,				--@MGR_TEAM varchar(50),
	@SALES_TAX,			--@SALES_TAX varchar(50),
	null,				--@OBJ_USED_ON varchar(8000),
	@INTERNAL_ROLE_ID,	--@MGR_ROLE varchar(50),
	@OEM,
	@MODEL,
	@PROCESS_AREA,
	@COPPER,
	@MM,
	@strNTLogin	

	set @newID = @newID + ' Created the new product with the ID = ' + @intProdObjID
	exec A_SP_OBJECTS_QUICK_APPROVE @intProdObjID,@strNTLogin
	exec A_SP_PRODUCTS_FINISH_WF @intProdHistID,@intProdObjID,@strNTLogin

	if @EXTERNAL_PRODUCT_ID is not null
		begin
		exec A_SP_OBJECT_ADD_EXTERNAL_REFERENCE @intProdHistID,@intProdObjID,'A_PRODUCTS_HISTORY',@EXTERNAL_PRODUCT_ID,@strNTLogin
		end

	end
 
SELECT @intProdHistID = ID FROM A_PRODUCTS_HISTORY WHERE OBJECT_ID = @intProdObjID
INSERT INTO A_PRODUCTS_QUICK_PRICE 
	(
	ID,
	PROD_HIST_ID,
	CUST_ID,
	PRICE,
	DRCM,
	MODBY,
	CREATE_PRICE_LIST,
	PROD_TIME,
	PROD_TIME_UNIT,
	CAPACITY,CAPACITY_UNITS,
	IS_KIT,
	KIT_ID,
	KIT_QTY
	) 
	VALUES 
	(
	newID(),
	@intProdHistID,
	@intCoRootID,
	@PRICE,
	getDate(),
	@strNTLogin,
	1,
	@RESPONSE_TIME,
	'TIME_SYS_DAYS',
	null,null,
	@IS_KIT,
	@KIT_ID,
	@KIT_QTY
	)
print ' INSERTED THE QUICK PRICE'
print 'exec A_SP_PRODUCTS_AUTO_CREATE_PRICE_LIST_AND_PURCHASE_AGREEMENT ''' + @intProdObjID + ''',''' + @strNTLogin + ''''
set @sql = 'exec A_SP_PRODUCTS_AUTO_CREATE_PRICE_LIST_AND_PURCHASE_AGREEMENT ''' + @intProdObjID + ''',''' + @strNTLogin + ''''
exec(@sql)
--exec A_SP_ADMIN_SQL_TO_RUN_QUE_UP @SQL,@strNTLogin


print 'Time to find or make the Account'

declare @intAcctRootID varchar(50),@intAcctHistID varchar(50),@intAcctObjID varchar(50)
SELECT @intAcctRootID = ID FROM A_V_ACCOUNTS_APPROVED_DATA WHERE
	ACCT_TYPE = 'PURCHASING_ACCOUNT' AND CUSTOMER_CO = @intCoRootID AND SUPPLIER_CO = @intAcctSupplierRootID
	AND INVOICE_TRIGGER = 'ALL_PURCHASE_ITEMS'

if @intAcctRootID is NULL
	begin 
	print 'We gots to make the account'
	declare @acctNewName varchar(500),@openDate datetime
	set @openDate = getDate()
	set @acctNewName = isnull(@custName,'') + '(Customer) General Purchasing Account with ' + isNull(@AcctSupName,'') + '(Supplier) (use for single POs)'
	exec A_SP_ACCOUNTS_UPDATE_ACCOUNT
	@intAcctObjID OUTPUT,null,
	null, --@objID nvarchar(50),
	null, --@ID nvarchar(50),
	@acctNewName, -- nvarchar(2000),
	null, --@REFERENCE_PO nvarchar(200),
	null, --@REFERENCE_NAME nvarchar(200),
	null, --@REFERENCE_FILES varchar(8000),
	@openDate, -- nvarchar(50),
	null, --@CLOSE_DATE nvarchar(50),
	@intAcctSupplierRootID, -- nvarchar(50),
	@intCoRootID, --@CUSTOMER_CO nvarchar(50),
	null, --@CUSTOMER_BILL_CO nvarchar(50),
	null, --@OBJECT_APPLIES_TO varchar(8000),
	null, --@MAXIMUM_USES nvarchar(50),
	null, --@TOTAL_PURCHASE_LIMIT nvarchar(50),
	null, --@CREDIT_LIMIT nvarchar(50),
	null, --@APPROVAL_WF nvarchar(50),
	'ALL_PURCHASE_ITEMS', --@INVOICE_TRIGGER nvarchar(50),
	'MONTHS', --@INVOICE_PERIOD_TYPE nvarchar(50),
	'1', --@INVOICE_PERIOD_NUMBER nvarchar(50),
	null, --@FIRST_INVOICE_DATE nvarchar(50),
	'30', --@PAYMENT_GRACE_PERIOD nvarchar(50),
	null, --@LATE_FEE_PERCENTAGE nvarchar(50),
	null, --@REAPPLY_LATE_FEE nvarchar(50),
	'PURCHASING_ACCOUNT', --@ACCT_TYPE varchar(50),
	'1', --@LABOR_INCLUDED smallInt,
	null, --@LABOR_EXCEPTIONS varchar(8000),
	'1', --@CONSUMABLES_INCLUDED smallInt,
	null, --@CONSUMABLE_EXCEPTIONS varchar(8000),
	1, --@NONCONSUMABLE_INCLUDED smallInt,
	null, --@NONCONSUMABLE_EXCEPTIONS varchar(8000),
	null, --@BILLING_EMAIL varchar(500),
	@SALES_TAX, --@TAX_RATE float,
	@strNTLogin --varchar(50)
	set @newID = @newID + ' Created the new Account with the ID = ' + @intAcctObjID
	SELECT @intAcctRootID = @intAcctObjID, @intAcctHistID = ID FROM A_ACCOUNTS_HISTORY WHERE OBJECT_ID = @intAcctObjID
	exec A_SP_OBJECTS_QUICK_APPROVE @intAcctObjID,@strNTLogin
	exec A_SP_ACCOUNTS_FINISH_WF @intAcctHistID,@intAcctObjID,@strNTLogin
	end
else
	begin
	print 'There was already an Account for this combination'
	set @newID = @newID + ' -- There was already an Account for this combination'
	end





fin:

















