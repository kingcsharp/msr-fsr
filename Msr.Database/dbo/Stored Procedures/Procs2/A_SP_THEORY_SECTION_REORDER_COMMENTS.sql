
Create  procedure A_SP_THEORY_SECTION_REORDER_COMMENTS
@ID as varchar(50),
@LOC as varchar(50)
AS
declare @curs as CURSOR,
	@it as varchar(50)
Declare @counter int  
set @counter =0
print 'Reordering the comments for the theory ' + @ID + 'AND LOC = ' + @LOC
set @curs = Cursor For 
SELECT ID FROM A_THEORY_COMMENTS WHERE THEORY_HIST_ID = @ID AND LOC = @LOC
	ORDER BY PRINT_ORDER
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
	Begin
	set @counter = @counter +1 
	print 'reordering= ' + @it
        print 'counter = ' + convert(varchar(50),@counter)	
	UPDATE A_THEORY_COMMENTS SET PRINT_ORDER = @counter WHERE ID = @it
 	Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs

