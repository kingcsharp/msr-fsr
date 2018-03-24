

/*
STORED PROCEDURE CALLED IN meeting/saveMeeting.asp
*/
CREATE             PROCEDURE A_SP_MEETING_UPDATE_INV_COMPANY
@meetingID varchar(50),
@companyID varchar(50),
@optional varchar(50),
@strNTLogin varchar(50)
AS
declare @newID varchar(50)
exec SP_GETUNIQUEID3 @newID OUTPUT 
INSERT INTO A_MEETING_INV_COMPANY
	([ID], [MEETING_ID],[COMPANY_ID], OPTIONAL,[DRCM],[MODBY])
		VALUES(@newID,@meetingID,@companyID, @optional, getDate() ,@strNTLogin)
print 'Finished with A_SP_MEETING_UPDATE_INV_COMPANIES'
exec A_SP_MEETING_UPDATE_REALLY_INVITED_COMPANY @meetingID, @companyID, @strNTLogin