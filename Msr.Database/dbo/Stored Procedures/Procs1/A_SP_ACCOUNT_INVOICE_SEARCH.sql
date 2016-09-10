




CREATE     PROCEDURE dbo.A_SP_ACCOUNT_INVOICE_SEARCH
@strWhere nvarchar(4000),
@strSort nvarchar(1000),
@acctID varchar(50),
@strNTLogin nvarchar(50)
AS

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT * 
FROM A_V_INVOICES WHERE ACCOUNT_ID = ''' + @acctID + ''' '
if len(@strWhere) > 0
		set @sql = @sql + ' AND (' + @strWhere + ') '

if len(@strSort) > 0
		set @sql = @sql + @strSort
print @sql

exec(@sql)





