



/*
STORED PROCEDURE CALLED IN meetings/editAgendaMeeting.asp
*/

CREATE   PROCEDURE A_SP_MEETING_GET_AGENDA_COUNT 
@lastItem int OUTPUT,
@msg varchar(1000) OUTPUT,
@meetingID varchar(50),
@strNTLogin varchar(50)
AS

print 'Getting number of last item'
SELECT @lastItem = COUNT(ITEM) 
FROM A_MEETING_AGENDA_ITEMS 
WHERE ROOT=@meetingID

set @lastItem = @lastItem +1 
print 'count is'
print +@lastItem














































