CREATE PROCEDURE [dbo].[A_SP_NOTES_SEARCH]
--@fieldList varchar(4000),
--@alias varchar(50),
@strWhere nvarchar(4000),
@strSort nvarchar(1000),
@strNTLogin nvarchar(50)
AS

declare @sql nvarchar(4000)

set @sql = 'SELECT * FROM A_V_NOTES_SEARCH_DATA'
if @strWhere is not null
begin
	set @sql = @sql + ' ' + isNull('WHERE ' + @strWhere,'')
end
print @sql
exec (@sql)
goto fin

-- 
-- declare @isStandardSearch int
-- set @isStandardSearch = 0
-- if @fieldList is null
-- 	begin
-- 		print 'The field list is so null so we know this a standard search from searchDiscussions.asp'
-- 		set @isStandardSearch =1
-- 	end
-- 
-- --find out my Company
-- declare @myCO as nvarchar(50)
-- exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT
-- print @myCO
-- 
-- if @fieldList IS NULL 
-- 	begin
-- 	print 'My field list is null so setting it to the standard list'
-- 	set @fieldList ='ID,
-- 					PARENT_ID,
-- 					SENDER_NAME,
-- 					SENDER,
-- 					MESSAGE,
-- 					DATE_CREATED,
-- 					STATUS ,
-- 					DATE_SENT,
-- 					HIDE_MESSAGE,
-- 					IS_READ,
-- 					IS_CC_MESSAGE,
-- 					IMPORTANCE, 
-- 					hasChild,
-- 					R.PERSON_ID,
-- 					TO_COUNT,
-- 					TO_READ_COUNT,
-- 					CC_COUNT,
-- 					CC_READ_COUNT,
-- 					ROUND(TO_READ_COUNT/TO_COUNT * 100,0) AS TO_READ_RATIO,			
-- 					ROUND(CC_READ_COUNT/CC_COUNT * 100,0) AS CC_READ_RATIO,	
-- 					PARENT_SENDER, 
-- 					CASE 	WHEN (STATUS = ''SENT'' AND IMPORTANCE = ''HIGH'' AND IS_READ = 0 AND IS_CC_MESSAGE = 0 AND PERSON_ID = ''' + @strNTlogin + ''') THEN  1
-- 					WHEN (STATUS = ''SENT'' AND IMPORTANCE = ''NORMAL'' AND IS_READ = 0 AND IS_CC_MESSAGE = 0 AND PERSON_ID = ''' + @strNTlogin + ''') THEN  2
-- 					WHEN (STATUS = ''SENT'' AND IMPORTANCE = ''HIGH'' AND IS_READ = 0 AND IS_CC_MESSAGE = 1 AND PERSON_ID = ''' + @strNTlogin + ''') THEN  3
-- 					WHEN (STATUS = ''SENT'' AND IMPORTANCE = ''NORMAL'' AND IS_READ = 0 AND IS_CC_MESSAGE = 1 AND PERSON_ID = '''+@strNTlogin + ''') THEN  4
-- 					WHEN (STATUS = ''CREATING'') THEN  5
-- 					WHEN (STATUS = ''SENT'' AND IMPORTANCE = ''HIGH'' AND SENDER = ''' + @strNTlogin  + ''' AND (TO_COUNT <> TO_READ_COUNT OR CC_COUNT <> CC_READ_COUNT )) THEN  6
-- 					WHEN (STATUS = ''SENT'' AND IMPORTANCE = ''NORMAL'' AND SENDER = ''' + @strNTlogin + ''' AND (TO_COUNT <> TO_READ_COUNT or CC_COUNT <> CC_READ_COUNT)) THEN  7
-- 					WHEN (STATUS = ''SENT'' AND IS_READ = 1 AND PERSON_ID = '''+ @strNTlogin + + ''') THEN  8
-- 					WHEN (STATUS = ''SENT'' AND SENDER = ''' + @strNTlogin + ''' AND TO_COUNT = TO_READ_COUNT AND (CC_COUNT = CC_READ_COUNT or CC_COUNT is null)) THEN 9
-- 					end STD_SORT_COLUMN '
-- 	set @fieldList = '*'
-- 	end 
-- 
-- 
-- declare @sql nvarchar(4000)
-- 
-- CREATE TABLE #TNotes (
-- 	ID varchar(50),
-- 	TXT nvarchar(4000),
-- 	DRCM datetime,
-- 	MODBY varchar(50),
-- 	PEOPLE_SECURE tinyint,
-- 	COMPANY_SECURE tinyint,
-- 	ROLE_SECURE tinyint,
-- 	SECURITY_LEVEL varchar(50),
-- 	NOTE_TYPE varchar(50),
-- 	SECURITY_TYPE varchar(50),
-- 	RESPONSE_ALLOWED varchar(50),
-- 	STATUS varchar(50),
-- 	AUTHOR varchar(50),
-- 	REVISION int,
-- 	TO_READ_COUNT real,
-- 	DATE_CREATED datetime,
-- 	HIDE_NOTE tinyInt,
-- 	PUBLISH_DATE datetime,
-- 	NOTIFY tinyint,
-- 	)
-- print 'created table #TMessages'
-- 
-- CREATE TABLE #TPeopleView (
-- 	MESSAGE_ID varchar(50),
-- 	PERSON_ID varchar(50),
-- 	IS_READ varchar(50),
-- 	IS_CC_MESSAGE varchar(50)
-- 	)
-- 
-- print 'created table #TPeopleView'
-- 
-- print 'Inserting my message data into #TMessages'
-- INSERT INTO #TMessages (ID,PARENT_ID,PARENT_MESSAGE,SENDER_NAME,SENDER,MESSAGE,DATE_CREATED,STATUS,hasChild,DATE_SENT,HIDE_MESSAGE,IMPORTANCE,TO_COUNT,CC_COUNT,TO_READ_COUNT,CC_READ_COUNT,PARENT_SENDER) 
-- (SELECT DISTINCT ID,PARENT_ID,PARENT_MESSAGE,SENDER_NAME,SENDER,MESSAGE,DATE_CREATED,STATUS,hasChild,DATE_SENT,HIDE_MESSAGE,IMPORTANCE,TO_COUNT,CC_COUNT,TO_READ_COUNT,CC_READ_COUNT, PARENT_SENDER
-- FROM A_V_MESSAGES_SEARCH_DATA 
-- WHERE 
-- (HIDE_MESSAGE = 0 AND SENDER = @strNTlogin AND (STATUS= 'CREATING' OR STATUS= 'SENT')) OR (ID  IN (SELECT MESSAGE_ID FROM A_MESSAGES_PEOPLE_LINK WHERE PERSON_ID= @strNTLogin) AND STATUS='SENT'))
-- 
-- print 'Inserting my people data into #TPeopleView'
-- INSERT INTO #TPeopleView (MESSAGE_ID,PERSON_ID,IS_READ,IS_CC_MESSAGE) 
-- (SELECT MESSAGE_ID,PERSON_ID,IS_READ,IS_CC_MESSAGE
-- FROM A_V_MESSAGES_PEOPLE_LINK_WITH_NAMES
-- WHERE PERSON_ID = @strNTLogin)
-- 
-- print 'Doing default search'
-- set @sql = 'SELECT ' + @fieldList + 
-- 	'FROM #TMessages S LEFT OUTER JOIN
--     #TPeopleView R ON S.ID = R.MESSAGE_ID '
-- 
-- if @strWhere is not null 
-- begin
-- set @sql = @sql + isNull('WHERE ' + @strWhere,'')
-- end
-- 
-- 
-- print @sql
-- runSQL:
-- --if len(@strWhere) > 0
-- --		set @sql = @sql + ' AND (' + @strWhere + ')'
-- 
-- if len(@strSort) > 0
-- 		set @sql = @sql + @strSort
-- 
-- print @sql
-- exec (@sql)


fin:


