CREATE PROCEDURE dbo.A_SP_MEETING_UPDATE_AGENDA_TOPIC
@meetingID varchar(50),
@noteID varchar(50),
@agendaItemID varchar(50),
@duration float,
@cnt int,
@strNTLogin varchar(50)
AS
print 'Updating an Agenda topic'
if @agendaItemID = 'NEW_NOTE_ID'
	begin
	print 'This is a totally new Agenda Item'
	set @agendaItemID = newID()
	end
DELETE FROM A_MEETING_TOPICS WHERE ID = @agendaItemID
INSERT INTO A_MEETING_TOPICS (ID,MEETING_ID,NOTE_ID,DURATION,CNT)
	VALUES (@agendaItemID,@meetingID,@noteID,@duration,@cnt)