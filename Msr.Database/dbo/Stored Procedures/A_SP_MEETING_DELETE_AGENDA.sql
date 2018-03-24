


CREATE        PROCEDURE A_SP_MEETING_DELETE_AGENDA 
@agendaID varchar(50),
@meetingID varchar(50),
@strNTLogin varchar(50)
AS
DELETE FROM A_MEETING_AGENDA_ITEMS WHERE 
ID = @agendaID

exec A_SP_MEETING_ORDER_AGENDA_ITEMS @meetingID, @strNTLogin