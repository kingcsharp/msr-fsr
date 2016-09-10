









/*
STORED PROCEDURE CALL: 
- MODULE: \asp\meeting\searchMeeting.asp
- MODULE: \asp\projects\viewProjects.asp
*/
CREATE                                   PROCEDURE A_SP_MEETING_SEARCH
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

declare @boolDebug as int
set @boolDebug = 0
--build the sql for the query
declare @sql as varchar(4000)
--Get my Company
declare @myCo as varchar(50)
SELECT @myCo = COMPANY 
FROM A_APPROVED_PEOPLE 
WHERE ID=@strNTLogin
print 'my company is' + @myCo + ''


if @fieldList = '' or @fieldList is null
begin
set @fieldList ='ID, 
				MEETING_NAME, 
				SETTING, 
				START_DATE, 
				STOP_DATE, 
				STANDARD_SEARCH,
				LOCATION_NAME, 
				WEB_LOCATION, 
				HOST, 
				OWNER, 
				SCRIBE, 
				TIME_KEEP,
				DATE_EMAIL_SENT,
				CASE WHEN (ID IN (SELECT MEETING_ID FROM A_MEETING_RESPONSES WHERE RESPONSE=''REJECT'' AND PERSON_ID = ''' + @strNTlogin + ''')) THEN  ''REJECT''
					WHEN (ID IN (SELECT MEETING_ID FROM A_MEETING_RESPONSES WHERE RESPONSE=''ACCEPT'' AND PERSON_ID = ''' + @strNTlogin + ''')) THEN   ''ACCEPT''
				ELSE ''NOT REPLIED''
				end RESPONSE '
end 

set @sql = 'SELECT DISTINCT ' +@fieldList + 
			'FROM A_V_MEETING_SEARCH ' + isNull(@alias, '') + '
       		WHERE (SCRIBE = ''' + @strNTLogin  + '''
		    OR 	OWNER=  ''' +  @strNTLogin + '''
			OR TIME_KEEP= ''' +  @strNTLogin + '''
			OR HOST= ''' +  @strNTLogin + '''
			OR  ID IN (
			(SELECT MEETING_ID FROM A_MEETING_INV_PEOPLE WHERE PEOPLE_ID= ''' + @strNTLogin + '''))
			OR  ID IN (
	 		(SELECT MEETING_ID FROM A_MEETING_REALLY_INIVTED_COMPANY WHERE COMPANY_ID= ''' + @myCo + '''))
			OR  ID IN (
	 	   (SELECT MEETING_ID FROM A_MEETING_INV_ROLE WHERE ROLE_ID IN
			(SELECT ROLE_ID FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS WHERE PERSON = ''' +  @strNTLogin +'''))))'
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
	@strPurposes,'A_MEETINGS','PURPOSES','S', @sql output, @strNTLogin
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@strObjects,'A_MEETINGS','OBJECT', 'S', @sql output, @strNTLogin
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@strProjects,'A_MEETINGS','PROJECT', 'S', @sql output, @strNTLogin
end

if @strSort is not null
	begin
	set @sql = @sql + ' ' +  @strSort 
	end
executeSQL:
print 'SQL = ' + @SQL
if @boolDebug = 1 select 'SQL = ' + @SQL
EXEC(@SQL)




















