















/*

STORED PROCEDURE CALL: 

- MODULE: \asp\prodPriceList\searchPriceList.asp

*/

CREATE     PROCEDURE A_SP_PROD_PRICE_LIST_SEARCH
@strWhere nvarchar(4000),
@strSort nvarchar(2000),
@showCustomers nvarchar(50),
@strNTLogin nvarchar(50)
AS
declare @strFieldList varchar(4000)
set @strFieldList = ' DISTINCT ID,FOR_INDIVIDUAL_SALE,
ROOT,CREATING_CO,LOCKED_BY,UNLOCKED_BY,CREATED_BY,PRODUCT_NAME,
CREATE_DATE,REV_INFO,STATUS,REV,LOCKED_BY_NAME,CREATING_CO_NAME,PRODUCT,UNIT,MIN_QUANTITY,
UNIT_PRICE,INVOICE_FROM,OBJECT_ID,OBJ_ID,OBJ_TABLE,DRCM,
SUPPLIER_ID,SUPPLIER_NAME,dbo.A_FN_COMPANY_GET_PATH_NAME_FROM_APPROVED_ID(SUPPLIER_ID) AS SUPPLIER_FULL_NAME '

if @showCustomers = 'TRUE' set @strFieldList = @strFieldList + ',CUST_ID,CUSTOMER '

--build the sql for the query
declare @sql as nvarchar(4000)
declare @myCo as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCo OUTPUT
print 'my company=' +@myCo
set @sql = 'SELECT ' + @strFieldList + ' FROM A_O_PROD_PRICE_LISTS WHERE CREATING_CO = ''' + @myCo + ''' AND '
+ @strWhere +  ' ' + @strSort
print 'my sql=' +@sql
EXEC(@SQL)















