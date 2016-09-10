







CREATE      PROCEDURE A_SP_EQUIP_EXP_SEARCH
	@strWhere nvarchar(4000),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
	FROM A_O_EQUIP_EXP '

if len(@strWhere) > 0
	begin
	  set @sql = @sql + ' WHERE ' + @strWhere + ' '
	end
--We need to add some standard security in here
set @sql = @sql + 'AND ('
--Creator can see 
set @sql = @sql + '(CREATED_BY = ''' + @strNTLogin + ''')'
--Person its about can see
set @sql = @sql + ' OR (PERSON_ID = ''' + @strNTLogin + ''')'
-- Person its about bosses can see
set @sql = @sql + ' OR (PERSON_ID IN (SELECT SUBORDINATE FROM A_PEOPLE_SUB_LOOKUP_TABLE WHERE BOSS = ''' + @strNTLogin + '''))'
--end the Security 
set @sql = @sql + ')'

if len(@strSort) > 0
	begin
	  set @sql = @sql + @strSort
	  print 'SQL = ' + @sql
	end

print 'SQL = ' + @SQL
EXEC(@SQL)













