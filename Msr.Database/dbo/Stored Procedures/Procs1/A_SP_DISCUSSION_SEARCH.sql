

/*

STORED PROCEDURE CALL: 

- MODULE: \asp\discussions\searchDiscussion.asp
- MODULE: \asp\projects\viewProjects.asp


*/
CREATE                        PROCEDURE A_SP_DISCUSSION_SEARCH
@fieldList varchar(4000),
@alias varchar(50),
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

SELECT @myCo = COMPANY 
FROM A_APPROVED_PEOPLE 
WHERE ID=@strNTLogin

if @fieldList = '' or @fieldList is NULL 
	begin
	set @fieldList ='ID,
					RESPONSE_ID,
					SUBJECT,
					I_ID, 
					DATE_CREATED, 
					INITIATOR_NAME,
					STATUS, 
					STANDARD_SEARCH,
					LAST_RESPONSE,
					dbo.A_FN_DISCUSSION_GET_LAST_VIEW_DATE (''' +  @strNTlogin + ''',ID) AS VIEW_DATE ' 
	end 
set @sql = 'SELECT DISTINCT ' +@fieldList + ' ' + 
			'FROM A_V_DISCUSSION_SEARCH ' + isNull(@alias,'') + '
	         WHERE (I_ID =''' +  @strNTLogin +''''+
			        'OR ID IN (SELECT DISCUSSION_ID FROM A_DISCUSSION_INV_PEOPLE WHERE PEOPLE_ID = ''' + @strNTLogin +''')
	    		    OR ID IN (SELECT DISCUSSION_ID FROM A_DISCUSSION_INV_COMPANY WHERE COMPANY_ID = ''' + @myCo +''')
 					OR ID IN (SELECT DISCUSSION_ID FROM A_DISCUSSION_INV_ROLE WHERE ROLE_ID IN (SELECT ROLE_ID FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS WHERE PERSON =''' + @strNTLogin +''')))'


print @sql

runSQL:
addWhere:
print 'Adding the where for all those that do not have it'
if @strWhere is not null
	begin
	set @sql = @sql + ' AND ' +  @strWhere
	end
addSort:

print '@fieldList is ' + isNull(@fieldList,'this is null')

if @isStandardSearch = 1 
	begin
	print 'Doing busniess purposes, objects and project searches'
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@strPurposes,'A_DISCUSSIONS','PURPOSES','S', @sql output, @strNTLogin
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@strObjects,'A_DISCUSSIONS','OBJECT', 'S', @sql output, @strNTLogin
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@strProjects,'A_DISCUSSIONS','PROJECT', 'S', @sql output, @strNTLogin
	end 

if @strSort is not null
	begin
	set @sql = @sql + ' ' +  @strSort 
	end
executeSQL:
print 'SQL = ' + @SQL
EXEC(@SQL)










