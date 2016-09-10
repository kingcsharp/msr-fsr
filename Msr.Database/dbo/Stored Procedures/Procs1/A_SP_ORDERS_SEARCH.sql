










CREATE         PROCEDURE A_SP_ORDERS_SEARCH
	@strWhere nvarchar(4000),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT
declare @myRootCO as nvarchar(50)
SELECT @myRootCO = ROOT_COMPANY FROM A_V_PEOPLE_DATA_QUICK
					WHERE ID = @strNTLogin

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
	FROM A_O_ORDERS WHERE 
	( 
	CREATING_CO = ''' + @myCO + ''' 
	OR 
	CUSTOMER_PERSON = ''' + @strNTLogin + ''' 
	OR
	CUSTOMER_CO IN 
		(SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE
			WHERE COMPANY = ''' + @myRootCo + ''')
	OR
	(
	SUPPLIER_ID IN 
		(SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE
			WHERE COMPANY = ''' + @myRootCo + ''')
	OR
	SUPPLIER_ID = ''' + @myRootCo + '''
	)

	) '

if len(@strWhere) > 0
		set @sql = @sql + ' AND ' + @strWhere + ' '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)
















