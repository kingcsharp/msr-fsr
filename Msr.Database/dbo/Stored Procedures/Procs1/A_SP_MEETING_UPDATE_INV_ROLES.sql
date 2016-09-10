
/*
STORED PROCEDURE CALLED IN meetings/saveMeeting.asp
*/

CREATE      PROCEDURE A_SP_MEETING_UPDATE_INV_ROLES
@meetingID varchar(50),
@roleID varchar(50),
@optional varchar(50),
@strNTLogin varchar(50)
AS
declare @newID as varchar(50)
exec SP_GETUNIQUEID3 @newID OUTPUT 
INSERT INTO A_MEETING_INV_ROLE
	([ID],[MEETING_ID],[ROLE_ID],OPTIONAL, [DRCM],[MODBY])
		VALUES(@newID, @meetingID,@roleID, @optional, getDate() ,@strNTLogin)
print 'Finished with A_SP_MEETING_UPDATE_INV_ROLES'

