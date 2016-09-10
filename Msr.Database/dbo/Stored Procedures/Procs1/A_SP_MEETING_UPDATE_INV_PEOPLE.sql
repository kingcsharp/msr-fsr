/*
STORED PROCEDURE CALLED IN meeting/saveMeeting.asp
*/
CREATE        PROCEDURE A_SP_MEETING_UPDATE_INV_PEOPLE
@meetingID varchar(50),
@peopleID varchar(50),
@optional varchar(50),
@strNTLogin varchar(50)
AS
declare @newID varchar(50)
exec SP_GETUNIQUEID3 @newID OUTPUT 
INSERT INTO A_MEETING_INV_PEOPLE
	([ID],[MEETING_ID],[PEOPLE_ID], OPTIONAL, [DRCM],[MODBY])
		VALUES(@newID, @meetingID,@peopleID, @optional, getDate() ,@strNTLogin)
print 'Finished with A_SP_MEETING_UPDATE_INV_PEOPLE'






