







/*
projID = a single project ID ie. '12345'
strWhere = Properly written sql for after the where.  ie. ' ITEM_NAME = ''%red%''
strType = The item type to find must be: NULL | 'A_SURVEYS' | 'A_PROJECTS' | 'A_TASKS' | 'A_DISCUSSIONS' | 'A_MEETINGS' -- NULL gets everything
strStatus = Status of Items. not sure what all the different satuses are NULL | 'OPEN' | 'CREATING' | 'CLOSED'
strFilterType = The type of filtering we are doing must be: NULL | 'PROJ_ID'  -- The NULL searches Not Proj ID
strAddPurposes = List of purposes to search in addition to project info ie. '1234, 12322, 122334'
strAddObject = List of objects to search in addition to project info ie. '1234, 12322, 122334'
strNTLogin = Person's ID ie. '7'
*/

CREATE             PROCEDURE DBO.A_SP_PROJECT_SEARCH_ALL_RELATED_ITEMS
@projID varchar(50),
@strWhere varchar(1000),
@strType varchar(1000),
@strStatus varchar(1000),
@strFilterType varchar(50),
@strAddPurposes varchar(4000),
@strAddObects varchar(4000),
@strOrder varchar(2000),
@strNTLogin varchar(50)
AS
declare @myCo as varchar(50)
SELECT @myCo = COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin

CREATE TABLE #TempItems(
ITEM_ID varchar(50),
ITEM_NAME nvarchar(4000),
ITEM_STATUS varchar(50),
ITEM_TYPE varchar(50),
WHERE_FROM varchar(50),
STD_SEARCH int,
ITEM_PERSON varchar(50),
ITEM_DATE datetime

)

CREATE TABLE #TempProjItems(
ITEM_ID varchar(50),
ITEM_NAME nvarchar(4000),
ITEM_STATUS varchar(50),
ITEM_TYPE varchar(50),
WHERE_FROM varchar(50),
STD_SEARCH int,
ITEM_PERSON varchar(50),
ITEM_DATE datetime
)

CREATE TABLE #TempFinalItems(
ITEM_ID varchar(50),
ITEM_NAME nvarchar(4000),
ITEM_STATUS varchar(50),
ITEM_TYPE varchar(50),
WHERE_FROM varchar(50),
STD_SEARCH int,
ITEM_PERSON varchar(50),
ITEM_DATE datetime
)
declare @sql varchar(8000)
declare @fieldList varchar(8000)
declare @coma varchar(50)

