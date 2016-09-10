













CREATE           PROCEDURE dbo.A_SP_PROPOSALS_SEARCH
	@strWhere nvarchar(4000),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
SELECT @myCO = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTlogin
declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
	FROM A_O_PROPOSALS WHERE
	(
		(
		CUSTOMER_CO = ''' + @myCO + ''' OR  
		CUSTOMER_CO 
		IN (SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @myCo + ''')
		)
		OR
		(
		SUPPLIER_CO = ''' + @myCO + ''' OR  
		SUPPLIER_CO 
		IN (SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @myCo + ''')
		)
		
	)


'

if len(@strWhere) > 0
		set @sql = @sql + ' AND ' + @strWhere + ' '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)



















