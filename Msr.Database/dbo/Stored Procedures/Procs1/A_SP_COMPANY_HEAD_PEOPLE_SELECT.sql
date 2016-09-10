






CREATE     PROCEDURE A_SP_COMPANY_HEAD_PEOPLE_SELECT
@strWhere nvarchar(4000), 
@strNTLogin nvarchar(50),
@companyObjID varchar(50),
@strSort nvarchar(4000) 
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT
--Find out the Company ID
declare @companyID as varchar(50)
SELECT @companyID = ROOT FROM A_OBJECTS WHERE ID = @companyObjID
declare @sql nvarchar(500)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
	FROM A_V_APPROVED_PEOPLE_SIMPLE_SEARCH WHERE COMPANY_ID = ''' + @companyID + ''' '

if len(@strWhere) > 0
		set @sql = @sql + 'AND  ' + @strWhere + ' '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)










