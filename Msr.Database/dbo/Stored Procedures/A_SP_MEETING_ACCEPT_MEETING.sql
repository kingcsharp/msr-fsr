





/*
STORED PROCEDURE CALLED IN meeting/saveMeeting.asp
*/

CREATE  PROCEDURE A_SP_MEETING_ACCEPT_MEETING
@meetingID varchar(50),
@strNTLogin varchar(50)
AS

declare @newID as nvarchar(50)

exec SP_GETUNIQUEID3 @newID OUTPUT 
INSERT INTO 
A_MEETING_RESPONSES 
([ID],[MEETING_ID], [PERSON_ID], [RESPONSE], [DRCM], [MODBY])
VALUES
(@newID, @meetingID,@strNTLogin, 'ACCEPT', getdate(), @strNTLogin)