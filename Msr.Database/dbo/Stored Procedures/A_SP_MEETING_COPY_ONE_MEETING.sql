



/*
STORED PROCEDURE CALLED IN meeting/saveMeeting.asp

*/
CREATE     PROCEDURE A_SP_MEETING_COPY_ONE_MEETING
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@meetingID varchar(50),
@includeNotes varchar(50),
@strNTLogin varchar(50)
AS
declare @copyMeetingID varchar(50)
exec SP_GETUNIQUEID3 @newID OUTPUT 
set @copyMeetingID = @newID
print 'my meeting id for this copy is' +isNull(@copyMeetingID,'null')


print 'Inserting a copy of meeting' + @meetingID
INSERT INTO A_MEETINGS 
([ID],
MEETING_NAME,
SETTING,
START_DATE,
STOP_DATE,
HOST,
TIME_KEEP,
SCRIBE,
LOCATION,
WEB_LOCATION,
COMMENT,
OWNER,
DATE_CREATED,
DRCM,
MODBY)
(SELECT 
@copyMeetingID,
'Follow up to ' + MEETING_NAME,
SETTING,
START_DATE,
STOP_DATE,
HOST,
TIME_KEEP,
SCRIBE,
LOCATION,
WEB_LOCATION,
COMMENT,
@strNTlogin,
getDate(),
getDate(),
MODBY
FROM A_MEETINGS 
WHERE ID = @meetingID)

print 'updating invited people (required)'
INSERT INTO A_MEETING_INV_PEOPLE
(ID,MEETING_ID,PEOPLE_ID,OPTIONAL,DRCM,MODBY)
(SELECT newID(),@copyMeetingID,PEOPLE_ID,OPTIONAL,getDate(),@strNTlogin
FROM A_MEETING_INV_PEOPLE 
WHERE MEETING_ID = @meetingID AND OPTIONAL=0)

print 'updating invited people (optional)'
INSERT INTO A_MEETING_INV_PEOPLE
(ID,MEETING_ID,PEOPLE_ID,OPTIONAL,DRCM,MODBY)
(SELECT newID(),@copyMeetingID,PEOPLE_ID,OPTIONAL,getDate(),@strNTlogin
FROM A_MEETING_INV_PEOPLE 
WHERE MEETING_ID = @meetingID AND OPTIONAL=1)

print 'updating invited roles (required)'
INSERT INTO A_MEETING_INV_ROLE
(ID,MEETING_ID,ROLE_ID,OPTIONAL,DRCM,MODBY)
(SELECT newID(),@copyMeetingID,ROLE_ID,OPTIONAL,getDate(),@strNTlogin
FROM A_MEETING_INV_ROLE
WHERE MEETING_ID = @meetingID AND OPTIONAL=0)

print 'updating invited roles (optional)'
INSERT INTO A_MEETING_INV_ROLE
(ID,MEETING_ID,ROLE_ID,OPTIONAL,DRCM,MODBY)
(SELECT newID(),@copyMeetingID,ROLE_ID,OPTIONAL,getDate(),@strNTlogin
FROM A_MEETING_INV_ROLE 
WHERE MEETING_ID = @meetingID AND OPTIONAL=1)

print 'updating invited companies (required)'
INSERT INTO A_MEETING_INV_COMPANY
(ID,MEETING_ID,COMPANY_ID,OPTIONAL,DRCM,MODBY)
(SELECT newID(),@copyMeetingID,COMPANY_ID,OPTIONAL,getDate(),@strNTlogin
FROM A_MEETING_INV_COMPANY
WHERE MEETING_ID = @meetingID AND OPTIONAL=0)

print 'updating invited companies (optional)'
INSERT INTO A_MEETING_INV_COMPANY
(ID,MEETING_ID,COMPANY_ID,OPTIONAL,DRCM,MODBY)
(SELECT newID(),@copyMeetingID,COMPANY_ID,OPTIONAL,getDate(),@strNTlogin
FROM A_MEETING_INV_COMPANY
WHERE MEETING_ID = @meetingID AND OPTIONAL=1)

print 'updating really invited companies (required)'
INSERT INTO A_MEETING_REALLY_INIVTED_COMPANY
(ID,MEETING_ID,COMPANY_ID,OPTIONAL,DRCM,MODBY)
(SELECT newID(),@copyMeetingID,COMPANY_ID,OPTIONAL,getDate(),@strNTlogin
FROM A_MEETING_REALLY_INIVTED_COMPANY
WHERE MEETING_ID = @meetingID AND OPTIONAL=0)

print 'updating really invited companies (optional)'
INSERT INTO A_MEETING_REALLY_INIVTED_COMPANY
(ID,MEETING_ID,COMPANY_ID,OPTIONAL,DRCM,MODBY)
(SELECT newID(),@copyMeetingID,COMPANY_ID,OPTIONAL,getDate(),@strNTlogin
FROM A_MEETING_REALLY_INIVTED_COMPANY
WHERE MEETING_ID = @meetingID AND OPTIONAL=1)

print 'getting general project info'

