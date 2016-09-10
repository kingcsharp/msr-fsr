









CREATE            PROCEDURE dbo.A_SP_PRODUCTS_SHOPPING_SEARCH
	@strWhere nvarchar(2000),
	@strCustomer varchar(50),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
print 'Searching for products where you are the customer'
declare @myWhere as nvarchar(2000)
--find out my Company
declare @myCO as nvarchar(50)
SELECT @myCO = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
print 'Your company number is ' + isNull(@myCO,'NULL')
if @strCustomer = @myCo
	begin
	--next show me the ones where I am the customer of it
	set @myWhere = '' + ' 
	( 
		CUSTOMER_ID = ''' + @myCO + ''' OR 
		CUSTOMER_ID IN 
			(SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @myCO + ''')
	) 
	AND (FOR_INDIVIDUAL_SALE is null OR FOR_INDIVIDUAL_SALE = 1)'
	end
	else
	begin
	if @strCustomer is not null
		begin
		--next show me the ones where I am the customer of it
		set @myWhere = '' + ' 
		( 
			CUSTOMER_ID = ''' + @strCustomer + ''' OR 
			CUSTOMER_ID IN 
				(SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @strCustomer + ''')
		) AND '
		end

	set @myWhere = isnull(@myWhere,'') + '
	(FOR_INDIVIDUAL_SALE is null OR FOR_INDIVIDUAL_SALE = 1) AND 
	( 
		SUPPLIER_ID = ''' + @myCO + ''' OR 
		SUPPLIER_ID IN 
			(SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @myCO + ''')
	) 
	'
	end


declare @sql nvarchar(4000)

set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT 
		DISTINCT 
		CUSTOMER_ID,CUST_NAME,OBJ_ID,PRICE_LIST_ID,ID,SUP_ROOT_CO_NAME,SUPPLIER_ID,
		SUPPLIER_NAME,APP_OBJ_PART_DESC,PROC_NAME,UNIT_PRICE,[UNIT],PRODUCT_NAME,CUSTOMIZABLE,REQ_FORM,
		OEM,AREA,MM,MODEL,CU,PRODUCTION_TIME,PRODUCTION_TIME_UNIT
		FROM A_V_PRODUCTS_FOR_PURCHASING_DATA '
		if len(@strWhere) > 0
			set @sql = @sql + 'WHERE '+ @myWhere + ' AND ' + @strWhere + ' '
		if len(@strSort) > 0
			set @sql = @sql + @strSort

print @sql
exec (@sql)


























