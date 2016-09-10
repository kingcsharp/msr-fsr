








CREATE     PROCEDURE dbo.A_SP_PROD_REQ_FORMS_SEARCH
	@strWhere nvarchar(2000),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
declare @myWhere as nvarchar(2000)
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

--first let me see all products that I am the supplier of
--set @myWhere = '' + ' ((SUPPLIER_ID = ''' + @myCO + ''') '

--next show me the ones where I am the customer of it
--set @myWhere = @myWhere + 'OR (CREATING_CO = ''' + @myCO + ''' AND STATUS LIKE ''APPROVED%'' AND SECURITY_LEVEL <= ' + @mySecurityLevel + ')' 

--let me see the ones I am creating or my company created
--set @myWhere = @myWhere + 'OR (CREATING_CO = ''' + @myCO + ''')' 

--set @myWhere = @myWhere + ')'

declare @sql nvarchar(4000)

set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
		FROM A_V_PROD_REQUEST_FORMS_WITH_ALL_DATA '
		if len(@strWhere) > 0
			set @sql = @sql + 'WHERE ('+ @strWhere + ') AND 
	(
		(
		SUPPLIER_ID = ''' + @myCo + ''' OR 
		SUPPLIER_ID IN (SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @myCo + ''')
		)
		or
		(
		CUSTOMER_CO = ''' + @myCo + ''' OR 
		CUSTOMER_CO IN (SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @myCo + ''')
		)
		
	)
		'
			
		if len(@strSort) > 0
			set @sql = @sql + @strSort

print @sql
exec (@sql)














