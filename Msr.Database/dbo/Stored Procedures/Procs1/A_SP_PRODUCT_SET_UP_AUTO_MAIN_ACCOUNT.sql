


CREATE  PROCEDURE A_SP_PRODUCT_SET_UP_AUTO_MAIN_ACCOUNT
@PROD_ID as varchar(50),
@strNTLogin as varchar(50)
AS
print 'Creating the Main Auto Account for this Product'

declare @newID as varchar(50),
		@SUPPLIER_CO as varchar(50),
		@PRODUCT as varchar(50)

SELECT 
@SUPPLIER_CO = SUPPLIER_ID,
@PRODUCT = ID
FROM A_APPROVED_PRODUCTS WHERE ID = @PROD_ID

if @PRODUCT is null
	begin
	print ' Could not find this product'
	goto fin
	end

exec A_SP_ACCOUNTS_AUTO_CREATE_PRODUCT_ACCOUNT @newID OUTPUT,
@SUPPLIER_CO,NULL,@PRODUCT,'PRODUCT_ACCOUNT',@strNTLogin	


fin:


