CREATE PROCEDURE dbo.A_MEETINGS_REORDER_AGENDA_TOPICS
@meetingID varchar(50)
AS
declare @curs as Cursor,@it as varchar(50),@c int
set @c = 1
set @curs = CURSOR FOR SELECT ID FROM A_MEETING_TOPICS WHERE MEETING_ID = @meetingID ORDER BY CNT
open @curs
fetch next from @curs into @it
while @@fetch_status = 0 
	begin
	UPDATE A_MEETING_TOPICS SET CNT = @c WHERE ID = @it
	set @c = @c + 1
	fetch next from @curs into @it
	end