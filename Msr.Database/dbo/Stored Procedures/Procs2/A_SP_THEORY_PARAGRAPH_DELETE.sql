


CREATE         PROCEDURE A_SP_THEORY_PARAGRAPH_DELETE 
@ID varchar(50),
@THEORY_OBJ_ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Get the value of the Theory ID for this Theory Object ID'
declare @tID as nvarchar(50)
SELECT @tID = ID FROM A_THEORY_HISTORY WHERE OBJECT_ID = @THEORY_OBJ_ID

DELETE FROM A_THEORY_PARAGRAPHS WHERE 
ID = @ID


/*re-order the paragraphs*/
Declare @curs Cursor
Declare @it varchar(50)
Declare @counter int  
set @counter =0

set @curs = Cursor For 
SELECT  ID FROM A_THEORY_PARAGRAPHS 
WHERE THEORY_ID=@tID ORDER BY PARAGRAPH_ORDER_NUMBER
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
	Begin
	set @counter = @counter +1 
	print 'reordering= ' + @it
        print 'counter = ' 
    	print @counter
        UPDATE A_THEORY_PARAGRAPHS 
	   set PARAGRAPH_ORDER_NUMBER = @counter
       	WHERE ID = @it
 	Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs

















