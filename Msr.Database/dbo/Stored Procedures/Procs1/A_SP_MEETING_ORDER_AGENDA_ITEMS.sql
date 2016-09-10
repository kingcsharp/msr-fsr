


/*
STORED PROCEDURE CALLED IN meeting/listAgendaMeeting.asp
*/
CREATE                  PROCEDURE A_SP_MEETING_ORDER_AGENDA_ITEMS
@meetingID varchar (50),
@strNTLogIn varchar(50)
AS
Declare @duration int 
Declare @curs Cursor
Declare @it varchar(50)
Declare @counter int  
set @counter =0
Declare @currentTime varchar(50) 
print 'meeting id '
print @meetingID

SELECT @currentTime = START_DATE FROM A_MEETINGS 
	WHERE ID=@meetingID


print 'reordering regular agenda list'
print @currentTime
set @curs = Cursor For 
SELECT  ID, DURATION FROM A_MEETING_AGENDA_ITEMS 
WHERE ROOT=@meetingID ORDER BY ITEM
open @curs
Fetch Next from @curs Into @it, @duration
while (@@fetch_status = 0)
	Begin
	set @counter = @counter +1 
	print 'reordering= ' + @it
    print 'counter = ' 
   	print @counter
	print 'duration is'
	print @duration
	print 'current time is'
	print @currentTime
    	UPDATE A_MEETING_AGENDA_ITEMS 
	   	set ITEM = @counter,
	   	START_DATE = @currentTime
       	WHERE ID = @it 
	set @currentTime = DATEADD(mi, isnull(@duration,0),@currentTime) 
 	Fetch Next from @curs Into @it, @duration
	End
close @curs
Deallocate @curs


