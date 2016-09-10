


/*

STORED PROCEDURE CALL: 

- MODULE: AnswerAlpha\asp\people\searchPeople.asp
*/

CREATE             PROCEDURE A_SP_PEOPLE_SEARCH
@strWHERE varchar(4000),
@strSort varchar(500),
@searchType nvarchar(50),
@SEARCH_ID nvarchar(200),
@strNTLogin nvarchar(50)
as
--build the sql for the query
declare @sql as nvarchar(4000)

--Get my company
declare @myCo as nvarchar(50)
SELECT @myCo = ROOT_COMPANY 
FROM A_APPROVED_PEOPLE 
WHERE ID=@strNTLogin

set @sql = 'SELECT * FROM A_V_PEOPLE_OBJECT_SEARCH WHERE '
--Add some security here
set @sql = @sql + '(
				(STATUS LIKE ''APPROVED%'') OR
				(LOCKED_BY = ''' + @strNTLogin + ''') OR
				((CREATED_BY = ''' + @strNTLogin + ''') OR (CREATED_BY IN (SELECT SUBORDINATE FROM A_PEOPLE_SUB_LOOKUP_TABLE WHERE BOSS = ''' + @strNTLogin + '''))) OR
				(CREATING_CO = ''' + @myCO + ''' AND 
					(STATUS LIKE ''APPROVED%'' OR STATUS = ''OLD'')

				)
				
				)'

set @sql = @sql + 'AND ' + @strWhere + ' '

if len(@strSort) > 0
	begin
	  set @sql = @sql + @strSort
	  print 'SQL = ' + @sql
	end

EXEC(@SQL)





