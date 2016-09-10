
CREATE  procedure A_SP_THEORY_REORDER_COMMENTS
@ID as varchar(50)
AS
declare @curs as CURSOR,
	@it as varchar(50)
print 'Reordering the comments for the theory ' + @ID
set @curs = Cursor For 
SELECT DISTINCT LOC FROM A_THEORY_COMMENTS WHERE THEORY_HIST_ID = @ID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
	Begin
	print 'reordering Comments in the area of ' + @it
	exec A_SP_THEORY_SECTION_REORDER_COMMENTS @ID,@it
 	Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs




