








/*
STORED PROCEDURE CALLED IN meeting/editMeeting.asp

*/
CREATE                                PROCEDURE A_SP_MEETING_UPDATE_ONE_MEETING
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@meetingID varchar(50),
@MEETING_NAME nvarchar(500),
@SETTING varchar(50),
@COMMENT nvarchar(4000),
@START_DATE varchar(50),
@STOP_DATE varchar(50),
@HOST varchar(50),
@TIME_KEEP varchar(50),
@SCRIBE varchar(50),
@LOCATION varchar(50),
@WEB_LOCATION nvarchar (500),
@strNTLogin varchar(50)
AS
set @START_DATE = dbo.timeToGrenich(@START_DATE,@strNTLogin)
set @STOP_DATE =dbo.timeToGrenich(@STOP_DATE,@strNTLogin)

if @meetingID is null
begin
 print 'we are updating a New meeting'

   	exec SP_GETUNIQUEID3 @newID OUTPUT 
	INSERT INTO A_MEETINGS ([ID] ,[OWNER], [DATE_CREATED],[HOST],[START_DATE])
	VALUES(@newID,@strNTLogin , getDate(),@HOST,@START_DATE)
	set @meetingID=@newID
	 print 'we are updating the agendas'
	 print 'start date is ' +@START_DATE
	print 'inserting system made agenda items'
	EXEC A_SP_MEETING_ADD_SYSTEM_AGENDA_ITEMS @meetingID, @strNTlogin
end

set @newID = @meetingID

print 'we are updating the meeting with a meeting id of ' +@meetingID
UPDATE A_MEETINGS 
set MEETING_NAME=@MEETING_NAME,
SETTING = @SETTING,
COMMENT=@COMMENT,
START_DATE = @START_DATE,
STOP_DATE = @STOP_DATE,
HOST= @HOST,
TIME_KEEP=@TIME_KEEP,
SCRIBE= @SCRIBE,
LOCATION= @LOCATION,
WEB_LOCATION= @WEB_LOCATION,
DRCM = getDate(),
MODBY = @strNTLogin
WHERE ID = @meetingID 


exec A_SP_MEETING_ORDER_AGENDA_ITEMS @meetingID, @strNTLogin 



