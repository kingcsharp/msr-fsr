CREATE PROCEDURE DBO.A_SP_PROJECT_UPDATE_ALL_RELATED_ITEMS
@strID varchar(50),
@strNTLogin varchar(50)
AS

Declare @id nvarchar(50),@type varchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT ITEM_ID,ITEM_TYPE FROM A_PROJECT_ITEM_LINK WHERE PROJECT_ID = @strID
open @curs
Fetch Next from @curs Into @id,@type
while (@@fetch_status = 0)
Begin
	print 'going to update item = ' + @id
	exec A_SP_PROJECT_ITEM_CLEAN_UP_ALL_TAGS @id,@type,@strNTLogin
	Fetch Next from @curs Into @id,@type
End

