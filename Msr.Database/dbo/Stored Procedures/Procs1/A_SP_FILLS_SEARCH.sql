





CREATE       PROCEDURE DBO.A_SP_FILLS_SEARCH
@strWhere nvarchar(4000),
@strSort nvarchar(50),
@strNTLogin nvarchar(50)
AS

--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT * FROM A_V_FILLS_SEARCH '

set @sql = @sql + 'WHERE ('
print 'adding that i can see where i am the customer and fill by is customer'
set @sql = @sql + '(CUSTOMER = ''' + @myCo + ''' AND FILL_BY = ''CUSTOMER'') '
print 'adding that i can see where i am the supplier and fill by is SUPPLIER'
set @sql = @sql + ' OR (SUPPLIER = ''' + @myCo + ''' AND FILL_BY = ''SUPPLIER'') '
print 'adding that i can see where fill by is SUPPLIER and supplier is one of my child companies'
set @sql = @sql + ' OR (SUPPLIER IN (SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @myCo + ''') AND FILL_BY = ''SUPPLIER'') '
if len(@strWhere) > 0
		set @sql = @sql + ') AND ' + @strWhere + ' '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)







