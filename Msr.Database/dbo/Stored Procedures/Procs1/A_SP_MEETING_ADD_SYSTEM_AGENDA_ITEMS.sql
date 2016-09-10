
/*
STORED PROCEDURE CALLED IN A_SP_MEETING_UPDATE_ONE_MEETING
						   A_SP_MEETING_COPY_ONE_MEETING
*/

CREATE    PROCEDURE A_SP_MEETING_ADD_SYSTEM_AGENDA_ITEMS
@meetingID varchar(50),
@strNTLogin varchar(50)
AS
print 'running A_SP_MEETING_ADD_SYSTEM_AGENDA_ITEMS  '
print 'getting host and start date for ' + @meetingID
declare @HOST varchar(50)
declare @START_DATE varchar(50)

SELECT @HOST = HOST,
	   @START_DATE = START_DATE
FROM A_MEETINGS 
WHERE ID = @meetingID

--print 'The host value is' + isNull(@HOST,'null')
--print 'The start date value is' + isNull(@START_DATE,'null')

print 'inserting system made agenda items into A_MEETING_AGENDA_ITEMS '
INSERT INTO A_MEETING_AGENDA_ITEMS ([ID] ,[TEXT], [START_DATE],[ITEM], [DURATION], [FACILITATOR], [ROOT], [MODBY],  [DRCM])
VALUES(newID(), 'Record Attendees',@START_DATE,1,30,@HOST,@meetingID,'SYSTEM',getDate())
print 'Inserted Record Attendees '

--     taken out per eric on 4-1-05. Add back when modules are availiable.              
--	INSERT INTO A_MEETING_AGENDA_ITEMS ([ID] ,[TEXT], [START_DATE],[ITEM], [DURATION], [FACILITATOR],[ROOT], [MODBY],  [DRCM])
--	VALUES(newID(), 'Related Sales Opportunity.', @START_DATE, 2, 30, @HOST, @newID, 'SYSTEM' ,  getDate())

--      taken out per eric on 4-1-05. Add back when modules are availiable.              
--	INSERT INTO A_MEETING_AGENDA_ITEMS ([ID] ,[TEXT], [START_DATE],[ITEM], [DURATION], [FACILITATOR],[ROOT], [MODBY],  [DRCM])
--	VALUES(newID(), 'Related Sales Threat.', @START_DATE, 3,30, @HOST, @newID, 'SYSTEM' ,  getDate())

--      taken out per eric on 4-1-05      
--	INSERT INTO A_MEETING_AGENDA_ITEMS ([ID] ,[TEXT], [START_DATE],[ITEM], [DURATION], [FACILITATOR],[ROOT], [MODBY],  [DRCM])
--	VALUES(newID(), 'Related Purchase Requirements.', @START_DATE, 4, 30,@HOST, @newID, 'SYSTEM' ,  getDate())

INSERT INTO A_MEETING_AGENDA_ITEMS ([ID] ,[TEXT],[RELATED_ITEM],[START_DATE],[ITEM],[DURATION],[FACILITATOR],[ROOT],[MODBY],[DRCM])
VALUES(newID(), 'Related Tasks.','relatedTaskMeeting',@START_DATE,2,30,@HOST,@meetingID,'SYSTEM',getDate())
print 'Inserted Related Tasks'

--INSERT INTO A_MEETING_AGENDA_ITEMS ([ID],[TEXT],[START_DATE],[ITEM],[DURATION],[FACILITATOR],[ROOT],[MODBY],[DRCM])
--VALUES(newID(),'Related Escalated Tasks.',@START_DATE,3,30,@HOST,@meetingID,'SYSTEM',getDate())
--print 'Inserted Related Escalated Tasks.'

INSERT INTO A_MEETING_AGENDA_ITEMS ([ID] ,[TEXT], [START_DATE],[ITEM], [DURATION], [FACILITATOR],[ROOT], [MODBY],  [DRCM])
VALUES(newID(), 'Tentatively Scheduled Next Meeeting.', @START_DATE,4,30,@HOST,@meetingID, 'SYSTEM' ,  getDate())
print 'Tentatively Scheduled Next Meeeting.'
print 'FINISHED WITH A_SP_MEETING_ADD_SYSTEM_AGENDA_ITEMS '


