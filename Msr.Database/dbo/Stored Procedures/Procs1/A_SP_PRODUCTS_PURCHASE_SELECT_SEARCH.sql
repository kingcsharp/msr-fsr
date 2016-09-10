





CREATE        PROCEDURE dbo.A_SP_PRODUCTS_PURCHASE_SELECT_SEARCH
	@strWhere nvarchar(2000),
	@objID varchar(50),
	@sysType varchar(50),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
declare @strType varchar(50), @strID varchar(50), @myCO as nvarchar(50)

SELECT @strID = OBJ_ID, @strType = OBJ_TABLE FROM A_OBJECTS WHERE ID = @objID

if @strType = 'A_ORDERS_HISTORY' SELECT @myCO = CUSTOMER_CO FROM A_ORDERS_HISTORY WHERE ID = @strID
else SELECT @myCO = CUSTOMER_CO FROM A_QUOTES_HISTORY WHERE ID = @strID

print 'Searching for products where you are the customer'
declare @myWhere as nvarchar(2000)
print 'ok'
--find out my Company
print 'Your customer number is ' + isNull(@myCO,'NULL')
--next show me the ones where I am the customer of it
set @myWhere = '' + ' (CUSTOMER_ID = ''' + @myCO + ''') '
if @sysType is not null
	set @myWhere = @myWhere + ' AND PROC_SYS_ID = ''' + @sysType + ''' '
else
	set @myWhere = @myWhere + ' AND (FOR_INDIVIDUAL_SALE is null OR FOR_INDIVIDUAL_SALE = 1) '
declare @sql nvarchar(4000)

set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
		FROM A_V_PRODUCTS_FOR_PURCHASING_DATA '
		if len(@strWhere) > 0
			set @sql = @sql + 'WHERE '+ @myWhere + ' AND ' + @strWhere + ' '
		if len(@strSort) > 0
			set @sql = @sql + @strSort

print @sql
exec (@sql)






















