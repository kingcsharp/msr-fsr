







CREATE  PROCEDURE dbo.A_SP_QUOTE_ORDER_LINK_SHOW_NEW_ORDERS_SEARCH
	@strWhere nvarchar(4000),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
	FROM A_V_QUOTE_ORDER_LINK_WITH_COMPANY_NAMES 
	WHERE STATUS = ''NEW'' AND 
	ROLE_ID IN (SELECT ROLE_ID FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS WHERE PERSON = ''' + @strNTLogin + ''')
	'

if len(@strWhere) > 0
		set @sql = @sql + ' AND ' + @strWhere + ' '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)













