


CREATE              PROCEDURE A_SP_FILES_SEARCH 
@strWhere varchar(1000),
@strSetOrder varchar(100),
@strNTLogin varchar(50)
as
declare @sql as nvarchar(4000)

set @sql='SELECT DISTINCT ID,SORT_ID, NAME, DESCRIPTION, SRC_NAME, SRC_ID
		FROM A_V_FILES_SEARCH_BY_SUBORDINATE 
		WHERE (BOSS=' + @strNTLogin + ' OR CREATOR_ID = ' + @strNTLogin + ') '
		if @strWhere <> ''
			begin
			set @sql = @sql + ' AND (' + @strWhere + ')'
			end
	
		if @strSetOrder <> ''
			begin
			set @sql = @sql + ' ' + @strSetOrder + ''
			end
	

print @sql

EXEC(@SQL)








