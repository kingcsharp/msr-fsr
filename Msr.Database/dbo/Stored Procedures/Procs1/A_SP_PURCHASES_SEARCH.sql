





CREATE       PROCEDURE DBO.A_SP_PURCHASES_SEARCH
@strWhere nvarchar(4000),
@strSort nvarchar(50),
@strNTLogin nvarchar(50)
AS

--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT DISTINCT
ID,DATE_CREATED,PURCHASE_STATUS,ORDER_ID,DRCM,MODBY,OBJECT_ID,
LOCKED_BY,UNLOCKED_BY,CREATED_BY,CREATE_DATE,ROOT,CREATING_CO,
STATUS,LOCKED_BY_NAME,CREATING_CO_NAME,
CUSTOMER_PERSON,CUSTOMER_CO,DESCRIPTION,OBJECT_ID as OBJ_ID,
CUST_PURCH_NUM,SUP_PURCH_NUM
FROM A_O_PURCHASES WHERE ID IS NOT NULL AND 

(
CREATING_CO = ''' + @myCO + '''  OR 
ID IN 
	(
	SELECT DISTINCT PURCHASE_HIST_ID FROM A_V_PURCHASE_ITEMS_WITH_SUPPLIER_ID WHERE 
		(
		SUPPLIER_ID IN 
			(
			SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY =  
			''' + @myCo + '''
			)
		OR
		SUPPLIER_ID  =  ''' + @myCo + '''
		)
	)
)' 

if len(@strWhere) > 0
		set @sql = @sql + ' AND ' + @strWhere + ' '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)







