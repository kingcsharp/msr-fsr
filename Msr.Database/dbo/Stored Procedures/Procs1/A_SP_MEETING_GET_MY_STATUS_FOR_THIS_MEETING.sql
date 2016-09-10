

CREATE     PROCEDURE dbo.A_SP_MEETING_GET_MY_STATUS_FOR_THIS_MEETING
@newID varchar(50) OUTPUT,
@messages varchar(100) OUTPUT,
@meetingID varchar(50),
@strNTLogin varchar(50)
AS
declare @myCo varchar(50), @myRequiredMeetingID varchar (50)

SELECT @myCo = COMPANY 
FROM A_APPROVED_PEOPLE 
WHERE ID=@strNTLogin

	print 'my company is' + @myCo + ''
	SELECT @myRequiredMeetingID = ID 
	FROM A_MEETINGS
	   WHERE ID = @meetingID AND 
		(HOST= @strNTLOGIn OR SCRIBE=@strNTLOGIn OR TIME_KEEP =@strNTLogin)
	if not(@myRequiredMeetingID is null)
	begin
		print 'Found user ' + @strNTLOGIn + ' as HOST, TIMEKEEP, or SCRIBE' 
        set @newID = 'Required' 
        goto finished
	end  

	SELECT @myRequiredMeetingID = ID
	FROM A_MEETING_INV_PEOPLE
	WHERE MEETING_ID = @meetingID and (OPTIONAL = 0 OR OPTIONAL IS NULL) AND PEOPLE_ID = @strNTLogin
   
    if not(@myRequiredMeetingID is null)
	begin
		print 'Found user ' + @strNTLOGIn + ' as a required Person' 
		set @newID = 'Required' 
        goto finished 
	end 

	SELECT @myRequiredMeetingID = ID 
	FROM A_MEETING_INV_COMPANY
    WHERE MEETING_ID = @meetingID  AND (OPTIONAL = 0 OR OPTIONAL IS NULL ) AND COMPANY_ID = @myCo

    if not(@myRequiredMeetingID is null)
	begin
		print 'Found user ' + @strNTLOGIn + ' as a required Company' 
			set @newID = 'Required' 
        goto finished 
	end 

	SELECT @myRequiredMeetingID = ID 
	FROM A_MEETING_INV_ROLE
	   WHERE MEETING_ID = @meetingID AND (OPTIONAL = 0 OR OPTIONAL IS NULL) AND 
	  ROLE_ID IN  (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = @strNTLogin)

    if not(@myRequiredMeetingID is null)
		begin
			print 'Found user ' + @strNTLOGIn + ' as a required Role' 
			set @newID = 'Required' 
	        goto finished 
    	 end 

	SELECT @myRequiredMeetingID = ID 
	FROM A_MEETINGS
	   WHERE ID = @meetingID AND (HOST = @strNTLogin  OR TIME_KEEP = @strNTLogin OR SCRIBE= @strNTLogin)
    if not(@myRequiredMeetingID is null)
		begin
			print 'Found user ' + @strNTLOGIn + ' as a required HOST, TIMEKEEP OR SCRIBE' 
			set @newID = 'Required' 
	        goto finished 
    	 end 
    if @myRequiredMeetingID is null
     	begin
         set @newID = 'Optional'    
		end 
finished:
print 'myRequiredMeetingID is ' +@myRequiredMeetingID
print 'myStatus is ' +@newID


