




CREATE     PROCEDURE A_SP_ACCOUNTS_AUTO_CREATE_PRODUCT_ACCOUNT
@returnID varchar(50) OUTPUT,
@SUPPLIER_CO varchar(50),
@CUSTOMER_CO varchar(50),
@PROD_ID varchar(50),
@acctType varchar(50),
@strNTLogin varchar(50)
AS

declare @newID as varchar(50)
exec sp_GetUniqueID3 @newID OUTPUT
print 'We got a New ID of ' + @newID

declare @parent_Account as varchar(50),
		@supplierName as nvarchar(4000),
		@customerName as nvarchar(4000),
		@prodName as nvarchar(4000),
		@acctName as nvarchar(4000)


SELECT @supplierName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @SUPPLIER_CO
SELECT @customerName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @CUSTOMER_CO
SELECT @prodName = NAME FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @PROD_ID

print 'We are going to add or find an ' + @acctType + ' which has a supplier of ' + isNull(@supplierName,'No Supplier') + 
' and a customer of ' + isNull(@customerName,'No Customer') + ' and a product of ' + isNull(@prodName,'Product Name')

if @acctType = 'CUSTOMER_ACCOUNT'
	begin
	print 'This is a customer Account'	
	declare @Parent_Customer_ID as varchar(50)
	SELECT @Parent_Customer_ID = PARENT FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @CUSTOMER_CO
	IF @Parent_Customer_ID is not null
		begin
		print 'This customer has a parent Company = ' + @PArent_Customer_ID + ' so we need to find its account from this supplier and use that for the parent'
		SELECT @parent_Account = ID FROM A_V_ACCOUNTS_APPROVED_DATA WHERE 
			SUPPLIER_CO = @SUPPLIER_CO AND CUSTOMER_CO = @Parent_Customer_ID AND ACCT_TYPE = 'CUSTOMER_ACCOUNT'
		if @parent_Account is NULL
			begin
			print 'This PArent Account was not made yet so we need to Add it to get a parent'
			exec A_SP_ACCOUNTS_AUTO_CREATE_PRODUCT_ACCOUNT @parent_Account OUTPUT,
			@SUPPLIER_CO,@Parent_Customer_ID,NULL,'CUSTOMER_ACCOUNT',@strNTLogin
			end
		end
	set @acctName = @supplierName + ' sales to ' + @customerName
	print 'We set the name of this Account to ' + isNull(@acctName,'NULL')
	end
if @acctType = 'PRODUCT_ACCOUNT'
	begin
	print 'Okay so we have a Product Account'
	declare @Parent_Product_ID as varchar(50)
	SELECT @Parent_Product_ID = PARENT_ID FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @PROD_ID
	IF @Parent_Product_ID is not null and  @CUSTOMER_CO is not null
		begin
		print 'This Account has a parent with a product of ' + @Parent_Product_ID + ' and customer of ' + @customerName
		SELECT @parent_Account = ID FROM A_V_ACCOUNTS_APPROVED_DATA WHERE 
			SUPPLIER_CO = @SUPPLIER_CO AND PRODUCT_ID = @Parent_Product_ID AND ACCT_TYPE = 'PRODUCT_ACCOUNT' AND CUSTOMER_CO = @CUSTOMER_CO
		if @parent_Account is NULL
			begin
			print 'We Looked for the parent Account and it was not there, so we need to make it.'
			exec A_SP_ACCOUNTS_AUTO_CREATE_PRODUCT_ACCOUNT @parent_Account OUTPUT,
			@SUPPLIER_CO,@CUSTOMER_CO,@Parent_Product_ID,'PRODUCT_ACCOUNT',@strNTLogin
			end
		end
	IF @Parent_Product_ID is not null and @CUSTOMER_CO is null
		begin
		print 'This Account has a parent with a product of ' + @Parent_Product_ID + ' and customer is null '
		SELECT @parent_Account = ID FROM A_V_ACCOUNTS_APPROVED_DATA WHERE 
			SUPPLIER_CO = @SUPPLIER_CO AND PRODUCT_ID = @Parent_Product_ID AND ACCT_TYPE = 'PRODUCT_ACCOUNT' AND CUSTOMER_CO is Null
		if @parent_Account is NULL
			begin
			print 'We Looked for the parent Account and it was not there, so we need to make it.'
			exec A_SP_ACCOUNTS_AUTO_CREATE_PRODUCT_ACCOUNT @parent_Account OUTPUT,
			@SUPPLIER_CO,@CUSTOMER_CO,@Parent_Product_ID,'PRODUCT_ACCOUNT',@strNTLogin
			end
		end
	if @parent_Account is null and @CUSTOMER_CO is not null
		begin
		print 'This account does not have a parent yet, but it has a customer and product, so we need to set its parent to a product account with the null customer'
		SELECT @parent_Account = ID FROM A_V_ACCOUNTS_APPROVED_DATA WHERE 
			SUPPLIER_CO = @SUPPLIER_CO AND PRODUCT_ID = @PROD_ID AND ACCT_TYPE = 'PRODUCT_ACCOUNT' AND CUSTOMER_CO is Null
		if @parent_Account is NULL
			begin
			print 'We should have got a parent account, so we need to make it.'
			exec A_SP_ACCOUNTS_AUTO_CREATE_PRODUCT_ACCOUNT @parent_Account OUTPUT,
			@SUPPLIER_CO,NULL,@PROD_ID,'PRODUCT_ACCOUNT',@strNTLogin
			end
		end
	set @acctName = @supplierName + ' sales of ' + @prodName + isNull(' to ' + @customerName,'')
	print 'We are going to make an account with a name of ' + @acctName
	end

declare @tester varchar(50)
if @CUSTOMER_CO is NULL
	begin
	SELECT @tester = ID FROM A_ACCOUNTS_HISTORY WHERE
	SUPPLIER_CO = @SUPPLIER_CO and CUSTOMER_CO IS NULL AND PRODUCT_ID = @PROD_ID
	end
else
	begin
	SELECT @tester = ID FROM A_ACCOUNTS_HISTORY WHERE
	SUPPLIER_CO = @SUPPLIER_CO and CUSTOMER_CO = @CUSTOMER_CO AND PRODUCT_ID = @PROD_ID
	end

if @tester is null
	begin
	INSERT INTO A_ACCOUNTS_HISTORY
	(ID, NAME,OPEN_DATE, SUPPLIER_CO,CUSTOMER_CO, DRCM, MODBY, ACCT_TYPE, ACCT_STATUS, PRODUCT_ID,PARENT_ACCOUNT)
	VALUES
	(@newID, @acctName, getDate(),@SUPPLIER_CO,@CUSTOMER_CO, getDate(), @strNTLogin, @acctType, 'ACTIVE',@PROD_ID,@parent_Account)
	declare @objID as varchar(50)
	SELECT @objID = OBJECT_ID FROM A_ACCOUNTS_HISTORY WHERE ID = @newID
	UPDATE A_OBJECTS SET STATUS = 'APPROVED', LOCKED_BY = NULL WHERE ID = @objID
	print 'Finishing the Account workflow'
	exec A_SP_ACCOUNTS_FINISH_WF @newID,@newID,@strNTLogin
	print 'Approved it '
	set @returnID = @objID
	end	
else
	begin
	print 'This account exists so we need to update its name'
	UPDATE A_ACCOUNTS_HISTORY SET NAME = @acctName, PARENT_ACCOUNT=@parent_Account WHERE ID = @tester
	end




