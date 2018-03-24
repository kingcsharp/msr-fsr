


CREATE               PROCEDURE A_SP_PROJECT_SEARCH
@fieldList varchar(4000),
@alias varchar(4000),
@strWhere varchar(3000),
@strPurposes varchar(4000),
@strObjects varchar(4000),
@strProjects varchar(4000),
@strSort varchar(1000),
@strNTLogin varchar(50)
AS
declare @isStandardSearch int
set @isStandardSearch = 0
if @fieldList is null
	begin
		print 'The field list is so null so we know this a standard search from searchDiscussions.asp'
		set @isStandardSearch =1
	end


--build the sql for the query
declare @sql as varchar(8000)
declare @myCo as varchar(50)
SELECT @myCo = COMPANY 
FROM A_APPROVED_PEOPLE 
WHERE ID=@strNTLogin


if @fieldList = '' or @fieldList is null 
begin
set @fieldList = 'ID, 
				NAME, 
				convert(varchar(20),DATE_CREATED,101) AS DATE_CREATED,
				convert(varchar(20),ORIGINAL_PLANNED_STOP_DATE,101)AS ORIGINAL_PLANNED_STOP_DATE,
				convert(varchar(20),CURRENT_PLANNED_STOP_DATE,101) AS CURRENT_PLANNED_STOP_DATE,
				convert(varchar(20),ACTUAL_STOP_DATE,101) AS ACTUAL_STOP_DATE,
				PRIORITY,
				CURRENT_LEADER_NAME,
				SUMMARY, 
				STATUS,
				INITIATOR,
				LEADER,
				CASE WHEN (s.STATUS = ''CLOSED'') THEN ''2''
				WHEN (s.STATUS = ''OPEN'') THEN ''1''
				ELSE ''3''
				end STANDARD_SORT,
				CASE WHEN (LEADER = ''' + @strNTlogin +''' or MEMBER_ID = + ''' + @strNTlogin + ''') THEN ''1''
				ELSE ''0''
				end IS_MEMBER '
end
set @sql = 'SELECT DISTINCT ' +@fieldList + ' ' + 
'FROM A_V_PROJECT_SEARCH_ALL_ALLOWED ' + isNull(@alias,'') + '
WHERE 
(
INITIATOR= ''' + @strNTLogin + '''' +
'OR SPONSOR_ID= ''' + @strNTLogin + '''' +
'OR LEADER= ''' + @strNTLogin + '''' +
'OR MEMBER_ID= ''' + @strNTLogin + '''' +
'OR INV_PEOPLE_ID= ''' + @strNTLogin + '''' +
'OR COMPANY_ID = ''' + @myCO + '''' +
'OR ROLE_ID IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = ''' + @strNTLogin +''')
) '
set @sql = @sql + ' AND ' +  @strWhere

if @isStandardSearch = 1 
	begin
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@strPurposes,'A_PROJECTS','PURPOSES','S', @sql output, @strNTLogin
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@strObjects,'A_PROJECTS','OBJECT', 'S', @sql output, @strNTLogin
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@strProjects,'A_PROJECTS','PROJECT', 'S', @sql output, @strNTLogin
	end 


if @strSort is not null
	begin
	set @sql = @sql + ' ' +  @strSort 
	end

print 'SQL = ' + @SQL
EXEC(@SQL)