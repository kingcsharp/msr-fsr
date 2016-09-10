
CREATE      PROCEDURE dbo.A_SP_PRODUCTS_SELECT_SEARCH_FOR_QUOTE
	@strWhere nvarchar(2000),
	@quoteID varchar(50),
	@sysType varchar(50),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
print 'Searching for products where you are the customer'
declare @myWhere as nvarchar(2000)
print 'ok'
--find out my Company
declare @myCO as varchar(50), @supplierID varchar(50)
SELECT @myCO = CUSTOMER_CO,@supplierID = SUPPLIER_ID FROM A_QUOTES_HISTORY WHERE OBJECT_ID = @quoteID
print 'Your customer number is ' + isNull(@myCO,'NULL')
--next show me the ones where I am the customer of it
set @myWhere = '' + ' (CUSTOMER_ID = ''' + @myCO + ''' AND SUPPLIER_ID = ''' + @supplierID + ''') '
if @sysType is not null
	set @myWhere = @myWhere + ' AND PROC_SYS_ID = ''' + @sysType + ''' '
declare @sql nvarchar(4000)

set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
		FROM A_V_PRODUCTS_FOR_PURCHASING_DATA '
		if len(@strWhere) > 0
			set @sql = @sql + 'WHERE '+ @myWhere + ' AND ' + @strWhere + ' '
		if len(@strSort) > 0
			set @sql = @sql + @strSort

print @sql
exec (@sql)




















