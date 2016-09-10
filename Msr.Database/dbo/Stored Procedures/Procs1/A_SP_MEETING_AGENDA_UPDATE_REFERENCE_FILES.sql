








/*
STORED PROCEDURE CALLED IN meeting/saveAgendaFiles.asp
*/

CREATE      PROCEDURE A_SP_MEETING_AGENDA_UPDATE_REFERENCE_FILES
@agendaID nvarchar(50),
@docID nvarchar(2000),
@strNTLogin nvarchar(50)
AS

declare @newID varchar(50)
exec SP_GETUNIQUEID3 @newID OUTPUT 
INSERT INTO A_MEETING_AGENDA_ITEM_DOC_LINK
	([ID],[AGENDA_ID],[DOC_ID],[CREATOR_ID], [MODBY],[DRCM])
	VALUES(@newID,@agendaID, @docID, @strNTLogin, @strNTLogin,getDate())




