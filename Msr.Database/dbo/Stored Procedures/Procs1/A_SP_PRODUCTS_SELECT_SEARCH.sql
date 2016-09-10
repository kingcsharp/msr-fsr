










CREATE      PROCEDURE A_SP_PRODUCTS_SELECT_SEARCH
	@strWhere nvarchar(2000),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
declare @myWhere as nvarchar(2000)
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

--first let me see all products that I am the supplier of
set @myWhere = '' + ' ((SUPPLIER_ID = ''' + @myCO + ''') '

--next show me the ones where I am the customer of it
--set @myWhere = @myWhere + 'OR (CREATING_CO = ''' + @myCO + ''' AND STATUS LIKE ''APPROVED%'' AND SECURITY_LEVEL <= ' + @mySecurityLevel + ')' 

--let me see the ones I am creating or my company created
set @myWhere = @myWhere + 'OR (CREATING_CO = ''' + @myCO + ''')' 


set @myWhere = @myWhere + ')'

declare @sql nvarchar(4000)

set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
		FROM A_V_PRODUCTS_APPROVED_SELECT_DATA '
		if len(@strWhere) > 0
			set @sql = @sql + 'WHERE '+ @myWhere + ' AND ' + @strWhere + ' '
		if len(@strSort) > 0
			set @sql = @sql + @strSort

print @sql
exec (@sql)
















