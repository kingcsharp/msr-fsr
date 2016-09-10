

CREATE      PROCEDURE A_SP_PARTS_SEARCH_APPROVED_DATA
	@strWhere nvarchar(1000),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT
declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *,OBJECT_ID AS OBJ_ID
	FROM A_V_PART_DATA_BY_APPROVED_DATA WHERE STATUS LIKE ''APPROVED%'' AND CREATING_CO = ''' + @myCO + ''' '
if len(@strWhere) > 0
		set @sql = @sql + ' AND ' + @strWhere + ' '
if len(@strSort) > 0
		set @sql = @sql + @strSort
print @sql
exec (@sql)




