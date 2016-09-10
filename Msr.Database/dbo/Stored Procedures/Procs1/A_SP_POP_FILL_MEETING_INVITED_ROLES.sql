


/*
STORED PROCEDURE CALLED IN meeting/saveMeeting.asp
*/

CREATE              PROCEDURE A_SP_POP_FILL_MEETING_INVITED_ROLES
@MEETINGID varchar(50),
@optional varchar(50),
@strNTLogin varchar(50)
AS
if @optional ='1' 
begin
	 print 'getting optional person' 
	SELECT * FROM  A_V_POP_FILL_MEETING_INVITED_ROLES
	WHERE M_ID=@MEETINGID AND OPTIONAL=1
end
else if @optional ='0' 
begin
 print 'getting required person' 
 SELECT * FROM  A_V_POP_FILL_MEETING_INVITED_ROLES
	WHERE M_ID=@MEETINGID AND (OPTIONAL=0 OR OPTIONAL IS NULL)

end