print 'First lets get all the things related by obj and purp'
declare @purposeList varchar(8000),@objectList varchar(8000),@projectList varchar(50)
print 'We need to get the purpose and object lists for this project'
set @sql = 'SELECT OBJECT_ID AS ID FROM A_OBJECT_ITEM_LINK 
			WHERE ITEM_ID = ''' + @projID + ''' AND ITEM_TYPE = ''A_PROJECTS'''
print @sql
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@objectList OUTPUT 
set @sql = 'SELECT BUSINESS_PURPOSE_ID AS ID FROM A_BUSINESS_PURPOSES_ITEM_LINK 
			WHERE ITEM_ID = ''' + @projID +''' AND ITEM_TYPE = ''A_PROJECTS'''
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@purposeList OUTPUT 
set @sql = 'SELECT ITEM_ID AS ID FROM A_PROJECT_ITEM_LINK 
			WHERE PROJECT_ID = ''' + @projID + ''' AND ITEM_TYPE = ''A_PROJECTS'''
print @sql
exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@projectList OUTPUT 
set @projectList = isnull(@projectList+',','') + @projID

print 'The purpose list = ' + isNull(@purposeList,'NULL')
print 'The object list = ' + isNull(@objectList,'NULL')
print 'The @projectList list = ' + isNull(@projectList,'NULL')

--SEarch Messages


print 'processing messages'
set @sql = null

declare @tWhere varchar(8000)
set @fieldList = 'ID AS ITEM_ID,
				MESSAGE AS ITEM_NAME,
				STATUS AS ITEM_STATUS,
				''''A_MESSAGES'''' AS ITEM_TYPE, 
				SENDER_NAME AS ITEM_PERSON,
				DATE_SENT AS ITEM_DATE '
set @tWhere = '(STATUS <> ''CREATING'' AND 1=1 '
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@objectList,'A_MESSAGES','OBJECT', 'S', @tWhere output, @strNTLogin
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@purposeList,'A_MESSAGES','PURPOSES','S', @tWhere output, @strNTLogin
	set @tWhere = @tWhere + ' OR (STATUS <> ''CREATING'' AND 1=1 '
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@projectList,'A_MESSAGES','PROJECT','S', @tWhere output, @strNTLogin
	set @tWhere = @tWhere + ' ))'

--set @sql = 'exec A_SP_MESSAGES_SEARCH ''' + isNull(@fieldList,'NULL') + ''',''' + replace(@tWhere,'''','''''') + ''',''' + '' + ''',''' +  @strNTlogin + ''''
 set @sql = 'exec A_SP_MESSAGES_SEARCH ''' + 
			isNull(@fieldList,'NULL') + ''',''' +  --@fieldList 
			'S'+ ''',''' +  --alias
			+ replace(@tWhere,'''','''''') + ''','''  + --@strWhere 
			'' + ''','''  + -- @strPurposes
			'' + ''','''  + -- @strObjects
			'' + ''','''  + -- @strProjects
			'' + ''','''  + -- @strSort
			 ''+ @strNTlogin + '''' -- @strNTLogin


print 'Messages SQL = ' + @sql
INSERT INTO #TempItems(ITEM_ID,ITEM_NAME,ITEM_STATUS,ITEM_TYPE,ITEM_PERSON,ITEM_DATE)	exec(@sql)

--Search Surveys

set @tWhere = null
set @sql = null
set @fieldList = null

set @fieldList = 	'ID AS ITEM_ID, 
					SUBJECT AS ITEM_NAME, 
					STATUS AS ITEM_STATUS, 
					''''A_SURVEYS'''' AS ITEM_TYPE, 
					INITIATOR_NAME AS ITEM_PERSON,
					DATE_CREATED AS ITEM_DATE ' 
print 'First lets search the surveys'

-- set @sql = dbo.A_FN_SURVEYS_GET_STANDARD_SQL_STRING(@fieldList,'S',@strNTlogin)
-- set @sql = @sql + 'AND ( 1=1 '
-- exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
-- 	@objectList,'A_SURVEYS','OBJECT', 'S', @sql output, @strNTLogin
-- exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
-- 	@purposeList,'A_SURVEYS','PURPOSES','S', @sql output, @strNTLogin
-- 	set @sql = @sql + ' OR ( 1=1 '
-- exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
-- 	@projectList,'A_SURVEYS','PROJECT','S', @sql output, @strNTLogin
-- 	set @sql = @sql + ' ))'

set @tWhere = '( 1=1 '
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@objectList,'A_SURVEYS','OBJECT', 'S',@tWhere output, @strNTLogin
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@purposeList,'A_SURVEYS','PURPOSES','S',@tWhere output, @strNTLogin
	set @tWhere = @tWhere + ' OR ( 1=1 '
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@projectList,'A_SURVEYS','PROJECT','S',@tWhere output, @strNTLogin
	set @tWhere = @tWhere + ' ))'
print 'twhere is' + isNull(@tWhere,'NULL')
set @sql = 'exec A_SP_SURVEY_SEARCH ''' + 
			isNull(@fieldList,'NULL') + ''',''' +  --@fieldList 
			'S'+ ''',''' +  --alias
			+ replace(@tWhere,'''','''''') + ''','''  + --@strWhere 
			'' + ''','''  + -- @strPurposes
			'' + ''','''  + -- @strObjects
			'' + ''','''  + -- @strProjects
			'' + ''','''  + -- @strSort
			 ''+ @strNTlogin + '''' -- @strNTLogin

print 'To search the surveys we will use this sql = ' + isNull(@sql,'NULL')
INSERT INTO #TempItems(ITEM_ID,ITEM_NAME,ITEM_STATUS,ITEM_TYPE,ITEM_PERSON,ITEM_DATE)	exec(@sql)


--Search Projects
set @tWhere = null
set @sql = null
set @fieldList = null

set @fieldList = 	'ID AS ITEM_ID,
					NAME AS ITEM_NAME,
					STATUS AS ITEM_STATUS,
					''''A_PROJECTS'''' AS ITEM_TYPE, 
					CURRENT_LEADER_NAME AS ITEM_PERSON,
					DATE_CREATED AS ITEM_DATE '

print 'Now lets search the projects'
set @tWhere = ' (1=1 '
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@objectList,'A_PROJECTS','OBJECT', 'S', @tWhere output, @strNTLogin
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@purposeList,'A_PROJECTS','PURPOSES','S', @tWhere output, @strNTLogin
set @tWhere = @tWhere + ' OR ( 1=1 '
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@projectList,'A_PROJECTS','PROJECT','S', @tWhere output, @strNTLogin
	set @tWhere = @tWhere + ' ))'

set @sql = 'exec A_SP_PROJECT_SEARCH ''' + 
			isNull(@fieldList,'NULL') + ''',''' +  --@fieldList 
			'S'+ ''',''' +  --alias
			+ replace(@tWhere,'''','''''') + ''','''  + --@strWhere 
			'' + ''','''  + -- @strPurposes
			'' + ''','''  + -- @strObjects
			'' + ''','''  + -- @strProjects
			'' + ''','''  + -- @strSort
			 ''+ @strNTlogin + '''' -- @strNTLogin

print 'To search the projects we will use this sql = ' + isNull(@sql,'NULL')
INSERT INTO #TempItems(ITEM_ID,ITEM_NAME,ITEM_STATUS,ITEM_TYPE,ITEM_PERSON,ITEM_DATE)	exec(@sql)


set @tWhere = null
set @sql = null
set @fieldList = null


--Search Tasks
print 'Now lets search the Tasks'

set @fieldList = 'ID AS ITEM_ID, DESCRIPTION AS NAME, 
			CASE	
			WHEN (STATUS=''''CREATING'''') THEN ''''CREATING''''
			WHEN (STATUS=''''REQUESTED'''') THEN ''''REQUESTED''''
			WHEN (STATUS=''''ACCEPTED'''') THEN ''''ACCEPTED''''
			WHEN (STATUS=''''FINISHED'''') THEN ''''FINISHED''''
			WHEN (STATUS=''''CLOSED'''') THEN ''''CLOSED''''
			ELSE ''''STATUS_UNDETERMINED''''
			end ITEM_STATUS,
			''''A_TASKS'''' AS ITEM_TYPE, 
			LATEST_REQUESTEE_NAME AS ITEM_PERSON, 
			CREATE_DATE AS ITEM_DATE '

set @tWhere = ' (1=1 '
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@objectList,'A_TASKS','OBJECT', 'S', @tWhere output, @strNTLogin
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@purposeList,'A_TASKS','PURPOSES','S', @tWhere output, @strNTLogin
set @tWhere = @tWhere + ' OR ( 1=1 '
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@projectList,'A_TASKS','PROJECT','S', @tWhere output, @strNTLogin
	set @tWhere = @tWhere + '))' 

set @sql = 'exec A_SP_TASKS_SEARCH ''' + 
			isNull(@fieldList,'NULL') + ''',''' +  --@fieldList 
			'S'+ ''',''' +  --alias
			+ replace(@tWhere,'''','''''') + ''','''  + --@strWhere 
			'' + ''','''  + -- @strPurposes
			'' + ''','''  + -- @strObjects
			'' + ''','''  + -- @strProjects
			'' + ''','''  + -- @strSort
			 ''+ @strNTlogin + '''' -- @strNTLogin

print 'Task SQL = ' + @sql
INSERT INTO #TempItems(ITEM_ID,ITEM_NAME,ITEM_STATUS,ITEM_TYPE,ITEM_PERSON,ITEM_DATE)	exec(@sql)


set @tWhere = null
set @sql = null
set @fieldList = null

--Search Meetings

set @fieldList = 'ID AS ITEM_ID,MEETING_NAME AS NAME,
CASE	WHEN (START_DATE > getDate()) THEN ''''Upcoming''''
		WHEN (STOP_DATE < getDate()) THEN ''''Already Held''''
		WHEN (START_DATE < getDate() AND STOP_DATE > getDate()) THEN ''''In Progress''''
		ELSE ''''STATUS_UNDETERMINED''''
		end ITEM_STATUS,
		''''A_MEETINGS'''' AS ITEM_TYPE,
		HOST_NAME AS ITEM_PERSON,
		START_DATE AS ITEM_DATE '

set @tWhere =  '( 1=1 '
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@objectList,'A_MEETINGS','OBJECT', 'S', @tWhere output, @strNTLogin
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@purposeList,'A_MEETINGS','PURPOSES','S', @tWhere output, @strNTLogin
set @tWhere = @tWhere + ' OR ( 1=1 '
exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
	@projectList,'A_MEETINGS','PROJECT','S', @tWhere output, @strNTLogin
set @tWhere = @tWhere + ' ))'

set @sql = 'exec A_SP_MEETING_SEARCH ''' + 
			isNull(@fieldList,'NULL') + ''',''' +  --@fieldList 
			'S'+ ''',''' +  --alias
			+ replace(@tWhere,'''','''''') + ''','''  + --@strWhere 
			'' + ''','''  + -- @strPurposes
			'' + ''','''  + -- @strObjects
			'' + ''','''  + -- @strProjects
			'' + ''','''  + -- @strSort
			 ''+ @strNTlogin + '''' -- @strNTLogin


print 'Meeting SQL = ' + @sql
INSERT INTO #TempItems(ITEM_ID,ITEM_NAME,ITEM_STATUS,ITEM_TYPE,ITEM_PERSON,ITEM_DATE) exec(@sql)


--SUBJECT AS NAME,STATUS AS ITEM_STATUS,

--SEarch Discussions

set @tWhere = null
set @sql = null
set @fieldList = null

set @fieldList = 'ID AS ITEM_ID,
				SUBJECT AS NAME,
				CASE WHEN (STATUS=''''ACTIVE'''') THEN ''''OPEN''''
				WHEN (STATUS=''''CLOSED'''') THEN ''''CLOSED''''
				ELSE ''''STATUS_UNDETERMINED''''
				end	 ITEM_STATUS,
				''''A_DISCUSSIONS'''' AS ITEM_TYPE, 
				INITIATOR_NAME AS ITEM_PERSON, 
				DATE_CREATED AS ITEM_DATE '

set @tWhere = ' ( 1=1 '
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@objectList,'A_DISCUSSIONS','OBJECT', 'S', @tWhere output, @strNTLogin
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@purposeList,'A_DISCUSSIONS','PURPOSES','S', @tWhere output, @strNTLogin
	set @tWhere = @tWhere + ' OR ( 1=1 '
	exec A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
		@projectList,'A_DISCUSSIONS','PROJECT','S', @tWhere output, @strNTLogin
	set @tWhere = @tWhere + ' ))'

set @sql = 'exec A_SP_DISCUSSION_SEARCH ''' + 
			isNull(@fieldList,'NULL') + ''',''' +  --@fieldList 
			'S'+ ''',''' +  --alias
			+ replace(@tWhere,'''','''''') + ''','''  + --@strWhere 
			'' + ''','''  + -- @strPurposes
			'' + ''','''  + -- @strObjects
			'' + ''','''  + -- @strProjects
			'' + ''','''  + -- @strSort
			 ''+ @strNTlogin + '''' -- @strNTLogin

print 'Discussion SQL = ' + @sql
INSERT INTO #TempItems(ITEM_ID,ITEM_NAME,ITEM_STATUS,ITEM_TYPE,ITEM_PERSON,ITEM_DATE)	exec(@sql)


--Get everything related by project ID as well
print 'Surveys related by straight Project Type'
INSERT INTO #TempProjItems(ITEM_ID,ITEM_NAME,ITEM_STATUS,ITEM_TYPE,WHERE_FROM,ITEM_PERSON,ITEM_DATE)
	SELECT 
		p.ITEM_ID,
		s.SUBJECT,
		s.STATUS AS ITEM_STATUS,
		p.ITEM_TYPE,
		'BY_PROJ_ID' AS WHERE_FROM,
		pl.P_NAME AS ITEM_PERSON,		
		s.DATE_CREATED as ITEM_DATE
	FROM A_PROJECT_ITEM_LINK p,A_SURVEYS s, A_V_PEOPLE_BY_NTLOGIN pl   
	WHERE p.PROJECT_ID = @projID AND p.ITEM_TYPE = 'A_SURVEYS' AND p.ITEM_ID = s.ID AND s.OWNER = pl.P_ID

print 'Meetings related by straight Project Type'
INSERT INTO #TempProjItems(ITEM_ID,ITEM_NAME,ITEM_STATUS,ITEM_TYPE,WHERE_FROM,ITEM_PERSON,ITEM_DATE)
	SELECT 
		p.ITEM_ID AS ITEM_ID,
		m.MEETING_NAME AS ITEM_NAME,
		CASE WHEN (m.START_DATE > getDate()) THEN 'Upcoming'
			WHEN (m.STOP_DATE < getDate()) THEN 'Already Held'
			WHEN (START_DATE < getDate() AND STOP_DATE > getDate()) THEN 'In Progress'
			ELSE 'STATUS_UNDETERMINED'
			end ITEM_STATUS,
		p.ITEM_TYPE AS ITEM_TYPE,
		'BY_PROJ_ID' AS WHERE_FROM, 
		pl.P_NAME AS ITEM_PERSON,		
		m.START_DATE as ITEM_DATE
	FROM A_PROJECT_ITEM_LINK p,A_MEETINGS m , A_V_PEOPLE_BY_NTLOGIN pl   
	WHERE p.PROJECT_ID = @projID AND p.ITEM_TYPE = 'A_MEETINGS' AND p.ITEM_ID = m.ID AND m.HOST = pl.P_ID

print 'Discussions related by straight Project ID'
INSERT INTO #TempProjItems(ITEM_ID,ITEM_NAME,ITEM_STATUS,ITEM_TYPE,WHERE_FROM,ITEM_PERSON,ITEM_DATE)
	SELECT 
		p.ITEM_ID AS ITEM_ID,
		m.SUBJECT AS ITEM_NAME,
		CASE WHEN (m.STATUS='ACTIVE') THEN 'OPEN'
		WHEN (m.STATUS='CLOSED') THEN 'CLOSED'
		ELSE 'STATUS_UNDETERMINED'
		end ITEM_STATUS,
		p.ITEM_TYPE AS ITEM_TYPE,
		'BY_PROJ_ID' AS WHERE_FROM,
		pl.P_NAME AS ITEM_PERSON,		
		m.DATE_CREATED as ITEM_DATE
	FROM A_PROJECT_ITEM_LINK p,A_DISCUSSIONS m, A_V_PEOPLE_BY_NTLOGIN pl  
	WHERE p.PROJECT_ID = @projID AND p.ITEM_TYPE = 'A_DISCUSSIONS' AND p.ITEM_ID = m.ID AND m.INITIATOR = pl.P_ID

print 'Tasks related by straight Project Type'
INSERT INTO #TempProjItems(ITEM_ID,ITEM_NAME,ITEM_STATUS,ITEM_TYPE,WHERE_FROM,ITEM_PERSON,ITEM_DATE)
	SELECT 
		p.ITEM_ID AS ITEM_ID,
		t.DESCRIPTION AS ITEM_NAME,
		t.STATUS AS ITEM_STATUS,
		p.ITEM_TYPE AS ITEM_TYPE,
		'BY_PROJ_ID' AS WHERE_FROM,
		pl.P_NAME AS ITEM_PERSON,		
		t.CREATE_DATE as ITEM_DATE		
	FROM A_PROJECT_ITEM_LINK p,A_TASKS t, A_V_PEOPLE_BY_NTLOGIN pl  
	WHERE p.PROJECT_ID = @projID AND p.ITEM_TYPE = 'A_TASKS' AND p.ITEM_ID = t.ID AND t.REQUESTOR = pl.P_ID

print 'Projects related by straight Project'
INSERT INTO #TempProjItems(ITEM_ID,ITEM_NAME,ITEM_STATUS,ITEM_TYPE,WHERE_FROM,ITEM_PERSON,ITEM_DATE)
	SELECT 
		p.ITEM_ID AS ITEM_ID,
		t.NAME AS ITEM_NAME,
		t.STATUS AS ITEM_STATUS,
		p.ITEM_TYPE AS ITEM_TYPE,
		'BY_PROJ_ID' AS WHERE_FROM,
		pl.P_NAME AS ITEM_PERSON,		
		t.DATE_CREATED as ITEM_DATE		
	FROM A_PROJECT_ITEM_LINK p,A_PROJECTS  t , A_V_PEOPLE_BY_NTLOGIN pl  
	WHERE p.PROJECT_ID = @projID AND p.ITEM_TYPE = 'A_PROJECTS' AND p.ITEM_ID = t.ID AND t.LEADER = pl.P_ID


print 'Now We need to decide which items we are goin gto use'
if @strFilterType is null
	begin
	INSERT INTO #TempFinalItems SELECT * FROM #TempItems
	INSERT INTO #TempFinalItems SELECT * FROM #TempProjItems p 
		WHERE NOT EXISTS (SELECT ITEM_ID FROM #TempItems WHERE ITEM_ID = p.ITEM_ID AND ITEM_TYPE=p.ITEM_TYPE)
	end
if isnull(@strFilterType,'') = 'NO_PROJ_ID' 	begin
	INSERT INTO #TempFinalItems SELECT * FROM #TempItems
	end
if isnull(@strFilterType,'') = 'PROJ_ID'
	begin
	INSERT INTO #TempFinalItems SELECT * FROM #TempItems p
		WHERE  EXISTS (SELECT ITEM_ID FROM #TempProjItems WHERE ITEM_ID = p.ITEM_ID AND ITEM_TYPE = p.ITEM_TYPE)
	end 



 
/*

--just need these to test
SELECT 'TI',* FROM #tempItems
SELECT 'TPI',* FROM #tempProjItems
SELECT 'TFI',* FROM #tempFinalItems



*/

declare @myFirstDiscussionID varchar(50)
SELECT @myFirstDiscussionID = DISCUSSION_ID FROM A_PROJECTS WHERE ID = @projID

PRINT 'now sorting my search'
UPDATE #TempFinalItems SET STD_SEARCH = 1 WHERE ITEM_TYPE = 'A_PROJECTS' AND ITEM_STATUS = 'OPEN'
UPDATE #TempFinalItems SET STD_SEARCH = 3 WHERE ITEM_TYPE = 'A_DISCUSSIONS' AND ITEM_STATUS ='OPEN'
UPDATE #TempFinalItems SET STD_SEARCH = 4 WHERE ITEM_TYPE = 'A_SURVEYS' AND ITEM_STATUS='CREATING'
UPDATE #TempFinalItems SET STD_SEARCH = 5 WHERE ITEM_TYPE = 'A_SURVEYS' AND ITEM_STATUS='OPEN'
UPDATE #TempFinalItems SET STD_SEARCH = 6 WHERE ITEM_TYPE = 'A_MEETINGS' AND ITEM_STATUS ='Upcoming'
UPDATE #TempFinalItems SET STD_SEARCH = 7 WHERE ITEM_TYPE = 'A_MEETINGS' AND ITEM_STATUS ='In Progress'
UPDATE #TempFinalItems SET STD_SEARCH = 8 WHERE ITEM_TYPE = 'A_MEETINGS' AND ITEM_STATUS ='STATUS_UNDETERMINED'
UPDATE #TempFinalItems SET STD_SEARCH = 9 WHERE ITEM_TYPE = 'A_TASKS' AND ITEM_STATUS = 'CREATING'
UPDATE #TempFinalItems SET STD_SEARCH = 10 WHERE ITEM_TYPE = 'A_TASKS' AND ITEM_STATUS = 'REQUESTED'
UPDATE #TempFinalItems SET STD_SEARCH = 11 WHERE ITEM_TYPE = 'A_TASKS' AND ITEM_STATUS = 'ACCEPTED'
UPDATE #TempFinalItems SET STD_SEARCH = 12 WHERE ITEM_TYPE = 'A_TASKS' AND ITEM_STATUS = 'FINISHED'
UPDATE #TempFinalItems SET STD_SEARCH = 13 WHERE ITEM_TYPE = 'A_MESSAGES' AND ITEM_STATUS = 'SENT'
				-- FILES GOES HERE 14
UPDATE #TempFinalItems SET STD_SEARCH = 15 WHERE ITEM_TYPE = 'A_PROJECTS' AND ITEM_STATUS = 'CLOSED'
UPDATE #TempFinalItems SET STD_SEARCH = 16 WHERE ITEM_TYPE = 'A_DISCUSSIONS' AND ITEM_STATUS ='CLOSED'
UPDATE #TempFinalItems SET STD_SEARCH = 17 WHERE ITEM_TYPE = 'A_SURVEYS' AND ITEM_STATUS='CLOSED'
UPDATE #TempFinalItems SET STD_SEARCH = 18 WHERE ITEM_TYPE = 'A_MEETINGS' AND ITEM_STATUS ='Already Held'
UPDATE #TempFinalItems SET STD_SEARCH = 19 WHERE ITEM_TYPE = 'A_TASKS' AND ITEM_STATUS = 'CLOSED'
UPDATE #TempFinalItems SET STD_SEARCH = 2 WHERE ITEM_ID = @myFirstDiscussionID

DELETE FROM #TempFinalItems WHERE ITEM_ID = @projID 
print 'now it is time to build the SQL'
set @sql = 'SELECT distinct * FROM #TempFinalItems s WHERE ' + isNull(@strWhere,' ITEM_ID is not null ')
	set @sql = @sql + isNull(' AND ITEM_STATUS = ''' + @strStatus + ''' ','')
set @sql = @sql + isNull(' AND ITEM_TYPE = ''' + @strType + ''' ','')
Declare @it nvarchar(50)
Declare @curs Cursor
if @strAddPurposes is not null
	begin
	print 'We need to search these additional Purposes'
	CREATE TABLE #TempPurposes (IT varchar(50))
	INSERT INTO #TempPurposes Exec A_SP_Z_SPLIT @strAddPurposes,', '
	set @curs = Cursor For SELECT * FROM #TempPurposes
	open @curs
	Fetch Next from @curs Into @it
	while (@@fetch_status = 0)
		Begin
	 	print 'Adding a search for this purpose = ' + @it
	 	set @sql = @sql + 'AND s.ITEM_ID IN (SELECT ITEM_ID FROM A_BUSINESS_PURPOSES_ITEM_LINK WHERE BUSINESS_PURPOSE_ID = ''' + ltrim(@it) + ''' AND ITEM_TYPE = s.ITEM_TYPE)'
	 	Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs
	end
if @strAddObects is not null
	begin
	print 'We need to search these additional Objects'
	CREATE TABLE #TempObjects (IT varchar(50))
	INSERT INTO #TempObjects Exec A_SP_Z_SPLIT @strAddObects,', '
	set @curs = Cursor For SELECT * FROM #TempObjects
	open @curs
	Fetch Next from @curs Into @it
	while (@@fetch_status = 0)
		Begin
	 	print 'Adding a search for this object = ' + @it
	 	set @sql = @sql + 'AND s.ITEM_ID IN (SELECT ITEM_ID FROM A_OBJECT_ITEM_LINK WHERE OBJECT_ID = ''' + ltrim(@it) + ''' AND ITEM_TYPE = s.ITEM_TYPE)'
	 	Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs
	end
set @sql = @sql + isNull(' ' + @strOrder + ' ','')
print 'We need to put the std search orders on the table'




print @sql
exec(@sql)










