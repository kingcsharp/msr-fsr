





CREATE      PROCEDURE dbo.A_SP_ACCOUNT_INVOICE_SHOW_ITEMS
@strWhere nvarchar(4000),
@strSort nvarchar(1000),
@invoiceID varchar(50),
@strNTLogin nvarchar(50)
AS

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT * 
FROM A_V_ACCOUNT_INVOICE_ITEMS_ALL_DATA WHERE INVOICE_ID = ''' + @invoiceID + ''' '

if len(@strWhere) > 0
		set @sql = @sql + ' AND (' + @strWhere + ') '

if len(@strSort) > 0
		set @sql = @sql + @strSort
print @sql
exec(@sql)






