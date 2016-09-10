



/*
STORED PROCEDURE CALL: 

- MODULE: \asp\survey\searchSurvey.asp
- MODULE: \asp\projects\viewProject.asp
*/

CREATE                                           PROCEDURE A_SP_SURVEY_SEARCH
@fieldList varchar(4000),
@alias varchar(4000),
@strWhere varchar(4000),
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
declare @sql as varchar(4000)
declare @myCo as varchar(50)

SELECT @myCo = COMPANY 
FROM A_APPROVED_PEOPLE 
WHERE ID=@strNTLogin

if @fieldList = '' or @fieldList is NULL 
begin
set @fieldList = 'ID, 
				SUBJECT,
				INITIATOR_NAME, 
				DATE_CREATED, 
				STATUS, 
				OWNER,
				STANDARD_SEARCH, 
				CASE WHEN (NOT EXISTS (SELECT * FROM A_SURVEY_REPLIES WHERE ROOT = ID AND AUTHOR = ''' + @strNTlogin + ''' )) THEN  ''NOT REPLIED''
				     WHEN (EXISTS (SELECT * FROM A_SURVEY_REPLIES WHERE ROOT = ID AND AUTHOR = ''' + @strNTlogin + ''' )) THEN  ''REPLIED''
				ELSE ''NO VALUE'' 
				end REPLIED '
end

set @sql = 'SELECT DISTINCT ' +@fieldList + 
				'FROM A_V_SURVEY_BY_ID ' + @alias + ' 
        		WHERE (OWNER=  ''' +  @strNTLogin + ''' 
						OR (STATUS <> ''CREATING'' 
							AND (ID IN (SELECT SURVEY_ID FROM A_SURVEY_INV_PEOPLE WHERE PEOPLE_ID= ''' + @strNTLogin + ''')
							OR  ID IN (SELECT SURVEY_ID FROM A_SURVEY_REALLY_INIVTED_COMPANY WHERE COMPANY_ID= ''' + @myCo + ''')
							OR  ID IN (SELECT SURVEY_ID FROM A_SURVEY_INV_ROLE WHERE ROLE_ID IN
							(SELECT ROLE_ID FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS WHERE PERSON = ''' +  @strNTLogin +''')))))'

runSQL:
addWhere:
print 'Adding the where for all those that do not have it'
if @strWhere is not null 
	begin
	print '$$$$$$$Doing this'
	set @sql = @sql + ' AND ' +  @strWhere
	end

if @isStandardSearch = 1
	begin
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@strPurposes,'A_SURVEYS','PURPOSES','S', @sql output, @strNTLogin
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@strObjects,'A_SURVEYS','OBJECT', 'S', @sql output, @strNTLogin
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@strProjects,'A_SURVEYS','PROJECT', 'S', @sql output, @strNTLogin
	end

addSort:
if @strSort is not null
	begin
	set @sql = @sql + ' ' +  @strSort 
	end
executeSQL:
print 'SQL = ' + @SQL
EXEC(@SQL)
