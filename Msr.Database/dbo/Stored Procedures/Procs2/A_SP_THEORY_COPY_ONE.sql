




CREATE            procedure A_SP_THEORY_COPY_ONE
	@newObjID nvarchar(50) OUTPUT,
	@strID nvarchar(50),
	@copyPrefix nvarchar(50),
	@strNTLogin nvarchar(50)
as
--Make a new ID for the copy
declare @newID as nvarchar(50)
exec sp_getUniqueID3 @newID OUTPUT
--Insert the new one
INSERT INTO A_THEORY_HISTORY (ID,NAME,COMMENTS,SECURITY_LEVEL,DRCM,MODBY)
SELECT @newID as ID,NAME,COMMENTS,SECURITY_LEVEL,getDate(),@strNTLogin 
FROM A_THEORY_HISTORY WHERE ID = @strID
--if it is a copy then change the name
if len(@copyPrefix) > 0
	begin
	UPDATE A_THEORY_HISTORY SET
	NAME = @copyPrefix + NAME
	WHERE
	ID = @newID
	end
--find out what object id the new one got
print 'Copied the theory'
SELECT @newObjID = OBJECT_ID FROM A_THEORY_HISTORY WHERE ID = @newID
print 'The new object ID is ' + @newObjID

print 'Copy all the roles allowed too'
INSERT INTO A_THEORY_ROLES_ALLOWED (ID,THEORY_ID,ROLE_ID,DRCM,MODBY)
SELECT newID(),@newID,ROLE_ID,DRCM,MODBY FROM A_THEORY_ROLES_ALLOWED WHERE THEORY_ID = @strID

print 'Copy all the ref objects too'
INSERT INTO A_THEORY_REFERENCE_OBJECTS (ID,THEORY_ID,OBJ_ID,DRCM,MODBY)
SELECT newID(),@newID,OBJ_ID,DRCM,MODBY FROM A_THEORY_REFERENCE_OBJECTS WHERE THEORY_ID = @strID

print 'Copy all the ref theory too'
INSERT INTO A_THEORY_REFERENCE_THEORY (ID,THEORY_ID,THEORY_LINK,DRCM,MODBY)
SELECT newID(),@newID,THEORY_LINK,DRCM,MODBY FROM A_THEORY_REFERENCE_THEORY WHERE THEORY_ID = @strID

--now we need to copy all the paragraphs
Declare @Paragraph nvarchar(50)
Declare @ParagraphCursor Cursor
set @ParagraphCursor = Cursor For SELECT ID FROM A_THEORY_PARAGRAPHS  WHERE THEORY_ID = @strID
open @ParagraphCursor
Fetch Next from @ParagraphCursor
Into @Paragraph
while (@@fetch_status = 0)
Begin
	print 'Copying the paragraph = ' + @Paragraph
	exec A_SP_THEORY_PARAGRAPH_COPY @Paragraph,@strID,@newID,@strNTLogin
	Fetch Next from @ParagraphCursor Into @Paragraph
End
close @ParagraphCursor
Deallocate @ParagraphCursor


print 'We need to copy all the comments too'
INSERT INTO A_THEORY_COMMENTS (ID,DATA,LOC,THEORY_HIST_ID,DRCM,MODBY,PRINT_ORDER)
SELECT newID(),DATA,LOC,@newID,getDate(),@strNTLogin,PRINT_ORDER FROM A_THEORY_COMMENTS
WHERE THEORY_HIST_ID = @strID





