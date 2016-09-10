




/*
STORED PROCEDURE CALLED IN 

MODULE: meeting/editMeeting.asp
MODULE: meeting/viewMeeting.asp
*/

CREATE              PROCEDURE A_SP_POP_FILL_MEETING_INVITED_PEOPLE
@MEETINGID varchar(50),
@optional varchar(50),
@strNTLogin nvarchar(50)
AS
print 'optional is ' +@optional
if @optional ='0'
begin
    print 'getting required person' 
	SELECT * FROM  A_V_POP_FILL_MEETING_INVITED_PEOPLE
	WHERE M_ID=@MEETINGID AND (OPTIONAL = 0 OR OPTIONAL IS NULL)
end 
else if @optional='1'
begin
    print 'getting optional person' 
	SELECT * FROM  A_V_POP_FILL_MEETING_INVITED_PEOPLE
	WHERE M_ID=@MEETINGID AND  OPTIONAL = 1 
end 







