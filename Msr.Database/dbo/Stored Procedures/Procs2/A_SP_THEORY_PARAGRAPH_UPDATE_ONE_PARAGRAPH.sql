





CREATE          PROCEDURE A_SP_THEORY_PARAGRAPH_UPDATE_ONE_PARAGRAPH
@newID nvarchar(50) OUTPUT,
@messages nvarchar(500) OUTPUT,
@ID nvarchar(50),
@PARAGRAPH_ORDER_NUMBER real,
@PARAGRAPH_TEXT nvarchar(4000),
@THEORY_OBJ_ID nvarchar(50),
@PARAGRAPH_STYLE nvarchar(50),
@PARAGRAPH_NUMBER nvarchar(50),
@PARAGRAPH_INDENT nvarchar(50),
@PARAGRAPH_HEADING nvarchar(400),
@PARAGRAPH_SPACE_STYLE varchar(50),
@ReferenceTheory varchar(8000),
@ReferenceObjects varchar(8000),
@strNTLogin nvarchar(50)
AS
print 'Starting procedure A_SP_THEORY_PARAGRAPH_UPDATE_ONE_PARAGRAPH'
print 'Get the value of the Theory ID for this Theory Object ID'
declare @tID as nvarchar(50)
SELECT @tID = ID FROM A_THEORY_HISTORY WHERE OBJECT_ID = @THEORY_OBJ_ID
if @ID is null
	begin
		print 'ID is Null so we need to create this paragraph'
		exec sp_GetUniqueID3 @newID OUTPUT
		print 'Got a new ID = ' + @newID
		INSERT INTO A_THEORY_PARAGRAPHS (ID) VALUES (@newID)
	end
else
	begin
		print 'The ID is not null so we are just updating paragraph ID = ' + @ID
		set @newID = @ID
	end
print 'Now update all the values with the data passed in'
UPDATE A_THEORY_PARAGRAPHS SET
PARAGRAPH_TEXT = @PARAGRAPH_TEXT,
THEORY_ID = @tID,
PARAGRAPH_ORDER_NUMBER = @PARAGRAPH_ORDER_NUMBER,
PARAGRAPH_STYLE = @PARAGRAPH_STYLE,
PARAGRAPH_NUMBER = @PARAGRAPH_NUMBER,
PARAGRAPH_INDENT = @PARAGRAPH_INDENT,
PARAGRAPH_HEADING = @PARAGRAPH_HEADING,
PARAGRAPH_SPACE_STYLE  = @PARAGRAPH_SPACE_STYLE,
DRCM = getDate(),
MODBY = @strNTLogin
WHERE ID = @newID


print 'Adding links to the reference theory'
DELETE FROM A_THEORY_PARAGRAPH_THEORY_LINK WHERE THEORY_PARAGRAPH = @newID
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @ReferenceTheory,', '
INSERT INTO A_THEORY_PARAGRAPH_THEORY_LINK(ID,THEORY_PARAGRAPH,THEORY_LINK,DRCM,MODBY)
	SELECT newID(),@newID,ltrim(IT),getDate(),@strNTLogin FROM #TempItems

print 'Delete this paragraphs reference objects'
DELETE FROM A_THEORY_PARAGRAPH_OBJECT_LINK WHERE THEORY_PARAGRAPH = @newID
DELETE FROM #TempItems
print 'Now add the new objects'
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @ReferenceObjects,','
INSERT INTO A_THEORY_PARAGRAPH_OBJECT_LINK (ID,THEORY_PARAGRAPH,OBJ_ID,DRCM,MODBY)
		SELECT newID(),@newID,ltrim(IT),getDate(),@strNTLogin FROM #TempItems

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





