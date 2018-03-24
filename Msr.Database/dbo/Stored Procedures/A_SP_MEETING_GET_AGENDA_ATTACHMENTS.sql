









/*
STORED PROCEDURE CALLED IN meetings/viewAgendaMeeting.asp
*/
CREATE  PROCEDURE A_SP_MEETING_GET_AGENDA_ATTACHMENTS
@agendaID nvarchar(50),
@strNTLogin nvarchar(50)
AS

SELECT * FROM A_V_MEETING_AGENDA_ITEM_GET_DOC_INFO 
where AGENDA_ID = @agendaID