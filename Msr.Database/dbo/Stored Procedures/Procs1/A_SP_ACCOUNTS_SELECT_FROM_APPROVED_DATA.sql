




CREATE             PROCEDURE A_SP_ACCOUNTS_SELECT_FROM_APPROVED_DATA
	@strWhere nvarchar(4000),
	@strSort nvarchar(1000),
	@strPurchItemID varchar(50),
	@strForecastObjID varchar(50),
	@purchaseID varchar(50),
	@quoteID varchar(50),
	@strNTLogin nvarchar(50)
AS
declare @supplierID varchar(50), @myCO as nvarchar(50), @sql nvarchar(4000)
SELECT @myCO = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
if @strPurchItemID is not null
	begin
	--find out who the supplier compnay is
	SELECT @supplierID = SUPPLIER_ID FROM A_V_ORDER_ITEMS_ALL_DATA WHERE ID = @strPurchItemID
	end
if @strForecastObjID is not null
	begin
	--find out who the supplier compnay is
	SELECT @supplierID = CO FROM A_FORECASTS_HISTORY WHERE OBJECT_ID = @strForecastObjID
	end

if @quoteID is NOT NULL
	begin
	SELECT @supplierID = SUPPLIER_ID FROM A_V_QUOTES_APPROVED_DATA WHERE HISTORY_REF_ID = @quoteID
	end

--find out my Company

print 'Got the company tree'
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *,dbo.A_FN_COMPANY_GET_PATH_NAME_FROM_APPROVED_ID(SUPPLIER_CO) AS SUP_PATH_NAME
	FROM A_V_ACCOUNTS_APPROVED_DATA WHERE ID is not null '

if @supplierID is NULL
	set @sql = @sql + 'AND  (CREATING_CO = ''' + @myCO + '''  OR CUSTOMER_CO = ''' + @myCo + ''') '
if @strPurchItemID is not null
	set @sql = @sql +  'AND  (CUSTOMER_CO = ''' + @myCo + ''' OR CUSTOMER_CO in (SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @myCO + ''') ) '

if @quoteID is not null
	set @sql = @sql +  'AND  (CUSTOMER_CO = ''' + @myCo + ''' OR CUSTOMER_CO in (SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @myCO + ''') ) '
	
if @supplierID is not null
	begin
	set @sql = @sql + ' AND (SUPPLIER_CO = ''' + isNull(@supplierID,'NULL') + ''' '
	set @sql = @sql + ' OR SUPPLIER_CO in (SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @supplierID + ''') )'
	end

if len(@strWhere) > 0
		set @sql = @sql + ' AND ' + @strWhere + ' '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)





