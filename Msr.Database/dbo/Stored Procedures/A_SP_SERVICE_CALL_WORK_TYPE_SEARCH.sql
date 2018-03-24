



/*
STORED PROCEDURE CALLED IN serviceCalls/searchServiceCalls.asp
*/
CREATE       PROCEDURE A_SP_SERVICE_CALL_WORK_TYPE_SEARCH
@strWhere nvarchar(4000),
@strSort nvarchar(1000),
@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCo as varchar(50)
SELECT @myCo = COMPANY FROM A_APPROVED_PEOPLE WHERE ID=@strNTLogin
declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
	FROM A_V_SERVICE_CALL_WORK_TYPE_DATA WHERE 
	(CUSTOMER_ID =''' + @myCo + ''' OR SUPPLIER_ID = ''' + @myCo + ''') '  

if len(@strWhere) > 0
		set @sql = @sql + ' AND ' + @strWhere + ' '


if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)