print 'Purposes'
INSERT INTO A_BUSINESS_PURPOSES_ITEM_LINK(ID,ITEM_ID,ITEM_TYPE,BUSINESS_PURPOSE_ID,DRCM,MODBY)
SELECT newID(),@copyMeetingID,ITEM_TYPE,BUSINESS_PURPOSE_ID,getDate(),@strNTLogin 
FROM A_BUSINESS_PURPOSES_ITEM_LINK 
WHERE ITEM_ID = @meetingID AND ITEM_TYPE= 'A_MEETINGS'

print 'Objects'
INSERT INTO A_OBJECT_ITEM_LINK (ID,ITEM_ID,ITEM_TYPE,OBJECT_ID,DRCM,MODBY)
SELECT newID(),@copyMeetingID,ITEM_TYPE,OBJECT_ID,getDate(),@strNTLogin FROM A_OBJECT_ITEM_LINK 
WHERE ITEM_ID = @meetingID AND ITEM_TYPE='A_MEETINGS' 

print 'Projects'
INSERT INTO A_PROJECT_ITEM_LINK (ID,ITEM_ID,ITEM_TYPE,PROJECT_ID,DRCM,MODBY)
SELECT newID(),@copyMeetingID,ITEM_TYPE,PROJECT_ID,getDate(),@strNTLogin  FROM A_PROJECT_ITEM_LINK 
WHERE ITEM_ID = @meetingID AND ITEM_TYPE='A_MEETINGS' 

print 'Inserting Tasks'
INSERT INTO A_TASK_MEETING_LINK (ID,MEETING_ID,TASK_ID,DRCM,MODBY)
SELECT newID(),@copyMeetingID,l.TASK_ID,getDate(),@strNTLogin  FROM A_TASK_MEETING_LINK l, A_TASKS t 
WHERE MEETING_ID = @meetingID AND t.STATUS not in ('CLOSED','FINISHED') AND t.ID = l.TASK_ID

print 'Inserting Agenda Items'

CREATE TABLE #myNewAndCopyAgendaIDs (
					NEW_AGENDA_ID varchar(50),					
					COPY_AGENDA_ID varchar(50))
--		select * from #myNewAndCopyAgendaIDs



INSERT INTO #myNewAndCopyAgendaIDs (NEW_AGENDA_ID,COPY_AGENDA_ID)
(SELECT newID(),ID
FROM A_MEETING_AGENDA_ITEMS
WHERE ROOT = @meetingID)

SELECT * FROM #myNewAndCopyAgendaIDs

INSERT INTO A_MEETING_AGENDA_ITEMS ([ID],ROOT,[TEXT],ITEM,RELATED_ITEM,FACILITATOR,START_DATE,DURATION,MODBY,DRCM)
(SELECT NEW_AGENDA_ID,
		@copyMeetingID,
		TEXT,
		ITEM,
		RELATED_ITEM,
		FACILITATOR,
		START_DATE,
		DURATION,
		'SYSTEM_COPY',
		GETDATE()
FROM A_MEETING_AGENDA_ITEMS, #myNewAndCopyAgendaIDs 
WHERE ID = COPY_AGENDA_ID AND ROOT = @meetingID)


if @includeNotes = 'YES'
	begin
		print 'This edit is a copy of another meeting and the user wants to copy the notes.'
		print 'making a copy of the agenda items'
		print 'deleting my system made agendas'
		
-- CREATE TABLE #myNewAndCopyAgendaIDs (
-- 					NEW_AGENDA_ID varchar(50),					
-- 					COPY_AGENDA_ID varchar(50))
-- 		select * from #myNewAndCopyAgendaIDs
-- 
	
-- -- 		INSERT INTO #myNewAndCopyAgendaIDs (NEW_AGENDA_ID,COPY_AGENDA_ID)
-- -- 		(SELECT newID(),ID
-- -- 		FROM A_MEETING_AGENDA_ITEMS
-- -- 		WHERE ROOT = @meetingID)
-- 
-- --		SELECT * FROM #myNewAndCopyAgendaIDs
-- 
-- 		INSERT INTO A_MEETING_AGENDA_ITEMS ([ID],ROOT,[TEXT],ITEM,RELATED_ITEM,FACILITATOR,START_DATE,DURATION,MODBY,DRCM)
-- 		(SELECT NEW_AGENDA_ID,
-- 				@copyMeetingID,
-- 				TEXT,
-- 				ITEM,
-- 				RELATED_ITEM,
-- 				FACILITATOR,
-- 				START_DATE,
-- 				DURATION,
-- 				'SYSTEM_COPY',
-- 				GETDATE()
-- 		FROM A_MEETING_AGENDA_ITEMS, #myNewAndCopyAgendaIDs 
-- 		WHERE ID = COPY_AGENDA_ID AND ROOT = @meetingID)
-- 
		INSERT INTO  A_MEETING_AGENDA_KEY_STATEMENTS (ID,AGENDA_ID,KEY_STATEMENTS,NUM)
		(SELECT newID(),NEW_AGENDA_ID,KEY_STATEMENTS,NUM
		FROM  A_MEETING_AGENDA_KEY_STATEMENTS,#myNewAndCopyAgendaIDs
		WHERE AGENDA_ID = COPY_AGENDA_ID)
	end

print 'Finished inserting my copy. My meeting copy ID is ' +isNull(@newID,'NULL')