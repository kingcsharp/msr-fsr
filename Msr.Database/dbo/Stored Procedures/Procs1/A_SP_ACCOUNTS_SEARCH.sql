













CREATE            PROCEDURE A_SP_ACCOUNTS_SEARCH
	@strWhere nvarchar(4000),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *,TOTAL_PURCHASE_LIMIT-TOTAL_DEBITS AS UNUSED_AMOUNT
	FROM A_O_ACCOUNTS WHERE
	(
	(CREATING_CO = ''' + @myCO + ''' AND 
		(
		ACCT_TYPE IN (''PURCHASING_ACCOUNT'',''WARRANTY_ACCOUNT'')
		OR
		ROOT IN (SELECT ACCOUNT_ID FROM A_ACCOUNT_INVOICES)
		)
	)
	OR
	(
		(
		CUSTOMER_CO = ''' + @myCO + ''' OR
		CUSTOMER_CO 
		IN (SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE WHERE COMPANY = ''' + @myCo + ''')
		)
		AND ACCT_TYPE = ''PURCHASING_ACCOUNT'' 
		
	)
	)
'

if len(@strWhere) > 0
		set @sql = @sql + ' AND ' + @strWhere + ' '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)



















